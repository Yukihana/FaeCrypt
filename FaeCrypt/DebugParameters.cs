namespace FaeCrypt;

public static class DebugParameters
{
    /// <summary>
    /// Print public data in the console. (Like: IV, salt, etc.)
    /// </summary>
    public static bool DisplayPublicBytes { get; set; } = false;

    /// <summary>
    /// Print private data in the console. (Warning: unsafe exposure)
    /// </summary>
    public static bool DisplayPrivateBytes { get; set; } = true;
}