using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Initial_Stage;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.ServiceImple
{
    public class BeneficiaryServiceImple : BeneficiaryService
    {
        private readonly CommercialDBContext _context;
        public BeneficiaryServiceImple(CommercialDBContext context) 
        {
            _context = context;
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

        public Beneficiary GetbyId(int Id)
        {
            try
            {
                var beneficiary = _context.Beneficiary
                    .FirstOrDefault(c => c.BenID == Id);
                return beneficiary;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddBeneficiary(Beneficiary beneficiary)
        {
            try
            {
                _context.Beneficiary.Add(beneficiary);
                int result = _context.SaveChanges();
                return result > 0 ; 

            }
            catch (Exception)
            {
                throw;
            }
        }

        public bool UpdateBeneficiary(Beneficiary beneficiary)
        {
            try
            {
                _context.Beneficiary.Update(beneficiary);
                int result = _context.SaveChanges();
                return result > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }
    }
}
