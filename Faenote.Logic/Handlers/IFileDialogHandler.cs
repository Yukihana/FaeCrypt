namespace Faenote.Logic.Handlers;

public partial interface IFileDialogHandler
{
    Task<string?> ShowOpenDialog(string title, CancellationToken ctoken = default);

    Task<string?> ShowSaveDialog(string title, CancellationToken ctoken = default);
}