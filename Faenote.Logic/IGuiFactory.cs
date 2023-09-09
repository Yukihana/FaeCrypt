using Faenote.Logic.Handlers;
using Faenote.Logic.ViewContracts;

namespace Faenote.Logic;

public interface IGuiFactory
{
    // Singletons

    IMessagePromptHandler MessagePromptHandler { get; }

    IFileDialogHandler FileDialogHandler { get; }

    // Factories

    IMarqueeSpinnerSplash CreateMarqueeSpinnerSplash();

    // Dispatchers

    void Dispatch(Action dispatchAction);

    Task DispatchAsync(Action dispatchAction, CancellationToken ctoken = default);

    T Dispatch<T>(Func<T> dispatchAction);

    Task<T> DispatchAsync<T>(Func<T> dispatchAction, CancellationToken ctoken = default);
}