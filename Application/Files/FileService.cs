using Application.Files.Interfaces;
using Application.Utils.Helpers;
using Domain.Entities;
using Domain.Interfaces.Files;

namespace Application.Files;

public class FileService : IFileService
{
    private readonly IFileStorageService _fileStorageService;
    public FileService(IFileStorageService fileStorageService)
    {
        _fileStorageService = fileStorageService;
    }
    
    
    public async Task<FileUploaded> SaveFileAsync(string fileName, byte[] content, string[] permittedExtensions, string directory)
    {
        //validate extension file
        if (!FileValidator.ValidateFileExtension(fileName, permittedExtensions))
        {
            throw new ArgumentException("La extensión del archivo no es permitida.");
        }
        
        //validate file signature
        if (!FileValidator.ValidateFileSignature(new MemoryStream(content), Path.GetExtension(fileName).ToLowerInvariant()))
        {
            throw new ArgumentException("La firma del archivo no es válida.");
        }

        fileName = await _fileStorageService.SaveFileAsync(new MemoryStream(content), fileName, directory );

        return new FileUploaded()
        {
            FileName = fileName
        };

    }

    //get file
    public async Task<Stream> GetFileAsync(string fileName, string directory)
    {
        return await _fileStorageService.GetFileAsync(fileName, directory);
    }

    public async Task<bool> DeleteFileAsync(string fileName, string directory)
    {
        return await _fileStorageService.DeleteFileAsync(fileName, directory);
    }
    
    public async Task<string> CopyFileAsync(string sourceFileName, string targetFileName, string directory)
    {
        return await _fileStorageService.CopyFileAsync(sourceFileName, targetFileName, directory);
    }
    
    public void ReplaceTextInWord(string fileName, string directory, string placeholder, string replacementText)
    {
        _fileStorageService.ReplaceTextInWord(fileName, directory, placeholder, replacementText);
    }

    public void ConvertToPdf(string inputFilePath, string outputFilePath)
    {
        _fileStorageService.ConvertToPdf(inputFilePath, outputFilePath);
    }
}