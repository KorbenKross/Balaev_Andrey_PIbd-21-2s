using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using Microsoft.Reporting.WebForms;
using System.Web.Mvc;
using System.IO;
using VirtualStore;
using VirtualStorePlace.LogicInterface;
using VirtualStorePlace.ConnectingModel;
using VirtualStorePlace.UserViewModel;
using DocumentFormat.OpenXml.Packaging;
using DocumentFormat.OpenXml;
using System.Drawing;
using System.IO.Packaging;

namespace VirtualStoreWeb.Controllers
{
    public class ReportsController : Controller
    {
        public int Id { set { id = value; } }

        private IReportService service;

        private int? id;

        public ReportsController(IReportService service)
        {
            this.service = service;
        }

        // GET: Reports
        public ActionResult Reports()
        {
            return View();
        }

        public ActionResult Report(string id)
        {
            LocalReport lr = new LocalReport();
            DateTime date1 = new DateTime(2018, 5, 5);
            DateTime date2 = new DateTime(2018, 6, 3);
            string path = Path.Combine(Server.MapPath("~/"), "Report1.rdlc");
            if (System.IO.File.Exists(path))
            {
                lr.ReportPath = path;
            }
            else
            {
                return View("Reports");
            }

            ReportParameter parameter = new ReportParameter("ReportParameterPeriod",
                                             "c " + date1.ToShortDateString() +
                                             " по " + date2.ToShortDateString());
            lr.SetParameters(parameter);


            var dataSource1 = service.GetClientOrders(new ReportConnectingModel
            {
                DateFrom = date1,
                DateTo = date2
            });

            ReportDataSource sourceOrders = new ReportDataSource("DataSetOrders", dataSource1);
            lr.DataSources.Add(sourceOrders);

            lr.Refresh();

            string reportType = id;
            string mimeType;
            string encoding;
            string fileNameExtension;

            string deviceInfo =

            "<DeviceInfo>" +
            "  <OutputFormat>" + id + "</OutputFormat>" +
            "  <PageWidth>8.5in</PageWidth>" +
            "  <PageHeight>11in</PageHeight>" +
            "  <MarginTop>0.5in</MarginTop>" +
            "  <MarginLeft>1in</MarginLeft>" +
            "  <MarginRight>1in</MarginRight>" +
            "  <MarginBottom>0.5in</MarginBottom>" +
            "</DeviceInfo>";

            Warning[] warnings;
            string[] streams;
            byte[] renderedBytes;

            renderedBytes = lr.Render(
                reportType,
                deviceInfo,
                out mimeType,
                out encoding,
                out fileNameExtension,
                out streams,
                out warnings);
            return File(renderedBytes, mimeType);
        }

        public ActionResult ReportWord(string id)
        {
            service.SaveProductPrice(new ReportConnectingModel
            {
                FileName = Server.MapPath("~/ReportWord")
            });
            FileStream fs = new FileStream(Server.MapPath("~/ReportWord.docx"), FileMode.OpenOrCreate, FileAccess.ReadWrite);
            return File(fs, "application/vnd.ms-word", "ReportWord.docx");
        }

        public ActionResult ReportExcel(string id)
        {
            service.SaveStocksLoad(new ReportConnectingModel
            {
                FileName = Server.MapPath("~/ReportExcle")
            });

            //byte[] byteArray = File.ReadAllBytes(Server.MapPath("~/ReportExcle"));
            //using (MemoryStream memoryStream = new MemoryStream())
            //{
            //    memoryStream.Write(byteArray, 0, byteArray.Length);
            //    using (WordprocessingDocument doc = WordprocessingDocument.Open(memoryStream, true))
            //    {
            //        HtmlConverterSettings settings = new HtmlConverterSettings()
            //        {
            //            PageTitle = "My Page Title"
            //        };
            //        XElement html = HtmlConverter.ConvertToHtml(doc, settings);

            //        // Note: the XHTML returned by ConvertToHtmlTransform contains objects of type
            //        // XEntity. PtOpenXmlUtil.cs defines the XEntity class. See
            //        // [url]http://blogs.msdn.com/ericwhite/archive/2010/01/21/writing-entity-references-using-linq-to-xml.aspx[/url]
            //        // for detailed explanation.
            //        //
            //        // If you further transform the XML tree returned by ConvertToHtmlTransform, you
            //        // must do it correctly, or entities do not serialize properly.

            //        File.WriteAllText("Test.html", html.ToStringNewLineOnAttributes());
            //    }
            //}

                return RedirectPermanent("/Reports/Reports");
        }
    }
}