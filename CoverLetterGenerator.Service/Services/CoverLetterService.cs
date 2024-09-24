using CoverLetterGenerator.Service.Models;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml.Wordprocessing;
using Microsoft.Office.Interop.Word;
using System.Globalization;

namespace CoverLetterGenerator.Service.Services
{
    public interface ICoverLetterService
    {
        void ProcessFile( CoverLetterModel coverLetterModel );
    }

    public class CoverLetterService : ICoverLetterService
    {
        public void ProcessFile( CoverLetterModel coverLetterModel )
        {
            coverLetterModel = SharpCoverLetterModel(coverLetterModel);
            var sourceFolder = $"c:\\CoverLetterFiles";
            var sourceFile = $"{sourceFolder}\\FRANK RAMOS - Anschreiben.docx";

            string companyFolder = $"{sourceFolder}\\{coverLetterModel.CompanyName}";
            if (!Directory.Exists( companyFolder ))
            {
                Directory.CreateDirectory( companyFolder );
            }

            var finalResultPdf = $"{companyFolder}\\FRANK RAMOS - {coverLetterModel.CompanyName} - Anschreiben.pdf";
            var fileResult = $"{companyFolder}\\FRANK RAMOS - {coverLetterModel.CompanyName} - Anschreiben.docx";
            
            if (File.Exists( fileResult ))
            {
                File.Delete( fileResult );
            }

            File.Copy( sourceFile, fileResult );

            using (WordprocessingDocument wordDoc = WordprocessingDocument.Open( fileResult, true ))
            {
                // Access the main document part
                var body = wordDoc.MainDocumentPart.Document.Body;

                // Search for the text to replace
                foreach (var text in body.Descendants<Text>())
                {
                    if (text.Text.Contains( "companynametag" ))
                    {
                        text.Text = text.Text.Replace( "companynametag", coverLetterModel.CompanyName );
                    }
                    if (text.Text.Contains( "companyziptag" ))
                    {
                        text.Text = text.Text.Replace( "companyziptag", coverLetterModel.CompanyZip );
                    }
                    if (text.Text.Contains( "companycitytag" ))
                    {
                        text.Text = text.Text.Replace( "companycitytag", coverLetterModel.CompanyCity );
                    }
                    if (text.Text.Contains( "companyaddresstag" ))
                    {
                        text.Text = text.Text.Replace( "companyaddresstag", coverLetterModel.CompanyAddress );
                    }
                    if (text.Text.Contains( "jobpositiontag" ))
                    {
                        text.Text = text.Text.Replace( "jobpositiontag", coverLetterModel.JobPosition );
                    }
                    if (text.Text.Contains( "datetag" ))
                    {
                        var date = (DateTime.Now).ToString( "dd.MMMM.yyyy", new CultureInfo( "de-DE" ) );
                        text.Text = text.Text.Replace( "datetag", date );
                    }
                }
                wordDoc.MainDocumentPart.Document.Save();

            }

            Application app = new Application();
            var doc = app.Documents.Open( fileResult );
            doc.SaveAs2( finalResultPdf, WdSaveFormat.wdFormatPDF );
            doc.Close();
            app.Quit();

        }


        private CoverLetterModel SharpCoverLetterModel( CoverLetterModel coverLetterModel )
        {
            coverLetterModel.JobPosition = coverLetterModel.JobPosition.Trim().ToUpper();
            coverLetterModel.CompanyCity = coverLetterModel.CompanyCity.Trim().ToUpper();
            coverLetterModel.CompanyAddress = coverLetterModel.CompanyAddress.Trim().ToUpper();
            coverLetterModel.CompanyName = coverLetterModel.CompanyName.Trim().ToUpper();
            coverLetterModel.CompanyZip = coverLetterModel.CompanyZip.Trim().ToUpper();


            return coverLetterModel;
        }
    }
}
