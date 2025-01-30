using Domain.Entities;

namespace Application.Files.Interfaces;

public interface IFileService
{
    Task<FileUploaded> SaveFileAsync(string fileName, byte[] content, string[] permittedExtensions, string directory);
    
    Task<Stream> GetFileAsync(string fileName, string directory);
    
    Task<bool> DeleteFileAsync(string fileName, string directory);

}