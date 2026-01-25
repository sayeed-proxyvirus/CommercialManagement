using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.ExportInvoice;
using CommercialManagement.Models.Initial_Stage;

namespace CommercialManagement.Services.DropDownSerivces
{
    public interface DropDownService
    {
        List<Beneficiary> GetBeneficiary();
        List<NotifyingParty> GetNotifyingParty();
        List<Fabrics> GetFabrics();
        List<Customer> GetCustomer();
        List<Party> GetParty();
        List<ApplicantConsignees> GetApplicantConsignees();
        List<ExportMain> GetExportMainLCs();
        List<ExportInvoices> GetExportInvoices();
    }
}
