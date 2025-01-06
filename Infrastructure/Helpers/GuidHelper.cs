namespace Infrastructure.Helpers;

public static class GuidHelper
{
    public static string GenerateGuid()
    {
        return Guid.NewGuid().ToString("N"); // Genera un GUID corto
    }
}