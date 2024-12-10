namespace Application.Files.Interfaces;

public interface IFileStorageService
{
    //task to save a file
    Task<string> SaveFileAsync(string fileName, byte[] content);
}