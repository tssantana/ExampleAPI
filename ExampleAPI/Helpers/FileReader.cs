namespace ExampleAPI.Helpers;

public sealed class FileReader
{
    public static string ReadContentFile(string fileName)
    {
        // AppContext.BaseDirectory é mais moderno e preferível para .NET Core/.NET 5+
        // AppDomain.CurrentDomain.BaseDirectory funciona em .NET Framework e Core
        string baseDirectory = AppContext.BaseDirectory;
        string filePath = Path.Combine(baseDirectory, fileName);

        if (File.Exists(filePath))
        {
            return File.ReadAllText(filePath);
        }
        else
        {
            return null;
        }
    }
}