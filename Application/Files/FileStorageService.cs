using Application.Files.Interfaces;

namespace Application.Files;

public class FileStorageService : IFileStorageService
{
    public async Task<string> SaveFileAsync(string fileName, byte[] content)
    {
        var carpetaSubidas = Path.Combine("/home/wdg/Descargas/", "uploads");
        if (!Directory.Exists(carpetaSubidas))
            Directory.CreateDirectory(carpetaSubidas);
        
        var ruta = Path.Combine(carpetaSubidas, fileName);
        await File.WriteAllBytesAsync(ruta, content);
        return ruta;
    }
}