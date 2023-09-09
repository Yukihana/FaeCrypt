using Faenote.Logic.Handlers;
using System.Threading;
using System.Threading.Tasks;

namespace Faenote.Wpf.Handlers;

internal class FileDialogHandler : IFileDialogHandler
{
    // Singleton

    private static IFileDialogHandler? _current = null;

    public static IFileDialogHandler Current
        => _current ??= new FileDialogHandler();

    // Contract

    public async Task<string?> ShowOpenDialog(string title, CancellationToken ctoken = default)
    {
        await Task.Yield();
        return string.Empty;
    }

    public async Task<string?> ShowSaveDialog(string title, CancellationToken ctoken = default)
    {
        await Task.Yield();
        return string.Empty;
    }
}