// See https://aka.ms/new-console-template for more information

using FaeCrypt;
using System.Text;

Console.Write("Enter some text to encrypt: ");
string inputText = Console.ReadLine() ?? string.Empty;
Console.Write("Enter the password: ");
string password = Console.ReadLine() ?? string.Empty;
Console.WriteLine();

Console.Write("Encrypting... ");
byte[] cipherText = Encryptor.Encrypt(inputText, password);
Console.WriteLine("completed.\n");

string outputText;
Console.Write("Decrypting... ");
bool success = Encryptor.TryDecrypt(cipherText, password, out byte[] plainText);
Console.WriteLine(success ? "completed." : "failed.");
if (!success)
    return;
Console.WriteLine();

Console.Write("Verifying... ");
outputText = Encoding.UTF8.GetString(plainText);
success = inputText == outputText;
Console.WriteLine(success ? "completed." : "failed.");
if (!success)
    return;
Console.WriteLine();

Console.WriteLine("Cipher text:");
HexOutput(cipherText);

static void HexOutput(byte[] data, int octabytesPerLine = 4)
{
    int i = 0;
    int j = 0;

    while (i + 8 < data.Length)
    {
        Console.Write(BitConverter.ToString(data.AsSpan(i, 8).ToArray()).Replace("-", " "));
        i += 8;

        j++;
        if (j % octabytesPerLine == 0)
            Console.WriteLine();
        else
            Console.Write(" - ");
    }
    Console.WriteLine(BitConverter.ToString(data[i..]));
}