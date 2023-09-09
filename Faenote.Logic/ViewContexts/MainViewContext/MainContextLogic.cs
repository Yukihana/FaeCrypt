using CommunityToolkit.Mvvm.ComponentModel;
using CommunityToolkit.Mvvm.Input;

namespace Faenote.Logic.ViewContexts.MainViewContext;

public partial class MainContextLogic : ObservableObject
{
    // Data

    [ObservableProperty]
    private MainContextData _contextData = new();

    // Initialization

    public MainContextLogic()
    {
        // Commands
        DefaultCommand = new(ParseDefaultCommand);
    }

    // Gui Factory

    private IGuiFactory? _guiFactory = null;

    internal IGuiFactory GuiFactory
        => _guiFactory
        ?? throw new NullReferenceException($"{nameof(GuiFactory)} must be assigned at startup.");

    public void SetGuiFactory(IGuiFactory factory)
        => _guiFactory = factory;

    // Closing

    public bool IsClosingAllowed { get; set; } = false;
    public Func<Task>? CloseAction { get; set; } = null;

    // Default command

    [ObservableProperty]
    private RelayCommand<string> _defaultCommand;
}