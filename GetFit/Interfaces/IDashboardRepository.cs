using GetFit.Models;

namespace GetFit.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Home>> GetAllUserHomes();
        Task<List<Gym>> GetAllUserGyms();
        Task<AppUser> GetUserById(string userId);
        Task<AppUser> GetUserByIdNoTracking(string id);
        bool Update(AppUser user);
        bool Save();
    }
}
