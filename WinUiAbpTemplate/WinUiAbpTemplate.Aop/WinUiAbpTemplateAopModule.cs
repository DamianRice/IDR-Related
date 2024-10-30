using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.DependencyInjection;
using Volo.Abp.Modularity;

namespace WinUiAbpTemplate.Aop;

[DependsOn(
    typeof(AbpAutofacModule)
)]
public class WinUiAbpTemplateAopModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        context.Services.AddTransient<LoggingInterceptor>();

        context.Services.OnRegistered(registeredService =>
        {
            if (ShouldInterceptWithAttribute<LoggingInterceptor, LoggingInterceptorAttribute>(registeredService.ImplementationType))
            {
                registeredService.Interceptors.Add<LoggingInterceptor>();
            }
        });
    }

    private bool ShouldIntercept(Type? type)
    {
        if (type == null || type.IsSealed || type.IsAbstract || type == typeof(LoggingInterceptor))
            return false;

        return (typeof(ITransientDependency).IsAssignableFrom(type) ||
                typeof(ISingletonDependency).IsAssignableFrom(type)) &&
               type.GetMethods().Any(m => m.IsVirtual && !m.IsFinal);
    }

    private bool ShouldInterceptWithAttribute<TInterceptor, TAttribute>(Type? targetType)
    {
        if (targetType == null || targetType.IsSealed || targetType.IsAbstract || targetType == typeof(TInterceptor)) return false;

        // var  contains                   = targetType.FullName.Contains("PictureApplicationService");
        bool isTargetTypeAbpDependency = typeof(ITransientDependency).IsAssignableFrom(targetType) || typeof(ISingletonDependency).IsAssignableFrom(targetType);
        bool isMethodVirtualAndNotFinal = targetType.GetMethods().Any(m => m is { IsVirtual: true, IsFinal: false });
        bool isDefinedInterceptor = targetType.GetInterfaces().Any(t => t.CustomAttributes.Any(a => a.AttributeType == typeof(TAttribute)));

        return (isTargetTypeAbpDependency && isMethodVirtualAndNotFinal && isDefinedInterceptor);
    }
}