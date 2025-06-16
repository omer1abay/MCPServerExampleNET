using ModelContextProtocol.Server;
using System.ComponentModel;

namespace MCPExample;

[McpServerToolType]
public class FileSystemTool
{
    [McpServerTool, Description("Lists the files in the specified directory.")]
    public static string[] ListFiles(string directoryPath)
    {
        if (string.IsNullOrEmpty(directoryPath) || !System.IO.Directory.Exists(directoryPath))
        {
            throw new ArgumentException("Invalid directory path.", nameof(directoryPath));
        }
        return System.IO.Directory.GetFiles(directoryPath);
    }

    [McpServerTool, Description("Reads the content of a file.")]
    public static string ReadFile(string filePath)
    {
        if (string.IsNullOrEmpty(filePath) || !System.IO.File.Exists(filePath))
        {
            throw new ArgumentException("Invalid file path.", nameof(filePath));
        }
        return System.IO.File.ReadAllText(filePath);
    }

    [McpServerTool, Description("Writes content to a file.")]
    public static void WriteFile(string filePath, string content)
    {
        if (string.IsNullOrEmpty(filePath))
        {
            throw new ArgumentException("Invalid file path.", nameof(filePath));
        }
        System.IO.File.WriteAllText(filePath, content);
    }
}
