using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.ExportInvoice;

namespace CommercialManagement.Services.Exports
{
    public interface ExportDataService
    {
        List<ExportData> GetExportData(string ExpInvID);
        bool AddExportData(ExportData exportData);
        bool UpdateExportData(ExportData exportData);
        ExportData GetbyId(int Id);
    }
}
