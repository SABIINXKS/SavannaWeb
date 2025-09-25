using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text.Json;

namespace SavannaWeb.Models
{
    public class GameSave
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public string UserId { get; set; }

        [Required]
        public string SaveData { get; set; } // JSON string for simplicity

        public DateTime SavedAt { get; set; } = DateTime.Now;

        public void SerializeSaveData(int iteration, int livingAnimals, (string type, int health, int age)[] animals)
        {
            var saveData = new
            {
                iteration,
                livingAnimals,
                animals
            };
            SaveData = JsonSerializer.Serialize(saveData);
        }
    }
}
