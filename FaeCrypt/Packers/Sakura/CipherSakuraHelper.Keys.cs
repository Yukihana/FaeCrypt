using System.Security.Cryptography;
using System.Text;

namespace FaeCrypt.Packers.Sakura;

internal static partial class CipherSakuraHelper
{
    public static byte[] GenerateBaseKey(this CipherSakuraMetadata metadata, string password) => Rfc2898DeriveBytes.Pbkdf2(
        password: Encoding.UTF8.GetBytes(password),
        salt: metadata.Pbkdf2Salt,
        iterations: metadata.Pbkdf2Iterations,
        hashAlgorithm: HashAlgorithmName.FromOid(metadata.Pbkdf2Algorithm),
        outputLength: metadata.Pbkdf2OutputSize);

    public static byte[] GetCipherKey(this CipherSakuraMetadata metadata, byte[] baseKey) => HKDF.DeriveKey(
        hashAlgorithmName: HashAlgorithmName.FromOid(metadata.CipherKeyAlgorithm),
        ikm: baseKey,
        outputLength: metadata.CipherKeySize,
        salt: metadata.CipherKeySalt,
        info: metadata.CipherKeyInfo);

    public static byte[] GetHmacKey(this CipherSakuraMetadata metadata, byte[] baseKey) => HKDF.DeriveKey(
        hashAlgorithmName: HashAlgorithmName.FromOid(metadata.HmacKeyAlgorithm),
        ikm: baseKey,
        outputLength: metadata.HmacKeySize,
        salt: metadata.HmacKeySalt,
        info: metadata.HmacKeyInfo);
}