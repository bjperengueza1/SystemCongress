namespace Domain.Interfaces.Files;

public interface IFileStorageService
{
    Task<string> SaveFileAsync(Stream fileStream, string fileName, List<string> paths);
    Task<Stream> GetFileAsync(string fileName, string[] directory);
    Task<bool> DeleteFileAsync(string fileName, string[] directory);
    Task<string> CopyFileAsync(string sourceFileName, string targetFileName, string[] paths);
    string ConvertToPdf(string fileName, string[] outputFilePath);
    void ReplaceTextInWord(string fileName, string[] directory,  Dictionary<string, string> replacements);
    
    Task<Stream> CreateExcelStream( List<string> headers, List<List<string>> data);
}