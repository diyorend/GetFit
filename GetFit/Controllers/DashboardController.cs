using GetFit.Data;
using GetFit.Interfaces;
using GetFit.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GetFit.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;

        public DashboardController(IDashboardRepository dashboardRepository)
        {
            _dashboardRepository = dashboardRepository;
        }
        public async Task<IActionResult> Index()
        {
            var userGyms = await _dashboardRepository.GetAllUserGyms();
            var userHomes = await _dashboardRepository.GetAllUserHomes();

            var dashboardVM = new DashboardVM()
            {
                Homes = userHomes,
                Gyms = userGyms,
            };

            return View(dashboardVM);
        }
    }
}
