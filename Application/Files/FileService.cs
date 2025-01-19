using Application.Files.Interfaces;
using Application.Utils.Helpers;
using Domain.Entities;

namespace Application.Files;

public class FileService : IFileService
{
    private readonly IFileStorageService _fileStorageService;
    private readonly string[] _permittedExtensions = {".pdf" };
    
    public FileService(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }
    
    
    public async Task<FileUploaded> UploadFileAsync(string fileName, byte[] content)
    {
        //validate extension file
        if (!FileValidator.ValidateFileExtension(fileName, _permittedExtensions))
        {
            throw new ArgumentException("La extensión del archivo no es permitida.");
        }
        
        //validate file signature
        if (!FileValidator.ValidateFileSignature(new MemoryStream(content), Path.GetExtension(fileName).ToLowerInvariant()))
        {
            throw new ArgumentException("La firma del archivo no es válida.");
        }

        fileName = await _fileStorageService.SaveFileAsync(fileName, content);

        return new FileUploaded()
        {
            FileName = fileName
        };

    }

    //get file
    public async Task<byte[]> GetFileAsync(string fileName)
    {
        return await _fileStorageService.GetFileAsync(fileName);
    }
}