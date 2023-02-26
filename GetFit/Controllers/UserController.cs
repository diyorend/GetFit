using GetFit.Interfaces;
using GetFit.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GetFit.Controllers
{
    public class UserController : Controller
    {
        private readonly IUserRepository _userRepository;

        public UserController(IUserRepository userRepository) 
        {
            _userRepository = userRepository;
        }
        [HttpGet("users")]
        public async Task<IActionResult> Index()
        {
            var users = await _userRepository.GetAllUsersAsync();
            List<UserVM> result = new List<UserVM>();
            foreach (var user in users)
            {
                var userVM = new UserVM()
                {
                    UserName = user.UserName,
                    Id= user.Id,
                    Height= user.Height,
                    Weight= user.Weight,
                    ProfileImageUrl = user.ProfileImageUrl,
                };
                result.Add(userVM);
            };
            return View(result);
        }

        public async Task<IActionResult> Detail(string id)
        {
            var user = await _userRepository.GetUserById(id);
            var userDetailVM = new UserDetailVM()
            {
                Id = user.Id,
                UserName = user.UserName,
                Weight = user.Weight,
                Height = user.Height,
            };
            return View(userDetailVM);
        }
    }
}
