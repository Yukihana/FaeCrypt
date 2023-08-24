using System;
using System.Security.Cryptography;

namespace FaeCrypt.Packers.Sakura;

internal static partial class CipherSakuraHelper
{
    public static Span<byte> GetNotesSpan(this CipherSakuraHeader header, byte[] cipherText)
    {
        int start = CipherSakuraHeader.HEADER_LENGTH;
        return cipherText.AsSpan(start, header.NotesLength);
    }

    public static Span<byte> GetMetadataSpan(this CipherSakuraHeader header, byte[] cipherText)
    {
        int start = CipherSakuraHeader.HEADER_LENGTH
            + header.NotesLength;
        return cipherText.AsSpan(start, header.MetadataLength);
    }

    public static Span<byte> GetTextSpan(this CipherSakuraHeader header, byte[] cipherText)
    {
        int start = CipherSakuraHeader.HEADER_LENGTH
            + header.NotesLength
            + header.MetadataLength;
        return cipherText.AsSpan(start, header.TextLength);
    }

    public static Span<byte> GetHmacSpan(this CipherSakuraHeader header, byte[] cipherText)
    {
        int start = CipherSakuraHeader.HEADER_LENGTH
            + header.NotesLength
            + header.MetadataLength
            + header.TextLength;
        return cipherText.AsSpan(start, HMACSHA512.HashSizeInBytes);
    }

    public static Span<byte> GetPayloadSpan(this CipherSakuraHeader header, byte[] cipherText)
    {
        int length = CipherSakuraHeader.HEADER_LENGTH
            + header.NotesLength
            + header.MetadataLength
            + header.TextLength;
        return cipherText.AsSpan(0, length);
    }
}