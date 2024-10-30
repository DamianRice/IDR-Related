using System.IO;
using System.Reflection;
using System.Windows.Threading;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Serilog;
using Serilog.Events;
using Volo.Abp;
using WinUiAbpTemplate.Services;
using WinUiAbpTemplate.ViewModels.Pages;
using WinUiAbpTemplate.ViewModels.Windows;
using WinUiAbpTemplate.Views.Pages;
using WinUiAbpTemplate.Views.Windows;
using Wpf.Ui;

namespace WinUiAbpTemplate;

/// <summary>
/// Interaction logic for App.xaml
/// </summary>
public partial class App
{
    // The.NET Generic Host provides dependency injection, configuration, logging, and other services.
    // https://docs.microsoft.com/dotnet/core/extensions/generic-host
    // https://docs.microsoft.com/dotnet/core/extensions/dependency-injection
    // https://docs.microsoft.com/dotnet/core/extensions/configuration
    // https://docs.microsoft.com/dotnet/core/extensions/logging
    private IHost _host;
    private IAbpApplicationWithExternalServiceProvider _application;

    /// <summary>
    /// Gets registered service.
    /// </summary>
    /// <typeparam name="T">Type of the service to get.</typeparam>
    /// <returns>Instance of the service or <see langword="null"/>.</returns>
    public T GetService<T>()
        where T : class
    {
        // TODO:不用这个GetService也行，理论上来说WPF中任何地方都可以直接调用App实例的GetService<IService>(typeof(Service));
        // 一般直接DI给Ctor就好
        return _host.Services.GetService(typeof(T)) as T;
    }

    /// <summary>
    /// Occurs when the application is loading.
    /// </summary>
    private void OnStartup(object sender, StartupEventArgs e)
    {
        _host.Start();
    }

    /// <summary>
    /// Occurs when the application is closing.
    /// </summary>
    private async void OnExit(object sender, ExitEventArgs e)
    {
        await _host.StopAsync();

        _host.Dispose();
    }

    /// <summary>
    /// Occurs when an exception is thrown by an application but not handled.
    /// </summary>
    private void OnDispatcherUnhandledException(object sender, DispatcherUnhandledExceptionEventArgs e)
    {
        // For more info see https://docs.microsoft.com/en-us/dotnet/api/system.windows.application.dispatcherunhandledexception?view=windowsdesktop-6.0
        // TODO:崩溃重启，通过实现Host主机的重启
        Log.Logger.Fatal("Error not handled from {@SenderObj}, Error: {@Error}", sender, e);
    }


    public App()
    {
        Log.Logger = new LoggerConfiguration()
#if DEBUG
            .MinimumLevel.Debug()
#else
                    .MinimumLevel.Information()
#endif
            .MinimumLevel.Override("Microsoft", LogEventLevel.Information)
            .Enrich.FromLogContext()
            .WriteTo.Async(c => c.File("Logs/logs.txt"))
#if DEBUG
            .WriteTo.Async(c => c.Console())
#endif
            .CreateLogger();

        _host = CreateHostBuilder();

        // 从ABP.Core带来的一定不是Null,Null的话直接报错，说明缺DLL
        _application = _host.Services.GetService<IAbpApplicationWithExternalServiceProvider>() ??
                       throw new NullReferenceException(
                           "Not Found Instance of IAbpApplicationWithExternalServiceProvider");
    }

    private IHost CreateHostBuilder()
    {
        // TODO: host注入，如果需要DB支持，直接在这里注入，不需要手动
        return Host
            .CreateDefaultBuilder(null)
            .UseAutofac()
            .UseSerilog()
            // TODO: 核心是这一句，启动Application, 基于这个加载方式，可以把前后台服务，WPF UI进程全部分离开
            .ConfigureServices((hostContext, services) => { services.AddApplication<WinUiAbpTemplateModule>(); })
            .Build();
    }
}