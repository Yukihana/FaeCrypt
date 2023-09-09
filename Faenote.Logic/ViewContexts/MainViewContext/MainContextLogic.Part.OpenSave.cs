using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Faenote.Logic.ViewContexts.MainViewContext;

public partial class MainContextLogic
{
    public async Task Open(CancellationToken ctoken = default)
    {
        // Take inputs
        if (await ShowFileOpener("Select file to decrypt and open...", ctoken).ConfigureAwait(false) is not string path)
            return;
        if (await ShowPasswordInput("Enter the password for decryption...", ctoken).ConfigureAwait(false) is not string password)
            return;

        // Decrypt
        if (await Decrypt(path, password, ctoken).ConfigureAwait(false) is not string text)
        {
            await ShowMessagePrompt(
                message: "Failed to decrypt...",
                ctoken: ctoken)
                .ConfigureAwait(false);
            return;
        }

        // Update UI
        await GuiFactory.DispatchAsync(() =>
        {
            ContextData.Path = path;
            ContextData.ContentText = text;
        }, ctoken).ConfigureAwait(false);
    }

    private async Task<string?> Decrypt(string path, string password, CancellationToken ctoken = default)
    {
        using CancellationTokenSource cts = new();
        Task spinner = ShowMarqueeSpinnerSplash("Decrypting...", cts.Token);

        try
        {
            // Read
            byte[] cipherText = await File.ReadAllBytesAsync(path, cancellationToken: ctoken).ConfigureAwait(true);

            // Decryption process
            byte[]? plainText = await Task.Run(() =>
            {
                bool success = FaeCrypt.Encryptor.TryDecrypt(cipherText, password, out byte[] outputText);
                return success ? outputText : null;
            }, ctoken).ConfigureAwait(true);

            // Decode Utf8
            string contentText = string.Empty;
            if (plainText is not null)
                contentText = await Task.Run(() => Encoding.UTF8.GetString(plainText), cancellationToken: ctoken).ConfigureAwait(true);

            return contentText;
        }
        finally
        {
            cts.Cancel();
            await spinner.ConfigureAwait(false);
        }
    }

    // Save

    private static async Task<bool> SaveThis(string data, string password, string path)
    {
        try
        {
            byte[] cipherText = await Task.Run(() =>
            {
                var encoded = Encoding.UTF8.GetBytes(data);
                return FaeCrypt.Encryptor.Encrypt(encoded, password);
            }).ConfigureAwait(false);

            await File.WriteAllBytesAsync(path, cipherText).ConfigureAwait(false);
            return true;
        }
        catch { return false; }
    }
}