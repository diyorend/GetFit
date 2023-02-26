using GetFit.Data;
using GetFit.Interfaces;
using GetFit.Models;
using GetFit.Repository;
using GetFit.Services;
using GetFit.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GetFit.Controllers
{
    public class HomeWorkoutController : Controller
    {
       
        private readonly IHomeRepository _homeRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public HomeWorkoutController(IHomeRepository homeRepository,
            IPhotoService photoService, 
            IHttpContextAccessor httpContextAccessor)
        {
            
            _homeRepository = homeRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index()
        {
            IEnumerable<Home> homeGroups = await _homeRepository.GetAll();
            return View(homeGroups);
        }
        public async Task<IActionResult> Detail(int id)
        {
            Home home =await _homeRepository.GetByIdAsync(id);
            return View(home);
        }

        public IActionResult Create()
        {
            var currentUsedId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createHomeVM =  new CreateHomeVM { AppUserId  = currentUsedId };
            return View(createHomeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateHomeVM homeVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(homeVM.Image);

                var home = new Home
                {
                    Title = homeVM.Title,
                    Description = homeVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = homeVM.AppUserId,
                    Address = new Address
                    {
                        Street = homeVM.Address.Street,
                        City = homeVM.Address.City,
                        State = homeVM.Address.State,
                    }
                };
                _homeRepository.Add(home);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload is failed");

            }
            return View(homeVM);
        }

        public async Task<IActionResult> Edit(int id)
        {
            var home = await _homeRepository.GetByIdAsync(id);
            if (home == null)
                return View("Error");
            var homeVM = new EditHomeVM
            {
                Title = home.Title,
                Description = home.Description,
                Address = home.Address,
                AddressId = home.AddressId,
                Url = home.Image,
                HomeCategory = home.HomeCategory
            };
            return View(homeVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditHomeVM homeVM)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit gym.");
                return View("Edit", homeVM);
            }
            var userHome = await _homeRepository.GetByIdAsyncNoTracking(id);

            if (userHome != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userHome.Image);
                }
                catch (Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(homeVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(homeVM.Image);

                var home = new Home
                {
                    Id = id,
                    Title = homeVM.Title,
                    Description = homeVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = homeVM.AddressId,
                    Address = homeVM.Address,
                };

                _homeRepository.Update(home);
                return RedirectToAction("Index");
            }
            return View(homeVM);
        }
        public async Task<IActionResult> Delete(int id)
        {
            var homeDetails = await _homeRepository.GetByIdAsync(id);
            if (homeDetails == null)
                return View("Error");
            return View(homeDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteHome(int id)
        {
            var homeDetails = await _homeRepository.GetByIdAsync(id);
            if (homeDetails == null)
                return View("Error");

            _homeRepository.Delete(homeDetails);
            return RedirectToAction("Index");
        }

    }
}
