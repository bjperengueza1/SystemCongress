namespace Application.Utils.Helpers;

public static class FileValidator
{
    private static readonly Dictionary<string, List<byte[]>> _fileSignatures = 
        new Dictionary<string, List<byte[]>>
        {
            { ".pdf", new List<byte[]>
                {
                    new byte[] { 0x25, 0x50, 0x44, 0x46 }
                }
            },
        };

    public static bool ValidateFileSignature(Stream fileStream, string fileExtension)
    {
        if (!_fileSignatures.ContainsKey(fileExtension))
            return false;

        var signatures = _fileSignatures[fileExtension];

        using var reader = new BinaryReader(fileStream);
        var headerBytes = reader.ReadBytes(signatures.Max(m => m.Length));

        return signatures.Any(signature => 
            headerBytes.Take(signature.Length).SequenceEqual(signature));
    }

    public static bool ValidateFileExtension(string fileName, string[] permittedExtensions)
    {
        var ext = Path.GetExtension(fileName).ToLowerInvariant();
        return !string.IsNullOrEmpty(ext) && permittedExtensions.Contains(ext);
    }
}