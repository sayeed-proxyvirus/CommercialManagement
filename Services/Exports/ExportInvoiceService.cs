using CommercialManagement.Models.ExportInvoice;

namespace CommercialManagement.Services.Exports
{
    public interface ExportInvoiceService
    {
        List<ExportInvoices> GetExportInvoices();
        bool AddExportInvoices(ExportInvoices exportInvoices);
        bool UpdateExportInvoices(ExportInvoices exportInvoices);
        ExportInvoices GetbyId(int Id);
    }
}
