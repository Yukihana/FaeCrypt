namespace Faenote.Logic.Handlers;

public interface IPasswordDialogHandler
{
    Task<string?> ShowAsync(string message, CancellationToken ctoken = default);

    string? Show(string message);
}