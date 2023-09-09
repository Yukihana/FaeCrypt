using Faenote.Logic.Handlers;

namespace Faenote.Logic.ViewContexts.MainViewContext;

public partial class MainContextLogic
{
    // Message box handler

    public IMessagePromptHandler? MessageBoxHandler { get; set; } = null;

    private async Task<string?> ShowMessagePrompt(
        string message,
        string caption = "Information",
        ICollection<string>? buttons = null,
        string? image = null,
        string defaultButtonText = "Confirm",
        string cancelButtonText = "Cancel",
        CancellationToken ctoken = default)
    {
        if (MessageBoxHandler is null)
            throw new InvalidOperationException($"{nameof(MessageBoxHandler)} hasn't been assigned.");

        return await MessageBoxHandler.ShowAsync(
            message: message,
            caption: caption,
            buttons: buttons,
            image: image,
            defaultButtonText: defaultButtonText,
            cancelButtonText: cancelButtonText,
            ctoken: ctoken)
            .ConfigureAwait(false);
    }

    // File dialog handler

    public IFileDialogHandler? FileDialogHandler { get; set; } = null;

    private async Task<string?> ShowFileOpener(string title, CancellationToken ctoken = default)
    {
        if (FileDialogHandler is null)
            throw new InvalidOperationException($"{nameof(FileDialogHandler)} hasn't been assigned.");

        return await FileDialogHandler.ShowOpenDialog(
            title: title,
            ctoken: ctoken)
            .ConfigureAwait(false);
    }

    // Password dialog handler

    public IPasswordDialogHandler? PasswordDialogHandler { get; set; } = null;

    private async Task<string?> ShowPasswordInput(string message, CancellationToken ctoken = default)
    {
        if (PasswordDialogHandler is null)
            throw new InvalidOperationException($"{nameof(PasswordDialogHandler)} hasn't been assigned.");

        return await PasswordDialogHandler.ShowAsync(
            message: message,
            ctoken: ctoken)
            .ConfigureAwait(false);
    }

    // Spinner splash handler

    public IMarqueeSpinnerSplashHandler? MarqueeSpinnerSplashHandler { get; set; } = null;

    private async Task ShowMarqueeSpinnerSplash(string message, CancellationToken ctoken = default)
    {
        if (MarqueeSpinnerSplashHandler is null)
            throw new InvalidOperationException($"{nameof(MarqueeSpinnerSplashHandler)} hasn't been assigned.");

        await MarqueeSpinnerSplashHandler
            .ShowAsync(message, ctoken)
            .ConfigureAwait(false);
    }
}