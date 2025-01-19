using Application.Files.Interfaces;

namespace Application.Files;

public class FileStorageService : IFileStorageService
{
    public async Task<string> SaveFileAsync(string fileName, byte[] content)
    {
        var directorioPadre = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        var carpetaSubidas = Path.Combine(directorioPadre, "uploads");
        if (!Directory.Exists(carpetaSubidas))
            Directory.CreateDirectory(carpetaSubidas);
        
        var nameFile = Guid.NewGuid() + Path.GetExtension(fileName).ToLowerInvariant();
        
        var ruta = Path.Combine(carpetaSubidas, nameFile);
        await File.WriteAllBytesAsync(ruta, content);
        return nameFile;
    }
    
    public async Task<byte[]> GetFileAsync(string fileName)
    {
        var directorioPadre = Directory.GetParent(Directory.GetCurrentDirectory()).FullName;
        var carpetaSubidas = Path.Combine(directorioPadre, "uploads");
        var ruta = Path.Combine(carpetaSubidas, fileName);
        
        return await File.ReadAllBytesAsync(ruta);
    }
}