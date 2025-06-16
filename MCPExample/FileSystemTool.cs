using ModelContextProtocol.Server;
using System.ComponentModel;
using System.Diagnostics;
using System.Security.Principal;

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

    [McpServerTool, Description("Search specified file in the disk and return the paths you find")]
    public static List<string> ScanFilesAsync(string fileName, string searchPath = "C://")
    {
        var foundFiles = new List<string>();

        void ScanDirectory(string path)
        {
            try
            {
                // Dosyaları tara
                foreach (var file in Directory.GetFiles(path, fileName))
                {
                    foundFiles.Add(file); // Bulunan dosyayı listeye ekle
                }

                // Alt klasörleri tara
                foreach (var directory in Directory.GetDirectories(path))
                {
                    try
                    {
                        ScanDirectory(directory); // Özyinelemeli tarama
                    }
                    catch (UnauthorizedAccessException)
                    {

                    }
                    catch (Exception ex)
                    {

                    }
                }
            }
            catch (UnauthorizedAccessException)
            {

            }
            catch (Exception ex)
            {

            }
        }

        ScanDirectory(searchPath);
        return foundFiles;
    }
}