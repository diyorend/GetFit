using GetFit.Models;

namespace GetFit.Interfaces
{
    public interface IDashboardRepository
    {
        Task<List<Home>> GetAllUserHomes();
        Task<List<Gym>> GetAllUserGyms();

    }
}
