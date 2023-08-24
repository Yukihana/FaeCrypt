using System;
using System.Linq;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace FaeCrypt.Packers.Sakura;

internal partial class CipherSakura
{
    private static bool TryDecryptBytes(byte[] cipherText, string password, out byte[] plainText)
    {
        // Validate structure
        plainText = Array.Empty<byte>();
        if (!CipherSakuraHeader.TryReadFrom(cipherText, out CipherSakuraHeader? header) ||
             header is null ||
             !header.ValidateStructure(cipherText))
            return false;

        // Get components
        Span<byte> notesBytes = header.GetNotesSpan(cipherText); // Useless other than when reading using a hex editor
        Span<byte> metadataSpan = header.GetMetadataSpan(cipherText);
        Span<byte> textSpan = header.GetTextSpan(cipherText);
        Span<byte> hmacSpan = header.GetHmacSpan(cipherText);
        Span<byte> payloadSpan = header.GetPayloadSpan(cipherText);

        // Deserialize metadata
        if (JsonSerializer.Deserialize<CipherSakuraMetadata>(metadataSpan) is not CipherSakuraMetadata metadata)
            return false;
        if (DebugParameters.DisplayPublicBytes)
            Console.WriteLine(Encoding.UTF8.GetString(metadataSpan));

        // Get keys
        byte[] baseKey = metadata.GenerateBaseKey(password);
        byte[] cipherKey = metadata.GetCipherKey(baseKey);
        byte[] hmacKey = metadata.GetHmacKey(baseKey);

        // Authenticate
        byte[] hmac = HMACSHA512.HashData(hmacKey, payloadSpan);
        if (!hmacSpan.SequenceEqual(hmac.AsSpan()))
            return false;

        // Decrypt
        plainText = new byte[textSpan.Length];
        int textLength = 0;
        using (var aes = Aes.Create())
        {
            aes.Key = cipherKey;
            textLength = aes.DecryptCbc(textSpan, metadata.CipherIV, plainText, metadata.CipherPadding);
        }

        // Finish
        plainText = plainText[..textLength];
        return true;
    }
}