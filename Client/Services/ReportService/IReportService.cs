using EquipmentsSystem.Shared.Models;
using static System.Net.WebRequestMethods;

namespace EquipmentsSystem.Client.Services.ReportService
{
    public interface IReportService
    {
         Task<Byte[]> GetReport(ReportItems Report);

        Task<Byte[]> GetReportMultiple(ReportItemsMultiple Report);

    }
}
