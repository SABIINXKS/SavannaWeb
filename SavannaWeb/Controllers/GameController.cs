using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using SavannaWeb.Data;
using SavannaWeb.Models;
using System.Linq;
using System.Threading.Tasks;

namespace SavannaWeb.Controllers
{
    [Authorize]
    public class GameController : Controller
    {
        private readonly UserManager<IdentityUser> _userManager;
        private readonly ApplicationDbContext _context;

        public GameController(UserManager<IdentityUser> userManager, ApplicationDbContext context)
        {
            _userManager = userManager;
            _context = context;
        }

        // Spēles galvenā lapa ar sarakstu
        public async Task<IActionResult> Index()
        {
            var user = await _userManager.GetUserAsync(User);
            var saves = _context.GameSaves
                .Where(g => g.UserId == user.Id)
                .OrderByDescending(g => g.SavedAt)
                .ToList();

            ViewBag.Saves = saves;
            return View();
        }

        // Saglabā spēli (POST)
        [HttpPost]
        public async Task<IActionResult> SaveGame(string saveData)
        {
            var user = await _userManager.GetUserAsync(User);

            var gameSave = new GameSave
            {
                UserId = user.Id,
                SaveData = saveData
            };

            _context.GameSaves.Add(gameSave);
            await _context.SaveChangesAsync();

            return RedirectToAction("Index");
        }

        // Ielādē spēli (GET)
        public async Task<IActionResult> LoadGame(int id)
        {
            var user = await _userManager.GetUserAsync(User);
            var save = _context.GameSaves.FirstOrDefault(s => s.Id == id && s.UserId == user.Id);

            if (save == null)
                return NotFound();

            ViewBag.LoadedGame = save.SaveData;
            var saves = _context.GameSaves
                .Where(g => g.UserId == user.Id)
                .OrderByDescending(g => g.SavedAt)
                .ToList();
            ViewBag.Saves = saves;

            return View("Index");
        }
    }
}
