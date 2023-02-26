using GetFit.Data;
using GetFit.Interfaces;
using GetFit.Models;
using Microsoft.EntityFrameworkCore;

namespace GetFit.Repository
{
    public class GymRepository : IGymRepository
    {
        private readonly DataContext _context;

        public GymRepository(DataContext context)
        {
            _context = context;
        }
        public bool Add(Gym gym)
        {
            _context.Add(gym);
            return Save();
        }

        public bool Delete(Gym gym)
        {
            _context.Remove(gym);
            return Save();
        }

        public async Task<IEnumerable<Gym>> GetAll()
        {
            return await _context.Gyms.ToListAsync();
        }

        public async Task<Gym> GetByIdAsync(int id)
        {
            return await _context.Gyms.Include(g => g.Address).FirstOrDefaultAsync(g => g.Id == id);
        }
        public async Task<Gym> GetByIdAsyncNoTracking(int id)
        {
            return await _context.Gyms.Include(g => g.Address).AsNoTracking().FirstOrDefaultAsync(g => g.Id == id);
        }

        public async Task<IEnumerable<Gym>> GetGymByCity(string city)
        {
            return await _context.Gyms.Where(g => g.Address.City.Contains(city)).ToListAsync();
        }

        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }

        public bool Update(Gym gym)
        {
            _context.Gyms.Update(gym);
            return Save();
        }
    }
}
