using CommercialManagement.Models.ExportInvoice;
using CommercialManagement.Models.ViewModels;

namespace CommercialManagement.Services.Exports
{
    public interface ExportInvoiceService
    {
        List<ExportInvoiceViewModel> GetExportInvoices(string ExpInv);
        bool AddExportInvoices(ExportInvoices exportInvoices);
        bool UpdateExportInvoices(ExportInvoices exportInvoices);
        ExportInvoices GetbyId(int Id);
    }
}
