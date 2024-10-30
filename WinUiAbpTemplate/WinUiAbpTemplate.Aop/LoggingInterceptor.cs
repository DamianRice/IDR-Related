#region Header

// 2024
// 10

#endregion

using System.Diagnostics;
using Serilog;
using Volo.Abp.DependencyInjection;
using Volo.Abp.DynamicProxy;

namespace WinUiAbpTemplate.Aop;

[ExposeServices(typeof(LoggingInterceptor))]
public class LoggingInterceptor : IAbpInterceptor, ITransientDependency
{
    public async Task InterceptAsync(IAbpMethodInvocation invocation)
    {
        try
        {
            Stopwatch _stopwatch = new Stopwatch();
            _stopwatch.Start();
            await invocation.ProceedAsync();
            _stopwatch.Stop();
            Log.Logger.Debug("Function: {FunctionName}, Running in {ms} ms",
                invocation.Method.Name,
                _stopwatch.ElapsedMilliseconds);
        }
        catch (Exception e)
        {
            Log.Logger.Error(e, "Error in {FunctionName}", invocation.Method.Name);
        }
    }
}