using Faenote.Logic.Handlers;
using Faenote.Wpf.Dialogs;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Faenote.Wpf.Handlers;

internal class MessagePromptHandler : IMessagePromptHandler
{
    public static IMessagePromptHandler Current { get; internal set; }

    public async Task<string?> ShowAsync(
        string message,
        string caption = "Dialog",
        ICollection<string>? buttons = null,
        string? image = null,
        string defaultButtonText = "Confirm",
        string cancelButtonText = "Cancel",
        CancellationToken ctoken = default)
    {
        return await Dispatcher.CurrentDispatcher.InvokeAsync(() => Show(
            message: message,
            caption: caption,
            buttons: buttons,
            image: image,
            defaultButtonText: defaultButtonText,
            cancelButtonText: cancelButtonText),

            priority: DispatcherPriority.Normal,
            cancellationToken: ctoken);
    }

    public string? Show(
        string message,
        string caption = "Dialog",
        ICollection<string>? buttons = null,
        string? image = null,
        string defaultButtonText = "Confirm",
        string cancelButtonText = "Cancel")
    {
        MessageDialog md = new()
        {
            Message = message,
            Title = caption,
            Buttons = buttons,
            DefaultButtonText = defaultButtonText,
            CancelButtonText = cancelButtonText
        };

        md.SetImage(image);

        return md.ShowDialog() is true
            ? md.UserResult
            : null;
    }
}