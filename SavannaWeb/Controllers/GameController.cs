using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SavannaWeb.Models;
using SavannaWeb.DataAccess;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SavannaWeb.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GameSaveService _gameSaveService;

        public GameController(UserManager<IdentityUser> userManager, GameSaveService gameSaveService)
        {
            _userManager = userManager;
            _gameSaveService = gameSaveService;
        }

        // Main game page that displays the list of saved games
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var saves = await _gameSaveService.GetUserSavesAsync(user.Id);

            ViewBag.Saves = saves;
            return View();
        }

        // Saves a game (POST request)
        [HttpPost]
        public async Task<IActionResult> SaveGame(string saveData)
        {
            var user = await _userManager.GetUserAsync(User);

            var gameSave = new GameSave
            {
                UserId = user.Id,
                SaveData = saveData
            };

            await _gameSaveService.AddGameSaveAsync(gameSave);

            return RedirectToAction("Index");
        }

        // Loads a game by ID (GET request)
        public async Task<IActionResult> LoadGame(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var save = await _gameSaveService.GetUserSaveByIdAsync(id, user.Id);

            if (save == null)
                return NotFound();

            ViewBag.LoadedGame = save.SaveData;

            var saves = await _gameSaveService.GetUserSavesAsync(user.Id);
            ViewBag.Saves = saves;

            return View("Index");
        }

        // Displays the game grid
        public IActionResult Grid()
        {
            int gridWidth = 10;
            int gridHeight = 10;
            var animals = new List<Animal>
            {
                new Animal { Species = "Lion", X = 2, Y = 3 },
                new Animal { Species = "Zebra", X = 5, Y = 7 }
                // ...add more animals as needed
            };

            ViewBag.GridWidth = gridWidth;
            ViewBag.GridHeight = gridHeight;
            ViewBag.Animals = animals;

            return View();
        }
    }
}
