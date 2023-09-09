using Faenote.Logic;
using Faenote.Logic.Handlers;
using Faenote.Logic.ViewContracts;
using Faenote.Wpf.Handlers;
using Faenote.Wpf.Splashes;
using System;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Threading;

namespace Faenote.Wpf;

internal class GuiFactory : IGuiFactory
{
    // Runtime

    public Func<Task>? CloseAction { get; set; } = null;

    // Singletons

    public IMessagePromptHandler MessagePromptHandler
        => Handlers.MessagePromptHandler.Current;

    public IFileDialogHandler FileDialogHandler
        => Handlers.FileDialogHandler.Current;

    // Factories

    public IMarqueeSpinnerSplash CreateMarqueeSpinnerSplash()
        => new MarqueeSpinnerSplash();

    // Dispatch

    public void Dispatch(Action dispatchAction)
        => Dispatcher.CurrentDispatcher.Invoke(dispatchAction);

    public async Task DispatchAsync(Action dispatchAction, CancellationToken ctoken = default)
        => await Dispatcher.CurrentDispatcher.InvokeAsync(dispatchAction, DispatcherPriority.Normal, ctoken);

    public T Dispatch<T>(Func<T> dispatchAction)
        => Dispatcher.CurrentDispatcher.Invoke(dispatchAction);

    public async Task<T> DispatchAsync<T>(Func<T> dispatchAction, CancellationToken ctoken = default)
        => await Dispatcher.CurrentDispatcher.InvokeAsync(dispatchAction, DispatcherPriority.Normal, ctoken);
}