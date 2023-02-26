using GetFit.Data;
using GetFit.Interfaces;
using GetFit.Models;
using Microsoft.EntityFrameworkCore;

namespace GetFit.Repository
{
    public class HomeRepository : IHomeRepository
    {
        private readonly DataContext _context;

        public HomeRepository(DataContext context)
        {
            _context = context;
        }
        public bool Add(Home home)
        {
            _context.Add(home);
            return Save();
        }

        public bool Delete(Home home)
        {
            _context.Remove(home);
            return Save();
        }

        public async Task<IEnumerable<Home>> GetAll()
        {
            return await _context.Homes.ToListAsync();
        }

        public async Task<Home> GetByIdAsync(int id)
        {
           return await _context.Homes.Include(h => h.Address).FirstOrDefaultAsync(h => h.Id == id);
        }
        public async Task<Home> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Homes.Include(h => h.Address).AsNoTracking().FirstOrDefaultAsync(h => h.Id == id);
        }

        public async Task<IEnumerable<Home>> GetHomeByCity(string city)
        {
            return await _context.Homes.Where(h => h.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Home home)
        {
            _context.Update(home);
            return Save();
        }
    }
}
