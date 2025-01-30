namespace Application.Utils.Helpers;

public static class FileValidator
{
    private static readonly Dictionary<string, List<byte[]>> _fileSignatures = new()
    {
        { ".pdf", new List<byte[]> { new byte[] { 0x25, 0x50, 0x44, 0x46 } } }, // %PDF
    };

    public static bool ValidateFileSignature(Stream fileStream, string fileExtension)
    {
        if (!_fileSignatures.ContainsKey(fileExtension))
            return false;

        var signatures = _fileSignatures[fileExtension];

        // Guardar la posición original del Stream para no afectar su posterior lectura
        long originalPosition = fileStream.Position;
        fileStream.Seek(0, SeekOrigin.Begin); // Ir al inicio del archivo

        using var reader = new BinaryReader(fileStream);
        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

        // Restaurar la posición original del Stream
        fileStream.Seek(originalPosition, SeekOrigin.Begin);

        return signatures.Any(signature =>
            headerBytes.Take(signature.Length).SequenceEqual(signature));
    }

    public static bool ValidateFileExtension(string fileName, string[] permittedExtensions)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return !string.IsNullOrEmpty(ext) && permittedExtensions.Contains(ext);
    }
}
