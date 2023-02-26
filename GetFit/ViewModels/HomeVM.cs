using GetFit.Models;

namespace GetFit.ViewModels
{
    public class HomeVM
    {
        public IEnumerable<Gym> Gyms { get; set; }
        public string City { get; set; }
        public string State { get; set; } 
    }
}
