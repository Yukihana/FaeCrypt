namespace Faenote.Logic.Handlers;

public interface IMarqueeSpinnerSplashHandler
{
    Task ShowAsync(string message, CancellationToken ctoken = default);
}