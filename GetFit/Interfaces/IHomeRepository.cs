using GetFit.Models;

namespace GetFit.Interfaces
{
    public interface IHomeRepository
    {
        Task<IEnumerable<Home>> GetAll();
        Task<Home> GetByIdAsync(int id);
        Task<Home> GetByIdAsyncNoTracking(int id);
        Task<IEnumerable<Home>> GetHomeByCity(string city);
        bool Add(Home home);
        bool Update(Home home);
        bool Delete(Home home);
        bool Save();
    }
}
