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
    
    
    public async Task<FileUploaded> SaveFileAsync(string fileName, Stream fileStream, string[] permittedExtensions, string directory)
    {
        //validate extension file
        if (!FileValidator.ValidateFileExtension(fileName, permittedExtensions))
        {
            throw new ArgumentException("La extensión del archivo no es permitida.");
        }
        
        //validate file signature
        /*if (!FileValidator.ValidateFileSignature(fileStream, Path.GetExtension(fileName).ToLowerInvariant()))
        {
            throw new ArgumentException("La firma del archivo no es válida.");
        }*/
        
        var nameGuid = Guid.NewGuid().ToString("N")+Path.GetExtension(fileName).ToLowerInvariant();
        
        var paths = new List<string> {directory};

        fileName = await _fileStorageService.SaveFileAsync(fileStream, nameGuid, paths);

        return new FileUploaded()
        {
            FileName = fileName
        };

    }

    //get file
    public async Task<Stream> GetFileAsync(string fileName, string[] directory)
    {
        return await _fileStorageService.GetFileAsync(fileName, directory);
    }

    public async Task<bool> DeleteFileAsync(string fileName, string[] directory)
    {
        return await _fileStorageService.DeleteFileAsync(fileName, directory);
    }
    
    public async Task<string> CopyFileAsync(string sourceFileName, string targetFileName, string[] paths)
    {
        return await _fileStorageService.CopyFileAsync(sourceFileName, targetFileName,paths);
    }
    
    public void ReplaceTextInWord(string fileName, string[] directory,  Dictionary<string, string> replacements)
    {
        _fileStorageService.ReplaceTextInWord(fileName, directory, replacements);
    }

    public string ConvertToPdf(string fileName, string[] outputFilePath)
    {
        return _fileStorageService.ConvertToPdf(fileName, outputFilePath);
    }

    public async Task<Stream> CreateExcelStream(List<string> headers, List<List<string>> data)
    {
        return await _fileStorageService.CreateExcelStream(headers, data);
    }
}