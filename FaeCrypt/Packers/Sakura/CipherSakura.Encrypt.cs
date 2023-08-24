using System;
using System.Security.Cryptography;
using System.Text;
using System.Text.Json;

namespace FaeCrypt.Packers.Sakura;

internal partial class CipherSakura
{
    private static byte[] EncryptBytes(byte[] plainText, string password)
    {
        // Create metadata and cache the base key
        CipherSakuraMetadata metadata = new();
        metadata.Randomize();
        byte[] baseKey = metadata.GenerateBaseKey(password);

        // Process text
        byte[] cipherText;
        using (var encryptor = Aes.Create())
        {
            encryptor.Key = metadata.GetCipherKey(baseKey);
            cipherText = encryptor.EncryptCbc(
                plaintext: plainText,
                iv: metadata.CipherIV,
                paddingMode: metadata.CipherPadding);
        };

        // Process metadata
        string metadataJson = JsonSerializer.Serialize(metadata);
        if (DebugParameters.DisplayPublicBytes)
            Console.WriteLine(metadataJson);

        // Process components
        byte[] metadataBytes = Encoding.UTF8.GetBytes(metadataJson);
        byte[] notes = CipherSakuraNotes.GetNotes();
        byte[] header = new CipherSakuraHeader()
        {
            NotesLength = notes.Length,
            MetadataLength = metadataBytes.Length,
            TextLength = cipherText.Length
        }.ToBytes();

        // Allocate blob
        int blobLength = header.Length
            + notes.Length
            + metadataBytes.Length
            + cipherText.Length
            + HMACSHA512.HashSizeInBytes;
        byte[] blob = new byte[blobLength];

        // Copy all but HMAC
        header.CopyTo(blob, 0);
        int cursor = header.Length;
        notes.CopyTo(blob, cursor);
        cursor += notes.Length;
        metadataBytes.CopyTo(blob, cursor);
        cursor += metadataBytes.Length;
        cipherText.CopyTo(blob, cursor);
        cursor += cipherText.Length;

        // Process HMAC
        byte[] hmacKey = metadata.GetHmacKey(baseKey);
        var blobWithoutHmac = blob.AsSpan(0, blob.Length - HMACSHA512.HashSizeInBytes);
        byte[] hmac = HMACSHA512.HashData(hmacKey, blobWithoutHmac);
        hmac.CopyTo(blob, cursor);

        return blob;
    }
}