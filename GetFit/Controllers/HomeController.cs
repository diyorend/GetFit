using GetFit.Helper;
using GetFit.Interfaces;
using GetFit.Models;
using GetFit.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using System.Diagnostics;
using System.Globalization;
using System.Net;

namespace GetFit.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly IGymRepository _gymRepository;
        private readonly IUserRepository _userRepository;

        public HomeController(ILogger<HomeController> logger,
            IGymRepository gymRepository,
            IUserRepository userRepository)
        {
            _logger = logger;
            _gymRepository = gymRepository;
            _userRepository = userRepository;
        }

        public async Task<IActionResult> Index()
        {
            var ipInfo = new IPInfo();
            var users = await _userRepository.GetAllUsersAsync();
            
            if (users.Count() > 5) 
            {
                users.ToList().RemoveRange(5,users.Count() - 5);
            }
            var homeVM = new HomeVM()
            {
                Users = users.ToList(),
            };
            
            try
            {
                string url = "https://ipinfo.io?token=af247e51fbe8e2";
                var info = new WebClient().DownloadString(url);
                ipInfo = JsonConvert.DeserializeObject<IPInfo>(info);
                RegionInfo myRI1 = new RegionInfo(ipInfo.Country);
                ipInfo.Country = myRI1.EnglishName;
                homeVM.City = ipInfo.City;
                homeVM.State = ipInfo.Region;
                if(homeVM.City != null)
                {
                    homeVM.Gyms = await _gymRepository.GetGymByCity(homeVM.City);

                }
                else
                {
                    homeVM.Gyms = null;
                }
                return View(homeVM);
            }
            catch (Exception ex)
            {
                homeVM.Gyms = null;
            }
            return View(homeVM);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

    }
}