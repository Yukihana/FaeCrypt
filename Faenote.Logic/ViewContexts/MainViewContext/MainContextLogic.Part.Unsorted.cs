using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faenote.Logic.ViewContexts.MainViewContext;

public partial class MainContextLogic
{
    private static string AcceptButtonText
        => "OK";

    private static string CancelButtonText
        => "Cancel";

    public async Task ConfirmClose()
    {
        string? result = await ShowMessagePrompt(
            message: "Unsaved data may be lost. Continue with new file?",
            caption: "Confirmation",
            buttons: new string[] { "OK", "Cancel" },
            image: string.Empty,
            defaultButtonText: "OK",
            cancelButtonText: "Cancel")
            .ConfigureAwait(false);

        if (result != AcceptButtonText)
            return;

        IsClosingAllowed = true;
        await CloseAction().ConfigureAwait(false);
    }

    // Legacy

    public void ValidateStartup()
    {
        throw new NotImplementedException();
    }

    private bool CloseCurrent()
    {
        if (!_logic.Unsaved)
            return true;

        if (string.IsNullOrEmpty(Path) && string.IsNullOrEmpty(TextContent.Text))
            return true;

        return MessageBox.Show(
            "Unsaved data may be lost. Continue with new file?",
            "Confirmation",
            MessageBoxButton.YesNo,
            MessageBoxImage.Asterisk)
            == MessageBoxResult.Yes;
    }

    // Legacy code

    private bool QuickMode { get; set; } = false;
    private string Password { get; set; } = string.Empty;
    private string Path { get; set; } = string.Empty;
    private bool Unsaved { get; set; } = false;

    private void LoadNew()
    {
        if (!CloseCurrent())
            return;
        TextContent.Clear();
        Path = string.Empty;
    }
}