using Faenote.Logic.Handlers;
using Faenote.Wpf.Dialogs;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Faenote.Wpf.Handlers;

internal class PasswordDialogHandler : IPasswordDialogHandler
{
    public async Task<string?> ShowAsync(string message, CancellationToken ctoken = default)
    {
        return await Dispatcher.CurrentDispatcher.InvokeAsync(() => Show(message), DispatcherPriority.Normal, ctoken);
    }

    public string? Show(string message)
    {
        PasswordDialog pbox = new()
        {
            Message = message
        };
        if (pbox.ShowDialog() is not true)
            return null;

        return pbox.Password;
    }
}