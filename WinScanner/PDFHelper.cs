using IronPdf;
using System;
using System.Drawing;
using System.IO;

namespace WinScanner
{
    public class PDFHelper
    {
        static PDFHelper()
        {
            IronPdf.Installation.TempFolderPath = Path.Combine(Path.GetTempPath(), "NOC");
        }
        public static string[] ExtractImage(string pdfPath, string outputRootPath)
        {
            var pdf = PdfDocument.FromFile(pdfPath);
            string fileGuid = Guid.NewGuid().ToString();
            return pdf.RasterizeToImageFiles(@Path.Combine(outputRootPath, fileGuid + "_*.png"), DPI: 300);
        }

        public static string[] ExtractImage(string pdfPath)
        {
            var pdf = PdfDocument.FromFile(pdfPath);

            return pdf.ToPngImages(@Path.Combine(Path.GetDirectoryName(pdfPath), "pdf_pages_*.png"),300);
        }

        public static string GetAllText(string pdfPath)
        {
            var pdf = PdfDocument.FromFile(pdfPath);
            return pdf.ExtractAllText();
        }
    }
}