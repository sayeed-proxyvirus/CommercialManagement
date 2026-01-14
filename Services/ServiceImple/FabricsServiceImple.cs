using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Initial_Stage;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.ServiceImple
{
    public class FabricsServiceImple : FabricsService
    {
        private readonly CommercialDBContext _context;
        public FabricsServiceImple(CommercialDBContext context)
        {
            _context = context;
        }
        public Fabrics GetbyId(int Id)
        {
            try
            {
                var fabrics = _context.Fabrics
                    .FirstOrDefault(c => c.ItemID == Id);
                return fabrics;
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
        public bool AddFabrics(Fabrics fabrics)
        {
            try
            {
                _context.Fabrics.Add(fabrics);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateFabrics(Fabrics fabrics)
        {
            try
            {
                _context.Fabrics.Update(fabrics);
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
