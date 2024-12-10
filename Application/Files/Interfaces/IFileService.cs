using Domain.Entities;

namespace Application.Files.Interfaces;

public interface IFileService
{
    Task<FileUploaded> UploadFileAsync(string fileName, byte[] content);

}