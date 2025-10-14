using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SavannaWeb.Models;
using SavannaWeb.DataAccess;
using SavannaWeb.Logic;
using System.Threading.Tasks;
using System.Collections.Generic;

namespace SavannaWeb.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly GameSaveService _gameSaveService;
        private const int GridWidth = 10;
        private const int GridHeight = 10;

        // For demo: static animals list (replace with session or DB for multi-user)
        private static List<Animal> _animals = SavannaGridHelper.GetInitialAnimals();

        /// <summary>
        /// Initializes a new instance of the <see cref="GameController"/> class.
        /// </summary>
        public GameController(UserManager<IdentityUser> userManager, GameSaveService gameSaveService)
        {
            _userManager = userManager;
            _gameSaveService = gameSaveService;
        }

        /// <summary>
        /// Displays the main game page with the list of saved games for the current user.
        /// </summary>
        /// <returns>The Index view with the user's saved games.</returns>
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var saves = await _gameSaveService.GetUserSavesAsync(user.Id);

            ViewBag.Saves = saves;
            return View();
        }

        /// <summary>
        /// Saves the current game state for the user.
        /// </summary>
        /// <param name="saveData">The game state data to save (as JSON string).</param>
        /// <returns>Redirects to the Index view after saving.</returns>
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

        /// <summary>
        /// Loads a saved game by its ID for the current user.
        /// </summary>
        /// <param name="id">The ID of the saved game to load.</param>
        /// <returns>The Index view with the loaded game data, or NotFound if not found.</returns>
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

        /// <summary>
        /// Displays the Savanna grid with the current positions of all animals.
        /// </summary>
        /// <returns>The Grid view with animal positions.</returns>
        public IActionResult Grid()
        {
            ViewBag.GridWidth = GridWidth;
            ViewBag.GridHeight = GridHeight;
            ViewBag.Animals = _animals;
            return View();
        }

        /// <summary>
        /// Moves all animals to new positions on the grid and displays the updated grid.
        /// </summary>
        /// <returns>Redirects to the Grid view after moving animals.</returns>
        [HttpPost]
        public IActionResult MoveAnimals()
        {
            var movementService = new AnimalMovementService();
            movementService.MoveAnimals(_animals, GridWidth, GridHeight);
            return RedirectToAction("Grid");
        }
    }
}
