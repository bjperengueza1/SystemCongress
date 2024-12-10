namespace Domain.Entities;

public class FileUploaded
{
    public Guid FileId { get; set; } = Guid.NewGuid();
    public string FileName { get; set; }
    public string Path { get; set; }
    public DateTime UploadDate { get; set; } = DateTime.UtcNow;
    
}