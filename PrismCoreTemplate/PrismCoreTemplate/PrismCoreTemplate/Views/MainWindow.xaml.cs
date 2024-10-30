using System.Windows;
using MaterialDesignThemes.Wpf;
using System.Windows.Controls.Primitives;

namespace PrismCoreTemplate.Views;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();
    }

    private void MenuToggleButton_OnClick(object sender, RoutedEventArgs e)
        => DemoItemsSearchBox.Focus();

}