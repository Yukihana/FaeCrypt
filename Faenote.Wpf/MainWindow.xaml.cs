using Faenote.Logic.ViewContexts.MainViewContext;
using System.Threading.Tasks;
using System.Windows;

namespace Faenote.Wpf;

/// <summary>
/// Interaction logic for MainWindow.xaml
/// </summary>
public partial class MainWindow : Window
{
    public MainWindow()
    {
        InitializeComponent();

        _logic.CloseAction = CloseView;
        _logic.SetGuiFactory(new GuiFactory()
        {
            CloseAction = CloseView,
        });

        DataContext = _logic;
    }

    // Logic

    private readonly MainContextLogic _logic = new();

    // Closing

    private async void Window_Closing(object sender, System.ComponentModel.CancelEventArgs e)
    {
        if (_logic.IsClosingAllowed)
            return;
        e.Cancel = true;
        await _logic.ConfirmClose();
    }

    private async Task CloseView()
        => await Dispatcher.InvokeAsync(Close);
}