using Domain.Interfaces.Files;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Files;

public class LocalFileStorageService : IFileStorageService
{
    private readonly FileStorageSettings _settings;
    
    public LocalFileStorageService(IOptions<FileStorageSettings> options)
    {
        _settings = options.Value;
    }

    private readonly string _basePath = "wwwroot/uploads/";

    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, string directory)
    {
        var folderPath = Path.Combine(_settings.BasePath, directory);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath); // Asegura que la carpeta exista
            //permisos de lectura y escritura
        }
        
        var filePath = Path.Combine(folderPath, fileName);

        await using var fileStreamOutput = new FileStream(filePath, FileMode.Create);
        await fileStream.CopyToAsync(fileStreamOutput);

        return fileName; // Retorna la ruta almacenada
    }

    public async Task<Stream> GetFileAsync(string fileName, string directory)
    {
        var filePath = Path.Combine(_settings.BasePath, directory, fileName);
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Archivo no encontrado.");

        return new FileStream(filePath, FileMode.Open, FileAccess.Read);
    }

    public Task<bool> DeleteFileAsync(string fileName, string directory)
    {
        var filePath = Path.Combine(_settings.BasePath, directory, fileName);

        if (!File.Exists(filePath)) return Task.FromResult(false);
        File.Delete(filePath);
        return Task.FromResult(true);

    }
}
