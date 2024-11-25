using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Reporting.NETCore;
using System.Data;
using System.Text;
using EquipmentsSystem.Shared.Models;
using System.Net.Http.Headers;
using System.Net;

namespace EquipmentsSystem.Server.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IWebHostEnvironment _webHostEnvironment;
        public ReportController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
            System.Text.Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
        }

        [HttpPost]

        public IActionResult GetReport(ReportItems reportitem)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Reports", reportitem.ReportName + ".rdlc");
            Stream reportDefinition;
            using var fs = new FileStream(path, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.EnableExternalImages = true;
            DataTable dt = new DataTable();
            for (int i = 0; i < reportitem.Count; i++)
            {
                dt.Columns.Add(reportitem.items[i]);
            }
            DataRow row1;
            for (int k = reportitem.Count; k < reportitem.items.Count(); k = k + reportitem.Count)
            {
                row1 = dt.NewRow();
                for (int c = 0; c < reportitem.Count; c++)
                {
                    row1[reportitem.items[c]] = reportitem.items[k + c];
                }

                dt.Rows.Add(row1);

            }

            if (dt != null)
            {
                report.DataSources.Add(new ReportDataSource(reportitem.DataTableName, dt));
            }

            if (reportitem.parameters != null)
            {
                foreach (var item in reportitem.parameters)
                {
                    report.SetParameters(new[] { new ReportParameter(item.Key, item.Value) });
                }
            }

            byte[] pdf = report.Render("PDF");
            //fs.Dispose();

            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new ByteArrayContent(pdf);
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = "arnb";
            //return response;
            return File(pdf, "application/pdf", reportitem.ReportName + ".pdf");
        }
        [HttpPost("Multiple")]
        public IActionResult GetReportMultiple(ReportItemsMultiple reportitem)
        {
            string path = Path.Combine(_webHostEnvironment.WebRootPath, "Reports", reportitem.ReportName + ".rdlc");
            Stream reportDefinition;
            using var fs = new FileStream(path, FileMode.Open);
            reportDefinition = fs;
            LocalReport report = new LocalReport();
            report.LoadReportDefinition(reportDefinition);
            report.EnableExternalImages = true;
            int iterator = 0;
            DataRow row1;
            List<DataTable> dt = new List<DataTable>();

            for (int k = 0; k < reportitem.Count.Count(); k++)
            {
                dt.Add(new DataTable());
                for (int i = 0 + iterator; i < reportitem.Count[k] + iterator; i++)
                {
                    dt[k].Columns.Add(reportitem.items[i]);
                }
                for (int j = reportitem.Count[k] + iterator; j < (reportitem.Count[k] * (reportitem.RowsCount[k] + 1) + iterator); j = j + reportitem.Count[k])
                {
                    row1 = dt[k].NewRow();
                    for (int c = 0; c < reportitem.Count[k]; c++)
                    {
                        row1[reportitem.items[c + iterator]] = reportitem.items[j + c];
                    }

                    dt[k].Rows.Add(row1);

                }
                if (dt[k] != null)
                {
                    report.DataSources.Add(new ReportDataSource(reportitem.DataTableName[k], dt[k]));
                }
                iterator += (reportitem.Count[k] * (reportitem.RowsCount[k] + 1));
            }
            if (reportitem.parameters != null)
            {
                foreach (var item in reportitem.parameters)
                {
                    report.SetParameters(new[] { new ReportParameter(item.Key, item.Value) });
                }
            }
            byte[] pdf = report.Render("PDF");
            //fs.Dispose();

            //HttpResponseMessage response = new HttpResponseMessage(HttpStatusCode.OK);
            //response.Content = new ByteArrayContent(pdf);
            //response.Content.Headers.ContentType = new MediaTypeHeaderValue("application/pdf");
            //response.Content.Headers.ContentDisposition = new System.Net.Http.Headers.ContentDispositionHeaderValue("attachment");
            //response.Content.Headers.ContentDisposition.FileName = "arnb";
            //return response;
            return File(pdf, "application/pdf", reportitem.ReportName + ".pdf");
        }
    }
}