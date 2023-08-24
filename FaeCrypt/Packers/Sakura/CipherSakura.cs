using System;

namespace FaeCrypt.Packers.Sakura;

/// <summary>
/// Version: 0.1.0.0 (Prototype)
/// </summary>
internal partial class CipherSakura : ICipherPacker
{
    // Properties

    public CipherPackerName CipherPackerName
        => CipherSakuraVersion.CipherPackerName;

    public Version Version
        => CipherSakuraVersion.Version;

    // Methods

    public byte[] Encrypt(byte[] plainText, string password)
        => EncryptBytes(plainText, password);

    public bool TryDecrypt(byte[] cipherText, string password, out byte[] plainText)
        => TryDecryptBytes(cipherText, password, out plainText);
}