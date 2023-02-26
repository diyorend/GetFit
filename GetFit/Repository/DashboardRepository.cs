using GetFit.Data;
using GetFit.Interfaces;
using GetFit.Models;
using Microsoft.EntityFrameworkCore;

namespace GetFit.Repository
{
    public class DashboardRepository:IDashboardRepository
    {
        private readonly DataContext _context;
        private readonly IHttpContextAccessor _httpContext;

        public DashboardRepository(DataContext context, IHttpContextAccessor httpContext)
        {
            _context = context;
            _httpContext = httpContext;
        }

        public async Task<List<Gym>> GetAllUserGyms()
        {
            var currentUserId = _httpContext.HttpContext?.User.GetUserId();
            var userGyms = await _context.Gyms.Where(g => g.AppUser.Id == currentUserId).ToListAsync();
            return userGyms;
        }

        public async Task<List<Home>> GetAllUserHomes()
        {
            var currentUserId = _httpContext.HttpContext?.User.GetUserId();
            var userHomes = await _context.Homes.Where(g => g.AppUser.Id == currentUserId).ToListAsync();
            return userHomes;
        }

        public async Task<AppUser> GetUserById(string userId)
        {
            return await _context.Users.FindAsync(userId);
        }

        public async Task<AppUser> GetUserByIdNoTracking(string id)
        {
            return await _context.Users.Where(u => u.Id == id).AsNoTracking().FirstOrDefaultAsync();
        }

        public bool Update(AppUser user)
        {
            _context.Users.Update(user);
            return Save();
        }
        public bool Save()
        {
            var saved = _context.SaveChanges();
            return saved > 0 ? true : false;
        }
    }
}
