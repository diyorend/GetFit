using GetFit.Models;

namespace GetFit.ViewModels
{
    public class UserDetailVM
    {
        public string Id { get; set; }
        public string Name { get; set; }
        public string UserName { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? ProfileImageUrl { get; set; }
        public ICollection<Gym>? Gyms { get; set; }
    }
}
