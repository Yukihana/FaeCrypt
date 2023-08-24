using System;

namespace FaeCrypt;

public interface ICipherPacker
{
    CipherPackerName CipherPackerName { get; }

    Version Version { get; }

    byte[] Encrypt(byte[] plainText, string password);

    bool TryDecrypt(byte[] cipherText, string password, out byte[] plainText);
}