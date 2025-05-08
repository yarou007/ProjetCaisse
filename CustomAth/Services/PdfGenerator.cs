using System;
using System.IO;
using DinkToPdf;

namespace CustomAth.Services
{
    public static class PdfGenerator
    {
        public static byte[] GeneratePdf(string html, PaperKind paperKind = PaperKind.A4)
        {
            var converter = new SynchronizedConverter(new PdfTools());

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                    PaperSize = paperKind,
                    Orientation = Orientation.Portrait,
                    Margins = new MarginSettings { Top = 10, Bottom = 10, Left = 10, Right = 10 }
                },
                Objects = {
                    new ObjectSettings()
                    {
                        HtmlContent = html,
                        WebSettings = { DefaultEncoding = "utf-8" }
                    }
                }
            };

            return converter.Convert(doc);
        }
    }
}