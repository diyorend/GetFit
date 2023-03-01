using GetFit.Models;

namespace GetFit.ViewModels
{
    public class DashboardVM
    {
        public List<Gym> Gyms { get; set; } 
        public List<Home> Homes { get; set; }
        public AppUser AppUser { get; set; }
    }
}
