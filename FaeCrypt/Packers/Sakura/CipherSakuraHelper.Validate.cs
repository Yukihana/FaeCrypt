using System.Security.Cryptography;

namespace FaeCrypt.Packers.Sakura;

internal static partial class CipherSakuraHelper
{
    public static bool ValidateStructure(this CipherSakuraHeader header, byte[] blob)
    {
        int expectedLength
            = CipherSakuraHeader.HEADER_LENGTH
            + header.NotesLength
            + header.MetadataLength
            + header.TextLength
            + HMACSHA512.HashSizeInBytes;

        return blob.Length >= expectedLength;
    }
}