using System.Diagnostics;
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
        Console.WriteLine(filePath);
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
    
    public async Task<string> CopyFileAsync(string sourceFileName, string targetFileName, string directory)
    {
        var sourceFilePath = Path.Combine(_settings.BasePath, directory, sourceFileName);
        var targetFilePath = Path.Combine(_settings.BasePath, directory, targetFileName);

        if (!File.Exists(sourceFilePath))
            throw new FileNotFoundException("Archivo no encontrado.");

        await using var sourceStream = new FileStream(sourceFilePath, FileMode.Open, FileAccess.Read);
        await using var targetStream = new FileStream(targetFilePath, FileMode.Create, FileAccess.Write);

        await sourceStream.CopyToAsync(targetStream);

        return targetFileName;
    }

    public void ReplaceTextInWord(string fileName, string directory, string placeholder, string replacementText)
    {
        var inputFilePath = Path.Combine(_settings.BasePath, directory, fileName);
        // Abre el documento de Word
        using var doc = WordprocessingDocument.Open(inputFilePath, true);
        var body = doc.MainDocumentPart.Document.Body;
        foreach (var text in body.Descendants<Text>())
        {
            if (text.Text.Contains(placeholder))
            {
                text.Text = text.Text.Replace(placeholder, replacementText);
            }
        }
        doc.MainDocumentPart.Document.Save();
    }
    
    public void ConvertToPdf(string fileName, string directory)
    {
        var inputFilePath = Path.Combine(_settings.BasePath, directory, fileName);
        var outputFilePath = Path.Combine(_settings.BasePath, directory, Path.GetFileNameWithoutExtension(fileName) + ".pdf");
        
        var process = new Process
        {
            StartInfo = new ProcessStartInfo
            {
                FileName = LibreOfficePath,
                Arguments = $"--headless --convert-to pdf {inputFilePath} --outdir {Path.GetDirectoryName(outputFilePath)}",
                RedirectStandardOutput = true,
                RedirectStandardError = true,
                UseShellExecute = false,
                CreateNoWindow = true
            }
        };
        
        process.Start();
        process.WaitForExit();
    }
}
