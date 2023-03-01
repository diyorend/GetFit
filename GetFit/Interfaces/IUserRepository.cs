using GetFit.Models;

namespace GetFit.Interfaces
{
    public interface IUserRepository
    {
        Task<IEnumerable<AppUser>> GetAllUsersAsync();
        Task<AppUser> GetUserById(string id);
        bool Save();
    }
}
