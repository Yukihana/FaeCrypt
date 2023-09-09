using CommunityToolkit.Mvvm.ComponentModel;

namespace Faenote.Logic.ViewContexts.MainViewContext;

public partial class MainContextData : ObservableObject
{
    [ObservableProperty]
    private string _contentText = string.Empty;

    [ObservableProperty]
    private string _path = string.Empty;
}