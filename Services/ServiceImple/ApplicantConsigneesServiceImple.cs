using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Initial_Stage;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.ServiceImple
{
    public class ApplicantConsigneesServiceImple : ApplicantConsigneesService
    {
        private readonly CommercialDBContext _context;
        public ApplicantConsigneesServiceImple(CommercialDBContext context)
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

        public ApplicantConsignees GetbyId(int Id)
        {
            try
            {
                var applicants = _context.ApplicantConsignees
                    .FirstOrDefault(c => c.ApplicantID == Id);
                return applicants;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool AddApplicantConsignees(ApplicantConsignees applicantConsignees)
        {
            try
            {
                _context.ApplicantConsignees.Add(applicantConsignees);
                int result = _context.SaveChanges();
                return result > 0;

            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateApplicantConsignees(ApplicantConsignees applicantConsignees)
        {
            try
            {
                _context.ApplicantConsignees.Update(applicantConsignees);
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
