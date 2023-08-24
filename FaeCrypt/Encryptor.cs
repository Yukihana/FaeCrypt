using FaeCrypt.Helpers;
using FaeCrypt.Packers.Sakura;
using System;
using System.Collections.Generic;
using System.Collections.Immutable;
using System.Linq;
using System.Text;
using System.Text.Json;

namespace FaeCrypt;

public static class Encryptor
{
    public static CipherPackerName DefaultMethod { get; set; } = CipherPackerName.Sakura;

    // Versions

    internal static readonly ImmutableDictionary<CipherPackerName, ICipherPacker> VersionTable = new Dictionary<CipherPackerName, ICipherPacker>()
    {
        { CipherPackerName.Sakura, new CipherSakura() },
    }.ToImmutableDictionary();

    public static ICipherPacker? GetEncryptor(this CipherPackerName name)
    {
        if (VersionTable.TryGetValue(name, out ICipherPacker? packer))
            return packer;
        return null;
    }

    // Primary

    public static byte[] Encrypt(byte[] plainText, string password, CipherPackerName method = CipherPackerName.Auto)
    {
        if (method == CipherPackerName.Auto)
            method = DefaultMethod;

        if (method.GetEncryptor() is not ICipherPacker encryptor)
            throw new InvalidOperationException("Invalid encryptor");

        return encryptor.Encrypt(plainText, password);
    }

    public static bool TryDecrypt(byte[] cipherText, string password, out byte[] plainText, CipherPackerName packMethod = CipherPackerName.Auto)
    {
        // Auto
        if (packMethod == CipherPackerName.Auto)
        {
            foreach (var version in Enum.GetValues(typeof(CipherPackerName)).Cast<CipherPackerName>())
            {
                if (version != CipherPackerName.Auto &&
                    GetEncryptor(version) is ICipherPacker packer &&
                    packer.TryDecrypt(cipherText, password, out plainText))
                {
                    return true;
                }
            }
        }

        // Specified
        else if (GetEncryptor(packMethod) is ICipherPacker packer)
            return packer.TryDecrypt(cipherText, password, out plainText);

        // Unknown
        plainText = Array.Empty<byte>();
        return false;
    }

    // Secondary

    public static byte[] Encrypt(string plainText, string password, CipherPackerName method = CipherPackerName.Auto)
        => Encrypt(Encoding.UTF8.GetBytes(plainText), password, method);

    public static byte[] Encrypt<T>(T obj, string password, CipherPackerName method = CipherPackerName.Sakura)
        => Encrypt(JsonSerializer.Serialize(obj), password, method);

    public static bool TryDecrypt(byte[] cipherText, string password, out string plainText, CipherPackerName forceMethod = CipherPackerName.Auto)
    {
        bool success = TryDecrypt(cipherText, password, out byte[] result, forceMethod);
        plainText = success ? Encoding.UTF8.GetString(result) : string.Empty;
        return success;
    }

    public static bool TryDecrypt<T>(byte[] cipherText, string password, out T? obj, CipherPackerName forceMethod = CipherPackerName.Auto)
    {
        if (TryDecrypt(cipherText, password, out string result, forceMethod) &&
            result.TryDeserializeJson(out obj))
            return true;
        obj = default;
        return false;
    }
}