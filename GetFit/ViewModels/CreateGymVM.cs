
using GetFit.Data.Enum;
using GetFit.Models;

namespace GetFit.ViewModels
{
    public class CreateGymVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public Address Address { get; set; }
        public IFormFile Image { get; set; }
        public GymCategory GymCategory { get; set; }
        public string AppUserId { get; set; }
    }
}
