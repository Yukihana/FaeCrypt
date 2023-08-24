using System;

namespace FaeCrypt.Packers.Sakura;

internal class CipherSakuraHeader
{
    public const int HEADER_LENGTH = 64;
    public const int VERSION_POSITION = 16;
    public const int NOTES_LENGTH_POSITION = 32;
    public const int METADATA_LENGTH_POSITION = 36;
    public const int TEXT_LENGTH_POSITION = 40;

    public static ReadOnlySpan<byte> BRANDING_BYTES => "FaeCrypt"u8;
    public int NotesLength { get; set; } = 0;
    public int MetadataLength { get; set; } = 0;
    public int TextLength { get; set; } = 0;

    public byte[] ToBytes()
    {
        byte[] result = new byte[64];
        BRANDING_BYTES.CopyTo(result);
        CipherSakuraVersion.GetBytes().CopyTo(result, 16);
        BitConverter.GetBytes(NotesLength).CopyTo(result, 32);
        BitConverter.GetBytes(MetadataLength).CopyTo(result, 36);
        BitConverter.GetBytes(TextLength).CopyTo(result, 40);
        return result;
    }

    public static bool TryReadFrom(byte[] blob, out CipherSakuraHeader? header)
    {
        header = null;
        if (blob.Length < HEADER_LENGTH)
            return false;

        // Validate branding
        if (!blob.AsSpan(0, BRANDING_BYTES.Length).SequenceEqual(BRANDING_BYTES))
            return false;

        // Validate version
        byte[] versionBytes = CipherSakuraVersion.GetBytes();
        if (!blob.AsSpan(VERSION_POSITION, versionBytes.Length).SequenceEqual(versionBytes))
            return false;

        header = new CipherSakuraHeader()
        {
            NotesLength = BitConverter.ToInt32(blob.AsSpan(NOTES_LENGTH_POSITION, 4)),
            MetadataLength = BitConverter.ToInt32(blob.AsSpan(METADATA_LENGTH_POSITION, 4)),
            TextLength = BitConverter.ToInt32(blob.AsSpan(TEXT_LENGTH_POSITION, 4)),
        };
        return true;
    }
}