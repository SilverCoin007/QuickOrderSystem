using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System;
using System.IO;

namespace QuickOrderSystemClassLibrary.Service
{
    internal class ImageHandler
    {
        public byte[] ImageToByteArray(string imagePath)
        {
            try
            {
                // Das Bild von der Datei laden
                using (FileStream fs = new FileStream(imagePath, FileMode.Open, FileAccess.Read))
                {
                    // Byte-Array erstellen und Bilddaten aus der Datei lesen
                    byte[] imageData = new byte[fs.Length];
                    fs.Read(imageData, 0, (int)fs.Length);
                    return imageData;
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung hier hinzufügen, wenn das Laden des Bilds fehlschlägt
                Console.WriteLine($"Fehler beim Laden des Bilds: {ex.Message}");
                return null;
            }
        }

        public void ByteArrayToImage(byte[] imageData, string outputPath)
        {
            try
            {
                // Ein FileStream erstellen, um das Bild zu speichern
                using (FileStream fs = new FileStream(outputPath, FileMode.Create))
                {
                    // Das Byte-Array in den FileStream schreiben
                    fs.Write(imageData, 0, imageData.Length);
                }
            }
            catch (Exception ex)
            {
                // Fehlerbehandlung hier hinzufügen, wenn das Speichern des Bilds fehlschlägt
                Console.WriteLine($"Fehler beim Speichern des Bilds: {ex.Message}");
            }
        }
    }

}
