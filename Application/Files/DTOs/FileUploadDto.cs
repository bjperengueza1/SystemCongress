namespace Application.Files.DTOs;

public class FileUploadDto
{
    public string Nombre { get; set; }
    public string Descripcion { get; set; }
    public byte[] Contenido { get; set; } // Contenido del archivo en bytes
}