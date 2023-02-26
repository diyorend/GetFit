using GetFit.Data;
using GetFit.Interfaces;
using GetFit.Models;
using GetFit.ViewModels;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace GetFit.Controllers
{
    public class GymController : Controller
    {
        private readonly IGymRepository _gymRepository;
        private readonly IPhotoService _photoService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public GymController( IGymRepository gymRepository, IPhotoService photoService, IHttpContextAccessor httpContextAccessor)
        {
            _gymRepository = gymRepository;
            _photoService = photoService;
            _httpContextAccessor = httpContextAccessor;
        }
        public async Task<IActionResult> Index() 
        {
            IEnumerable<Gym> gyms = await _gymRepository.GetAll();
            return View(gyms);
        }

        public async Task<IActionResult> Detail(int id)
        {
            Gym gym = await _gymRepository.GetByIdAsync(id);
            return View(gym);
        }

        public IActionResult Create()
        {
            var currentUserId = _httpContextAccessor.HttpContext.User.GetUserId();
            var createGymVM = new CreateGymVM { AppUserId = currentUserId};
            return View(createGymVM);
        }

        [HttpPost]
        public async Task<IActionResult> Create(CreateGymVM gymVM)
        {
            if (ModelState.IsValid)
            {
                var result = await _photoService.AddPhotoAsync(gymVM.Image);

                var gym = new Gym
                {
                    Title = gymVM.Title,
                    Description = gymVM.Description,
                    Image = result.Url.ToString(),
                    AppUserId = gymVM.AppUserId,
                    Address = new Address
                    {
                        Street = gymVM.Address.Street,
                        City = gymVM.Address.City,
                        State = gymVM.Address.State,
                    }
                };
                _gymRepository.Add(gym);
                return RedirectToAction("Index");
            }
            else
            {
                ModelState.AddModelError("", "Photo upload is failed");
                
            }
            return View(gymVM); 
        }

        public async Task<IActionResult> Edit(int id)
        {
            var gym = await _gymRepository.GetByIdAsync(id);
            if (gym == null) 
                return View("Error");
            var gymVM = new EditGymVM
            {
                Title = gym.Title,
                Description = gym.Description,
                Address = gym.Address,
                AddressId = gym.AddressId,
                Url = gym.Image,
                GymCategory = gym.GymCategory
            };
            return View(gymVM);
        }

        [HttpPost]
        public async Task<IActionResult> Edit(int id, EditGymVM gymVM)
        {
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError("", "Failed to edit gym.");
                return View("Edit", gymVM);
            }
            var userGym = await _gymRepository.GetByIdAsyncNoTracking(id);

            if(userGym != null)
            {
                try
                {
                    await _photoService.DeletePhotoAsync(userGym.Image);
                }
                catch(Exception ex)
                {
                    ModelState.AddModelError("", "Could not delete photo");
                    return View(gymVM);
                }
                var photoResult = await _photoService.AddPhotoAsync(gymVM.Image);
                
                var gym = new Gym
                {
                    Id = id,
                    Title = gymVM.Title,
                    Description = gymVM.Description,
                    Image = photoResult.Url.ToString(),
                    AddressId = gymVM.AddressId,
                    Address = gymVM.Address,
                };

                _gymRepository.Update(gym);
                return RedirectToAction("Index");
            }
            return View(gymVM);
        }

        public async Task<IActionResult> Delete(int id)
        {
            var gymDetails = await _gymRepository.GetByIdAsync(id);
            if(gymDetails == null)
                return View("Error");
            return View(gymDetails);
        }

        [HttpPost, ActionName("Delete")]
        public async Task<IActionResult> DeleteClub(int id)
        {
            var gymDetails = await _gymRepository.GetByIdAsync(id);
            if (gymDetails == null)
                return View("Error");

             _gymRepository.Delete(gymDetails);
            return RedirectToAction("Index");
        }

        

    }
}
