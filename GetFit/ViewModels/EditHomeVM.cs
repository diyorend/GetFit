using GetFit.Data.Enum;
using GetFit.Models;

namespace GetFit.ViewModels
{
    public class EditHomeVM
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int AddressId { get; set; }
        public Address Address { get; set; }
        public string? Url { get; set; }
        public IFormFile Image { get; set; }
        public HomeCategory HomeCategory { get; set; }
    }
}
