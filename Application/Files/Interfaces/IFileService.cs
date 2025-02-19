using Domain.Entities;

namespace Application.Files.Interfaces;

public interface IFileService
{
    Task<FileUploaded> SaveFileAsync(string fileName, Stream fileStream, string[] permittedExtensions, string directory);
    
    Task<Stream> GetFileAsync(string fileName, string[] directory);
    
    Task<bool> DeleteFileAsync(string fileName, string[] directory);
    
    Task<string> CopyFileAsync(string sourceFileName, string targetFileName, string[] paths);

    void ReplaceTextInWord(string fileName, string[] directory, Dictionary<string, string> replacements);
    string ConvertToPdf(string fileName, string[] outputFilePath);
    Task<Stream> CreateExcelStream( List<string> headers, List<List<string>> data);

}