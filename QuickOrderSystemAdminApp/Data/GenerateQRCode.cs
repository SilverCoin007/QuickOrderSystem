using System;
using System.Collections.Generic;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Net.NetworkInformation;
using System.Text;
using System.Threading.Tasks;
using ZXing;
using ZXing.Windows.Compatibility;


namespace QuickOrderSystemAdminApp.Data
{
    public class GenerateQRCode
    {
        // Methode zur Generierung eines QR-Codes als Base64-String
        public string GenerateQRCodeAsBase64String(string url)
        {
            System.Drawing.Color background = System.Drawing.Color.White;
            System.Drawing.Color foreground = System.Drawing.Color.Black;

            if (string.IsNullOrEmpty(url))
            {
                throw new ArgumentNullException(nameof(url), "URL darf nicht leer sein.");
            }

            var barcodeWriter = new BarcodeWriter
            {
                Renderer = new BitmapRenderer { Background = background, Foreground = foreground },
                Format = BarcodeFormat.QR_CODE
            };
            barcodeWriter.Format = BarcodeFormat.QR_CODE;
            barcodeWriter.Options = new ZXing.Common.EncodingOptions
            {
                Width = 300,
                Height = 300
            };

            using (Bitmap result = barcodeWriter.Write(url))
            using (MemoryStream ms = new MemoryStream())
            {
                result.Save(ms, System.Drawing.Imaging.ImageFormat.Png);
                return Convert.ToBase64String(ms.ToArray());
            }
        }

        // Methode zur Umwandlung eines Base64-Strings in ein Bitmap-Bild
        public Bitmap Base64StringToBitmap(string base64String)
        {
            if (string.IsNullOrEmpty(base64String))
            {
                throw new ArgumentNullException(nameof(base64String), "Base64-String darf nicht leer sein.");
            }

            byte[] imageBytes = Convert.FromBase64String(base64String);
            using (MemoryStream ms = new MemoryStream(imageBytes))
            {
                return new Bitmap(ms);
            }
        }
    }
}
