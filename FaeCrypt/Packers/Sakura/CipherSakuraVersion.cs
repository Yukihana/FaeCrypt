using System;

namespace FaeCrypt.Packers.Sakura;

internal static class CipherSakuraVersion
{
    // Name

    public static CipherPackerName CipherPackerName => CipherPackerName.Sakura;

    // Version

    private const int VERSION_MAJOR = 0;
    private const int VERSION_MINOR = 1;
    private const int VERSION_BUILD = 0;
    private const int VERSION_REVISION = 0;

    public static Version Version => new(
        major: VERSION_MAJOR,
        minor: VERSION_MINOR,
        build: VERSION_BUILD,
        revision: VERSION_REVISION);

    public static byte[] GetBytes()
    {
        byte[] bytes = new byte[16];
        BitConverter.GetBytes(VERSION_MAJOR).CopyTo(bytes, 0);
        BitConverter.GetBytes(VERSION_MINOR).CopyTo(bytes, 4);
        BitConverter.GetBytes(VERSION_BUILD).CopyTo(bytes, 8);
        BitConverter.GetBytes(VERSION_REVISION).CopyTo(bytes, 12);
        return bytes;
    }

    // Validate

    private static ReadOnlySpan<byte> StartBytes => "FaeCrypt"u8;

    public static bool ValidateBlob(byte[] cipherText)
    {
        int cursor = 0;
        int length = StartBytes.Length;

        if (!cipherText.AsSpan(cursor, length).SequenceEqual(StartBytes))
            return false;
        cursor += length;

        byte[] version = GetBytes();
        length = version.Length;
        return cipherText.AsSpan(cursor, length).SequenceEqual(version);
    }
}