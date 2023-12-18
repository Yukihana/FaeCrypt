using System;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace FAegis;

internal class EncryptorWrapper
{
    public static async Task<byte[]> GetRawFileData(string filePath, CancellationToken ctoken = default)
    {
        using MemoryStream ms = new();

        string filename = Path.GetFileName(filePath);
        byte[] fileNameBytes = Encoding.UTF8.GetBytes(filename);
        ms.Write(BitConverter.GetBytes(fileNameBytes.Length));
        ms.Write(fileNameBytes, 0, fileNameBytes.Length);
        using FileStream fs = new(filePath, FileMode.Open, FileAccess.Read);
        await fs.CopyToAsync(ms, ctoken);

        fs.Close();
        return ms.ToArray();
    }
}