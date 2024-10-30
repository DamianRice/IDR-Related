using System.Windows.Controls;
using System.Windows.Documents;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Prism.Commands;
using Prism.Mvvm;
using Prism.Regions;
using PrismCoreTemplate.Core;
using PrismCoreTemplate.Core.Mvvm;
using PrismCoreTemplate.Models;

namespace PrismCoreTemplate.ViewModels;

public class MainWindowViewModel : BindableBase
{
    private string _title = "Prism Application";

    public string Title
    {
        get => _title;
        set => SetProperty(ref _title, value);
    }


    private ListBoxItem _selectedItem;

    public ListBoxItem SelectedItem
    {
        get => _selectedItem;
        set => SetProperty(ref _selectedItem, value);
    }


    private int _selectedIndex;

    public int SelectedIndex
    {
        get => _selectedIndex;
        set => SetProperty(ref _selectedIndex, value);
    }


    public DelegateCommand<SelectionChangedEventArgs> OnNavigationChangedCommand { get; private set; }


    private readonly ILogger<MainWindowViewModel> _logger;
    public readonly IRegionManager _regionManager;


    public MainWindowViewModel(ILogger<MainWindowViewModel> logger, IConfiguration configuration,
        IRegionManager regionManager)
    {
        _logger = logger;
        _regionManager = regionManager;
        _logger.LogInformation(configuration.GetSection("CustomerSettings:Name").Value ?? "No name found");
        OnNavigationChangedCommand = new DelegateCommand<SelectionChangedEventArgs>(Navigate, CanNavigate);
    }

    private void Navigate(System.Windows.Controls.SelectionChangedEventArgs args)
    {
        var source = SelectedItem.Tag.ToString();
        this._regionManager.RequestNavigate(RegionNames.ContentRegion, source);
    }


    private bool CanNavigate(object arg) => true;

}