namespace GetFit.ViewModels
{
    public class EditUserDashboardVM
    {
        public string Id { get; set; }
        public string? Name { get; set; }
        public int? Height { get; set; }
        public int? Weight { get; set; }
        public string? City { get; set; }
        public string? State { get; set; }
        public IFormFile Image { get; set; }
        public string? ProfileImageUrl { get; set; }
        public string UserName { get; set; }



    }
}
