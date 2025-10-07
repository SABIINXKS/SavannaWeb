using System;
using System.ComponentModel.DataAnnotations;
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

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        public void SerializeSaveData(int iteration, int livingAnimals, (string species, int age, int health, int offspringCount)[] animals)
        {
            var saveData = new
            {
                iteration,
                livingAnimals,
                animals = animals.Select(a => new
                {
                    species = a.species,
                    age = a.age,
                    health = a.health,
                    offspringCount = a.offspringCount
                }).ToArray()
            };
            SaveData = JsonSerializer.Serialize(saveData);
        }
    }
}
