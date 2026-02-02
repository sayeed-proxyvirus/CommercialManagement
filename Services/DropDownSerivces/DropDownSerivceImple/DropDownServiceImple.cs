using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.ExpLC;
using CommercialManagement.Models.ExportInvoice;
using CommercialManagement.Models.Initial_Stage;
using CommercialManagement.Models.Styles;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.DropDownSerivces.DropDownSerivceImple
{
    public class DropDownServiceImple : DropDownService
    {
        private readonly CommercialDBContext _context;
        public DropDownServiceImple(CommercialDBContext context)
        {
            _context = context;
        }

        public List<ApplicantConsignees> GetApplicantConsignees()
        {
            try
            {
                var applicants = _context.ApplicantConsignees
                    .FromSqlRaw("EXEC usp_ViewConApp")
                    .ToList();
                return applicants;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Beneficiary> GetBeneficiary()
        {
            try
            {
                var beneficiary = _context.Beneficiary
                    .FromSqlRaw("EXEC usp_ViewBeneficiary")
                    .ToList();
                return beneficiary;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Customer> GetCustomer()
        {
            try
            {
                var customers = _context.Customer
                    .FromSqlRaw("EXEC usp_ViewCust")
                    .ToList();
                return customers;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Fabrics> GetFabrics()
        {
            try
            {
                var fabrics = _context.Fabrics
                    .FromSqlRaw("EXEC usp_ViewFabrics")
                    .ToList();
                return fabrics;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<NotifyingParty> GetNotifyingParty()
        {
            try
            {
                var notifyingParty = _context.NotifyingParty
                    .FromSqlRaw("EXEC usp_ViewNotParty")
                    .ToList();
                return notifyingParty;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<Party> GetParty()
        {
            try
            {
                var party = _context.Party
                    .FromSqlRaw("EXEC usp_ViewParty")
                    .ToList();
                return party;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<ExportMain> GetExportMainLCs()
        {
            try
            {
                //var param = new SqlParameter("@ExpLCName", ExpLCName);
                var exportMainViewModel = _context.ExportMain////main model load hoy viewmodel na arki
                    .FromSqlRaw("EXEC usp_ViewExpLC")
                    .ToList();
                return exportMainViewModel;
            }
            catch (Exception)
            {
                throw;
            }
        }

        public List<ExportInvoices> GetExportInvoices()
        {
            try
            {
                var exportInvoice = _context.ExportInvoices
                    .FromSqlRaw("EXEC usp_ViewExpInv")
                    .ToList();
                return exportInvoice;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public List<StyleInfo> GetStyleInfos() 
        {
            try
            {
                var styleInf = _context.StyleInfo
                    .FromSqlRaw("EXEC usp_ViewStyleInfos")
                    .ToList();
                return styleInf;
            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
