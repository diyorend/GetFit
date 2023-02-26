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
    }
}
