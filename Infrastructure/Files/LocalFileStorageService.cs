using System.Diagnostics;
using System.Runtime.InteropServices;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Domain.Interfaces.Files;
using Infrastructure.Settings;
using Microsoft.Extensions.Options;

namespace Infrastructure.Files;

public class LocalFileStorageService : IFileStorageService
{
    private readonly FileStorageSettings _settings;
    private const string LibreOfficePath = "/usr/bin/soffice";
    private const string LibreOfficePathWindows = @"C:\Program Files\LibreOffice\program\soffice.exe";
    
    public LocalFileStorageService(IOptions<FileStorageSettings> options)
    {
        _settings = options.Value;
    }
    
    public async Task<string> SaveFileAsync(Stream fileStream, string fileName, List<string> directory)
    {
        var paths = new List<string> {_settings.BasePath};
        paths.AddRange(directory);
        
        var folderPath = Path.Combine(paths.ToArray());
        
        if (!Directory.Exists(folderPath))
            Directory.CreateDirectory(folderPath); // Asegura que la carpeta exista
        
        
        var filePath = Path.Combine(folderPath, fileName);
        
        //validar  Cannot access a closed Stream
        await using var fileStreamOutput = new FileStream(filePath, FileMode.Create);
        fileStream.Seek(0, SeekOrigin.Begin);
        await fileStream.CopyToAsync(fileStreamOutput);

        return fileName; // Retorna la ruta almacenada
    }

    public async Task<Stream> GetFileAsync(string fileName, string[] directory)
    {
        var path = Path.Combine(directory);
        var filePath = Path.Combine(_settings.BasePath, path, fileName);
        
        if (!File.Exists(filePath))
            throw new FileNotFoundException("Archivo no encontrado.");

        return new FileStream(filePath, FileMode.Open, FileAccess.Read);
    }

    public async Task<bool> DeleteFileAsync(string fileName, string[] directory)
    {
        var path = Path.Combine(directory);
        var filePath = Path.Combine(_settings.BasePath, path, fileName);

        if (!File.Exists(filePath)) return await Task.FromResult(false);
        File.Delete(filePath);
        return await Task.FromResult(true);

    }
    
    public async Task<string> CopyFileAsync(string sourceFileName, string targetFileName, string[] paths)
    {
        var directory = Path.Combine(paths);
        var sourceFilePath = Path.Combine(_settings.BasePath, directory, sourceFileName);
        var targetFilePath = Path.Combine(_settings.BasePath, directory, targetFileName);
        
        if (!File.Exists(sourceFilePath))
            throw new FileNotFoundException("Archivo no encontrado.");

        await using var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
        await using var targetStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write);

        await sourceStream.CopyToAsync(targetStream);

        return targetFileName;
    }

    public void ReplaceTextInWord(string fileName, string[] directory, Dictionary<string, string> replacements)
    {
        var path = Path.Combine(directory);
        var inputFilePath = Path.Combine(_settings.BasePath, path, fileName);
        //Validar si el archivo existe
        if (!File.Exists(inputFilePath))
            throw new FileNotFoundException("Archivo no encontrado.");
        // Abre el documento de Word
        using var doc = WordprocessingDocument.Open(inputFilePath, true);
        var body = doc.MainDocumentPart.Document.Body;
        foreach (var text in body.Descendants<Text>())
        {
            foreach (var replacement in replacements)
            {
                if (text.Text.Contains(replacement.Key))
                {
                    text.Text = text.Text.Replace(replacement.Key, replacement.Value);
                }
            }
        }
        doc.MainDocumentPart.Document.Save();
    }
    
    public string ConvertToPdf(string fileName, string[] directory)
    {
        var path = Path.Combine(directory);
        var inputFilePath = Path.Combine(_settings.BasePath, path, fileName);
        var nombrePdf = Path.GetFileNameWithoutExtension(fileName)+".pdf";
        var outputFilePath = Path.Combine(_settings.BasePath, path, nombrePdf);
        
        //check if is windows or linux
        var libreOfficePatch = "";
        if (RuntimeInformation.IsOSPlatform(OSPlatform.Windows))
        {
            libreOfficePatch = LibreOfficePathWindows;
        }
        else if (RuntimeInformation.IsOSPlatform(OSPlatform.Linux))
        {
            libreOfficePatch = LibreOfficePath;
        }
        else
        {
            throw new PlatformNotSupportedException("Plataforma no soportada.");
        }
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = libreOfficePatch,
                Arguments = $"--headless --convert-to pdf \"{inputFilePath}\" --outdir \"{Path.GetDirectoryName(outputFilePath)}\"",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        
        process.Start();
        process.WaitForExit();

        return nombrePdf;
    }
}
