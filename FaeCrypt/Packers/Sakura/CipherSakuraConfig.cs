using System.Security.Cryptography;

namespace FaeCrypt.Packers.Sakura;

internal static class CipherSakuraConfig
{
    // Generate time only

    public const int PBKDF_ITERATIONS_MIN = 5_000_000;
    public const int PBKDF_ITERATIONS_MAX = 10_000_000;

    // Associated data

    public const string PBKDF2_OID = "2.16.840.1.101.3.4.2.3";
    public const int PBKDF2_SALT_SIZE = 16;
    public const int PBKDF2_OUTPUT_SIZE = 64;

    public const string CIPHER_KEY_OID = "2.16.840.1.101.3.4.2.3";
    public const string CIPHER_KEY_INFO = "cipher";
    public const int CIPHER_KEY_SIZE = 32;

    public const string HMAC_KEY_OID = "2.16.840.1.101.3.4.2.3";
    public const string HMAC_KEY_INFO = "hmac";
    public const int HMAC_KEY_SIZE = 64;

    public const int CIPHER_IV_SIZE = 16;
    public static PaddingMode CIPHER_ENCRYPTION_PADDING = PaddingMode.PKCS7;
}