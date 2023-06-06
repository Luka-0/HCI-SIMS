using InitialProject.Commands;
using iTextSharp.text.pdf;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using iTextSharp.text;
using System.Windows;
using iTextSharp.text.pdf;
using Microsoft.Win32;

namespace InitialProject.ViewModel
{
    public class TourStatisticsViewModel : BindableBase
    {
        public MyICommand ExportPDFCommand { get; set; }

        public TourStatisticsViewModel()
        {
            UpdateHeaderTitle("Statistika o turama");
            UpdateFooterParametar("home");

            ExportPDFCommand = new MyICommand(OnExport);
        }

        public void OnExport()
        {
            // Create the PDF document
            Document document = new Document(PageSize.A4, 50, 50, 50, 50);

            // Create a SaveFileDialog to allow the user to choose the file path
            SaveFileDialog saveFileDialog = new SaveFileDialog();
            saveFileDialog.Filter = "PDF Files (*.pdf)|*.pdf";
            saveFileDialog.FileName = "output.pdf";

            if (saveFileDialog.ShowDialog() == true)
            {
                string outputPath = saveFileDialog.FileName;

                // Create a PdfWriter instance to write the document to the chosen file
                PdfWriter writer = PdfWriter.GetInstance(document, new FileStream(outputPath, FileMode.Create));

                // Open the document
                document.Open();

                // Add the heading to the document
                Paragraph heading = new Paragraph("Prikaz izvestaja o svim prisustvima turama u odredjenom vremenskom periodu",
                    new Font(Font.FontFamily.HELVETICA, 20, Font.BOLD));
                heading.Alignment = Element.ALIGN_CENTER;
                heading.SpacingAfter = 20;
                document.Add(heading);

                // Add content to the document
                Paragraph paragraph = new Paragraph("Ture odrzane od 03.05.2023. do 03.07.2023. godine:");
                paragraph.SpacingBefore = 20;
                paragraph.SpacingAfter = 20;
                document.Add(paragraph);

                PdfPTable table = new PdfPTable(3); // 3 columns

                // Add table headers
                table.AddCell("Ime gosta");
                table.AddCell("Ime ture");
                table.AddCell("Prisustvo (DA/NE)");

                // Add table data
                table.AddCell("Marko Markovic");
                table.AddCell("Tura jun 2k23");
                table.AddCell("DA");

                table.AddCell("Milan Zikic");
                table.AddCell("Safari Maj '23");
                table.AddCell("NE");

                table.AddCell("Sanja Miletic");
                table.AddCell("Safari Maj '23");
                table.AddCell("NE");

                table.AddCell("Mirza Selimovic");
                table.AddCell("Safari Maj '23");
                table.AddCell("NE");

                table.AddCell("Zeljko Raznatovic");
                table.AddCell("Safari Maj '23");
                table.AddCell("NE");

                table.AddCell("Nenad Rozga");
                table.AddCell("HoneyMoon tour");
                table.AddCell("DA");

                table.AddCell("Luka Petric");
                table.AddCell("CompleteFinalTour");
                table.AddCell("NE");

                table.AddCell("Vukasin Mrnjavcevic");
                table.AddCell("Old City Tour");
                table.AddCell("NE");

                table.AddCell("Lazar Hrebeljanovic");
                table.AddCell("Kosovo tour");
                table.AddCell("DA");

                table.AddCell("Marko Mrljavcevic");
                table.AddCell("Kosovo tour");
                table.AddCell("NE");

                table.AddCell("Vuk Brankovic");
                table.AddCell("Kosovo tour");
                table.AddCell("NE");

                table.AddCell("Sinan Sakic");
                table.AddCell("Tasmajdan Main Event");
                table.AddCell("DA");

                table.AddCell("Marko Zivkovic");
                table.AddCell("Tasmajdan Main Event");
                table.AddCell("DA");

                table.AddCell("Uros Krunic");
                table.AddCell("Tasmajdan Main Event");
                table.AddCell("DA");

                table.AddCell("Matija Pastarevic");
                table.AddCell("Tasmajdan Main Event");
                table.AddCell("DA");

                table.AddCell("Andrija Pantic");
                table.AddCell("Tasmajdan Main Event");
                table.AddCell("DA");

                table.AddCell("Nevena Gligorijevic");
                table.AddCell("PepersLastDance");
                table.AddCell("DA");

                table.AddCell("Miroslav Ilic");
                table.AddCell("Kablar Tour");
                table.AddCell("NE");

                table.AddCell("Matias Lesor");
                table.AddCell("Final Four");
                table.AddCell("NE");

                table.AddCell("Kevin Panter");
                table.AddCell("Final Four");
                table.AddCell("NE");

                table.AddCell("Mario Hezonja");
                table.AddCell("Final Four");
                table.AddCell("DA");

                // Add the table to the document
                document.Add(table);

                Paragraph paragraph1 = new Paragraph("Od ukupnog broja gostiju, 47.62% se pojavilo na turama dok se 52.38% nije pojavilo");
                paragraph1.SpacingBefore = 10;
                paragraph1.SpacingAfter = 10;
                document.Add(paragraph1);

                Paragraph paragraph2 = new Paragraph("Tura sa najvise prisustava: Tasmajdan Main Event");
                paragraph2.SpacingBefore = 10;
                paragraph2.SpacingAfter = 10;
                document.Add(paragraph2);

                Paragraph paragraph3 = new Paragraph("Tura sa najmanje prisustava: Safari Maj '23");
                paragraph3.SpacingBefore = 10;
                paragraph3.SpacingAfter = 10;
                document.Add(paragraph3);


               


                // Close the document
                document.Close();

                MessageBox.Show("PDF exported successfully!");
            }
        }
    }
}
