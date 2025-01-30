namespace Domain.Interfaces.Files;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName, string directory);
    Task<Stream> GetFileAsync(string fileName, string directory);
    Task<bool> DeleteFileAsync(string fileName, string directory);
    Task<string> CopyFileAsync(string sourceFileName, string targetFileName, string directory);
    void ConvertToPdf(string inputFilePath, string outputFilePath);
    void ReplaceTextInWord(string fileName, string directory, string placeholder, string replacementText);
}