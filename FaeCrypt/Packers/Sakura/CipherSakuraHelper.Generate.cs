using System.Security.Cryptography;

namespace FaeCrypt.Packers.Sakura;

internal static partial class CipherSakuraHelper
{
    public static void Randomize(this CipherSakuraMetadata metadata)
    {
        metadata.Pbkdf2Salt = new byte[metadata.Pbkdf2SaltSize];
        RandomNumberGenerator.Fill(metadata.Pbkdf2Salt);

        metadata.Pbkdf2Iterations = RandomNumberGenerator.GetInt32(
            CipherSakuraConfig.PBKDF_ITERATIONS_MIN,
            CipherSakuraConfig.PBKDF_ITERATIONS_MAX);

        metadata.CipherIV = new byte[metadata.CipherIVSize];
        RandomNumberGenerator.Fill(metadata.CipherIV);
    }
}