using System.IO;
using System.Windows;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using ModuleFunctionA;
using Prism;
using Prism.DryIoc;
using Prism.Ioc;
using Prism.Modularity;
using PrismCoreTemplate.Models;
using PrismCoreTemplate.Modules.ModuleName;
using PrismCoreTemplate.Services;
using PrismCoreTemplate.Services.Interfaces;
using PrismCoreTemplate.Views;
using Serilog;
using Serilog.Core;
using Serilog.Events;


namespace PrismCoreTemplate;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App : PrismApplication
{
    protected override Window CreateShell()
    {
        // TODO:启动页面
        return Container.Resolve<MainWindow>();
    }

    #region [Override]

    protected override void OnStartup(StartupEventArgs e)
    {
        // TODO: 程序启动前
        base.OnStartup(e);
    }

    protected override void OnExit(ExitEventArgs e)
    {
        // TODO: 程序退出前
        base.OnExit(e);
    }

    #endregion

    #region [DI]

    protected override void RegisterTypes(IContainerRegistry containerRegistry)
    {
        // TODO: 目前由于Prism的大版本更新，Prism.Microsoft.DependencyInjection.Extensions已经弃用，如果要使用最新的Prism MsDI集成，需要Commercial Plus License
        containerRegistry.RegisterServices(AddBasicComponents);
        containerRegistry.RegisterServices(AddDesktopServices);

        // TODO: 如果不使用Prism MsDI集成，那么下面两个静态方法中注册方式变更为：
        // containerRegistry.RegisterSingleton<IMessageService, MessageService>();
    }

    private static void AddBasicComponents(IServiceCollection services)
    {
        // Notice:这里的配置文件是在项目启动时加载的，如果需要在运行时加载配置文件，需要使用IOptionsSnapshot<T>来实现
        IConfigurationRoot _config = new ConfigurationBuilder()
            .SetBasePath(Directory.GetCurrentDirectory()) //指定配置文件所在的目录
#if DEBUG
            .AddJsonFile("appsettings.dev.json", true, true) //mantained separate config file
#elif RELEASE
            .AddJsonFile("appsettings.json", true, true) //mantained separate config file
#endif
            .Build();

        // 注册Logger,默认的静态全局Logger，适合在Static Function调用
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
            .WriteTo.Async(c => c.Console())
#else
            .ReadFrom.Configuration(_config) //使用配置文件中的设置，具体配置方案见Serilog wiki
#endif
            .Enrich.FromLogContext()
            .Enrich.WithProcessId()
            .Enrich.WithProcessName()
            .CreateLogger();

        // services.AddOptions(); //注入IOptions<T>，才可以在DI容器中获取IOptions<T>
        services.AddSingleton<IConfiguration>(_config);
        services.AddLogging(loggingBuilder => { loggingBuilder.AddSerilog(dispose: true); });
    }

    private static void AddDesktopServices(IServiceCollection services)
    {
        // TODO: 注册Desktop使用的服务，注意这里不是Domain or Application Service
        services.AddSingleton<IMessageService, MessageService>();
    }

    #endregion

    #region [Module注册]

#if RELEASE
        protected override IModuleCatalog CreateModuleCatalog()
        {
            // 所有的Module加载方式均为,是通过App.Config进行加载的
            // see: https://docs.prismlibrary.com/docs/modularity/index.html
            return new ConfigurationModuleCatalog();
        }
#endif

#if DEBUG
    protected override void ConfigureModuleCatalog(IModuleCatalog moduleCatalog)
    {
        moduleCatalog.AddModule<ModuleNameModule>();
        moduleCatalog.AddModule<ModuleFunctionAModule>();
    }
#endif

    #endregion
}