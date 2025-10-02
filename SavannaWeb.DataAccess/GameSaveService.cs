using Microsoft.EntityFrameworkCore;
using SavannaWeb.Models;
using SavannaWeb.Data;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SavannaWeb.DataAccess
{
    public class GameSaveService
    {
        private readonly ApplicationDbContext _context;

        public GameSaveService(ApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<List<GameSave>> GetUserSavesAsync(string userId)
        {
            return await _context.GameSaves
                .Where(gs => gs.UserId == userId)
                .OrderByDescending(gs => gs.SavedAt)
                .ToListAsync();
        }

        public async Task AddGameSaveAsync(GameSave gameSave)
        {
            _context.GameSaves.Add(gameSave);
            await _context.SaveChangesAsync();
        }

        public async Task<GameSave?> GetUserSaveByIdAsync(int id, string userId)
        {
            return await _context.GameSaves
                .FirstOrDefaultAsync(gs => gs.Id == id && gs.UserId == userId);
        }
    }
}