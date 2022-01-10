using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Core.Interfaces.Helpers;
using DinkToPdf;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;

namespace Core.Helpers
{

    public class DinkToPdfHelper : IDinkToPdfHelper
    {

        private readonly AppSettings _appSettings;

        public DinkToPdfHelper(
            IOptions<AppSettings> appSettings
        )
        {
            _appSettings = appSettings.Value;
        }

        public async Task<HtmlToPdfDocument> CreateHtmlToPdfDocument(
            string fileName,
            string htmlDocumentAsString
        )
        {

            var doc = new HtmlToPdfDocument()
            {
                GlobalSettings = {
                ColorMode = ColorMode.Color,
                Orientation = Orientation.Portrait,
                PaperSize = PaperKind.A4,
                Margins = new MarginSettings { Top = 10 },
                DPI = 380,
                DocumentTitle = fileName,
            },
                Objects = {
                new ObjectSettings(){
                    PagesCount = true,
                    HtmlContent = htmlDocumentAsString,
                    WebSettings = {
                        DefaultEncoding = "utf-8"
                    },
                    // HeaderSettings = {
                    //     FontSize = 9,
                    //     Right = "Página [page] de [toPage]",
                    //     Line = true,
                    //     Spacing = 2.812
                    // }
                    //HeaderSettings = { FontName = "Arial", FontSize = 9, Right = "Página [page] de [toPage]", Line = true },
                    FooterSettings = { FontName = "Arial", FontSize = 9, Line = true, Center = $"Página [page] de [toPage]." }
                }
            }
            };

            return doc;

        }

    }

}
