using CloudinaryDotNet.Actions;
using GetFit.Data;
using GetFit.Interfaces;
using GetFit.Models;
using GetFit.ViewModels;
using Microsoft.AspNetCore.Mvc;

namespace GetFit.Controllers
{
    public class DashboardController : Controller
    {
        private readonly IDashboardRepository _dashboardRepository;
        private readonly IHttpContextAccessor _httpContextAccessor;
        private readonly IPhotoService _photoService;

        public DashboardController(
            IDashboardRepository dashboardRepository,
            IHttpContextAccessor httpContextAccessor,
            IPhotoService photoService)
        {
            _dashboardRepository = dashboardRepository;
            _httpContextAccessor = httpContextAccessor;
            _photoService = photoService;
        }

        private void MapUserEdit(
            AppUser user,
            EditUserDashboardVM editUserVM,
            ImageUploadResult photoResult)
        {
            user.Id = editUserVM.Id;
            user.Weight = editUserVM.Weight;
            user.Height = editUserVM.Height;
            user.ProfileImageUrl = photoResult.Url.ToString();
            user.City = editUserVM.City;
            user.State = editUserVM.State;

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
        public async Task<IActionResult> EditUserProfile()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var user = await _dashboardRepository.GetUserById(currentUserId);
            if (user == null) return View("Error");
            var editUserVM = new EditUserDashboardVM()
            {
                Id = currentUserId,
                Weight = user.Weight,
                Height = user.Height,
                ProfileImageUrl = user.ProfileImageUrl,
                City = user.City,
                State = user.State,
            };
            return View(editUserVM);
        }

        [HttpPost]
        public async Task<IActionResult> EditUserProfile(EditUserDashboardVM editUserVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit profile");
                return View("EditUserProfile", editUserVM);
            }


            var user = await _dashboardRepository.GetUserByIdNoTracking(editUserVM.Id);
            //photo
            if (user.ProfileImageUrl == null || user.ProfileImageUrl == "")
            {
                var photoResult = await _photoService.AddPhotoAsync(editUserVM.Image);
                //optimistic cuncurrency --"Tracking error"
                // use no tracing
                // map dto without 
                MapUserEdit(user, editUserVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");
            }
            else
            {
                try
                {
                    await _photoService.DeletePhotoAsync(user.ProfileImageUrl);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(editUserVM);
                }

                var photoResult = await _photoService.AddPhotoAsync(editUserVM.Image);
                
                MapUserEdit(user, editUserVM, photoResult);

                _dashboardRepository.Update(user);
                return RedirectToAction("Index");

            }
        }
    }
}
