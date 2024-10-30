using Castle.Core.Logging;
using Microsoft.Extensions.Logging;
using WinUiAbpTemplate.Application.Abstract;

namespace WinUiAbpTemplate.ViewModels.Pages;

public partial class DashboardViewModel : ObservableObject
{
    [ObservableProperty] private int _counter = 0;

    [RelayCommand]
    private void OnCounterIncrement()
    {
        Counter++;
    }


    public DashboardViewModel(ILogger<DashboardViewModel> logger, ITestService testService)
    {
        logger.LogInformation(testService.GetMessage());
    }
}