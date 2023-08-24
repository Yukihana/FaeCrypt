using System;
using System.Security.Cryptography;
using System.Text;

namespace FaeCrypt.Packers.Sakura;

[Serializable]
public class CipherSakuraMetadata
{
    // Fixed

    public string Pbkdf2Algorithm { get; set; } = CipherSakuraConfig.PBKDF2_OID;
    public int Pbkdf2SaltSize { get; set; } = CipherSakuraConfig.PBKDF2_SALT_SIZE;
    public int Pbkdf2OutputSize { get; set; } = CipherSakuraConfig.PBKDF2_OUTPUT_SIZE;

    public string CipherKeyAlgorithm { get; set; } = CipherSakuraConfig.CIPHER_KEY_OID;
    public int CipherKeySize { get; set; } = CipherSakuraConfig.CIPHER_KEY_SIZE;
    public byte[] CipherKeyInfo { get; set; } = Encoding.UTF8.GetBytes(CipherSakuraConfig.CIPHER_KEY_INFO);

    public string HmacKeyAlgorithm { get; set; } = CipherSakuraConfig.HMAC_KEY_OID;
    public int HmacKeySize { get; set; } = CipherSakuraConfig.HMAC_KEY_SIZE;
    public byte[] HmacKeyInfo { get; set; } = Encoding.UTF8.GetBytes(CipherSakuraConfig.HMAC_KEY_INFO);

    public int CipherIVSize { get; set; } = CipherSakuraConfig.CIPHER_IV_SIZE;
    public PaddingMode CipherPadding { get; set; } = CipherSakuraConfig.CIPHER_ENCRYPTION_PADDING;

    // Generated

    public byte[] Pbkdf2Salt { get; set; } = Array.Empty<byte>();
    public int Pbkdf2Iterations { get; set; } = 5_000_000;

    public byte[] CipherKeySalt { get; set; } = Array.Empty<byte>();
    public byte[] CipherIV { get; set; } = Array.Empty<byte>();

    public byte[] HmacKeySalt { get; set; } = Array.Empty<byte>();
}