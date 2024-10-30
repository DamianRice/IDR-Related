using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Autofac.Core;
using Microsoft.Extensions.DependencyInjection;
using Volo.Abp.Autofac;
using Volo.Abp.Json.Newtonsoft;
using Volo.Abp.Modularity;
using WinUiAbpTemplate.Aop;
using WinUiAbpTemplate.Application;
using WinUiAbpTemplate.Services;
using WinUiAbpTemplate.ViewModels.Pages;
using WinUiAbpTemplate.ViewModels.Windows;
using WinUiAbpTemplate.Views.Pages;
using WinUiAbpTemplate.Views.Windows;
using Wpf.Ui;

namespace WinUiAbpTemplate;

[DependsOn(
    typeof(AbpAutofacModule),
    typeof(AbpJsonNewtonsoftModule),
    typeof(WinUiAbpTemplateApplicationModule),  // TODO: 注入Interface的实现类Module\
    typeof(WinUiAbpTemplateAopModule) // TODO: Aop的实现Module
)]
public class WinUiAbpTemplateModule : AbpModule
{
    public override void ConfigureServices(ServiceConfigurationContext context)
    {
        // TODO:注入当前Module下的服务

        // App Host
        context.Services.AddHostedService<ApplicationHostService>();

        // Page resolver service
        context.Services.AddSingleton<IPageService, PageService>();

        // Theme manipulation
        context.Services.AddSingleton<IThemeService, ThemeService>();

        // TaskBar manipulation
        context.Services.AddSingleton<ITaskBarService, TaskBarService>();

        // Service containing navigation, same as INavigationWindow... but without window
        context.Services.AddSingleton<INavigationService, NavigationService>();

        // Main window with navigation
        context.Services.AddSingleton<INavigationWindow, MainWindow>();
        context.Services.AddSingleton<MainWindowViewModel>();

        context.Services.AddSingleton<DashboardPage>();
        context.Services.AddSingleton<DashboardViewModel>();
        context.Services.AddSingleton<DataPage>();
        context.Services.AddSingleton<DataViewModel>();
        context.Services.AddSingleton<SettingsPage>();
        context.Services.AddSingleton<SettingsViewModel>();
    }
}