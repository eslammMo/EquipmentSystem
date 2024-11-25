using EquipmentsSystem.Shared.Models;
using Microsoft.AspNetCore.Mvc;
using System.Net.Http.Json;
using System.Text.Json;
using System.Text.Json.Serialization;

namespace EquipmentsSystem.Client.Services.ReportService
{
    public class ReportService : IReportService
    {
        private readonly HttpClient _http;
        public ReportService(HttpClient http)
        {
            _http = http;
        }



        public async Task<Byte[]> GetReport(ReportItems Report)
        {
            var result = await _http.PostAsJsonAsync("api/Report", Report);
            var newReport = (await result.Content
                .ReadAsByteArrayAsync());

            return newReport;
        }
        public async Task<Byte[]> GetReportMultiple(ReportItemsMultiple Report)
        {
            var result = await _http.PostAsJsonAsync("api/Report/Multiple", Report);
            var newReport = (await result.Content
                .ReadAsByteArrayAsync());

            return newReport;
        }

    }
}