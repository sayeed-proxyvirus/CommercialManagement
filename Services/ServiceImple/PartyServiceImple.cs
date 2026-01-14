using CommercialManagement.Models.ApplicationDBContext;
using CommercialManagement.Models.Initial_Stage;
using Microsoft.EntityFrameworkCore;

namespace CommercialManagement.Services.ServiceImple
{
    public class PartyServiceImple : PartyService
    {
        private readonly CommercialDBContext _context;
        public PartyServiceImple(CommercialDBContext context)
        {
            _context = context;
        }
        
        public Party GetbyId(int Id)
        {
            try
            {
                var party = _context.Party
                    .FirstOrDefault(c => c.PartyID == Id);
                return party;
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
        public bool AddParty(Party party)
        {
            try
            {
                _context.Party.Add(party);
                int result = _context.SaveChanges();
                return result > 0;
            }
            catch (Exception)
            {
                throw;
            }
        }
        public bool UpdateParty(Party party)
        {
            try
            {
                _context.Party.Update(party);
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
