namespace Faenote.Logic.Handlers;

public interface IMessagePromptHandler
{
    Task<string?> ShowAsync(
        string message,
        string caption = "Dialog",
        ICollection<string>? buttons = null,
        string? image = null,
        string defaultButtonText = "Confirm",
        string cancelButtonText = "Cancel",
        CancellationToken ctoken = default);

    string? Show(
        string message,
        string caption = "Dialog",
        ICollection<string>? buttons = null,
        string? image = null,
        string defaultButtonText = "Confirm",
        string cancelButtonText = "Cancel");
}