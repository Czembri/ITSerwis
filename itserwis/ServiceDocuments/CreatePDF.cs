using System;
using System.IO;
using iTextSharp.text;
using iTextSharp.text.pdf;
using iTextSharp.text.pdf.parser;


namespace ItSerwis_Merge_v2
{
    public class CreatePDF
    {
        private static readonly log4net.ILog log = LogHelper.GetLogger();

        public CreatePDF()
        {
            log.Debug($"Calling [{this.GetType()}]");
        }
        public void create()
        {
            FileStream fs = new FileStream("Chapter1_Example1.pdf", FileMode.Create, FileAccess.Write, FileShare.None);
            log.Debug($"New file stream: ['FileStream':'{fs}']");
            Document doc = new Document();
            PdfWriter writer = PdfWriter.GetInstance(doc, fs);
            doc.Open();
            doc.Add(new Paragraph("Hello World"));
            doc.Close();
            log.Debug($"Clossing document: ['Document':'{doc}']");
            PdfReader reader = new PdfReader("Chapter1_Example1.pdf");
            log.Debug($"Reading document: ['Document':'{reader}']");
            string text = string.Empty;
            for (int page = 1; page <= reader.NumberOfPages; page++)
            {
                text += PdfTextExtractor.GetTextFromPage(reader, page);
            }
            reader.Close();

            Console.WriteLine(text);
        }

    }
}

