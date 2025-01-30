namespace Application.Congresses.Services;

using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;

public class WordReplacer
{
    public static void ReplaceTextInWord(string inputFilePath, string outputFilePath, string placeholder, string replacementText)
    {
        // Copia el archivo original a un nuevo archivo
        File.Copy(inputFilePath, outputFilePath, true);

        // Abre el documento de Word copiado
        using (WordprocessingDocument wordDoc = WordprocessingDocument.Open(outputFilePath, true))
        {
            // Obtén el cuerpo del documento
            var body = wordDoc.MainDocumentPart.Document.Body;

            // Reemplaza el marcador de posición
            foreach (var text in body.Descendants<Text>())
            {
                if (text.Text.Contains(placeholder))
                {
                    text.Text = text.Text.Replace(placeholder, replacementText);
                }
            }

            // Guarda los cambios
            wordDoc.MainDocumentPart.Document.Save();
        }
    }
}
