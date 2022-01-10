using System.IO;
using System.Threading.Tasks;
using DinkToPdf;

namespace Core.Interfaces.Helpers
{

    public interface IDinkToPdfHelper
    {

        Task<HtmlToPdfDocument> CreateHtmlToPdfDocument(
            string fileName,
            string htmlDocumentAsString
        );

    }

}
