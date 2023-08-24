using System;
using System.Text;

namespace FaeCrypt.Packers.Sakura;

internal static class CipherSakuraNotes
{
    public static string[] DEFAULT_NOTES => new string[]
    {
        "[Structure Info]",
        "16bytes => Branding.",
        "16bytes => Version.",
        "4bytes => Notes Length.",
        "4bytes => Metadata Json Utf8 Length.",
        "4bytes => CipherText Length.",
        "Till 64 => Padding, Any.",
        ". => Notes.",
        ". => Metadata.",
        ". => CipherText.",
        ". => HMAC.",
        "[Methods]",
        "KeyDerivation: Pbkdf2 then HKDF",
        "SHA-512 OID: 2.16.840.1.101.3.4.2.3",
    };

    public static byte[] GetNotes()
    {
        string concatenated = string.Join(Environment.NewLine, DEFAULT_NOTES);
        return Encoding.UTF8.GetBytes(concatenated);
    }
}