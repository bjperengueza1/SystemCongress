namespace Infrastructure.Settings;

public class FileStorageSettings
{
    public string BasePath { get; set; } = "../Files";
    public string PresentationsPath { get; set; } = "Presentations";
    public string ConferencePath { get; set; } = "Conferences";
    public string TemplateCertificatesPath { get; set; } = "TemplateCertificates";
}