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

        public DateTime SavedAt { get; set; } = DateTime.UtcNow;

        /// <summary>
        /// Serializes the game state to JSON, including animal species, age, health, and offspring count.
        /// </summary>
        /// <param name="iteration">Current game iteration.</param>
        /// <param name="livingAnimals">Number of living animals.</param>
        /// <param name="animals">Array of animal statistics (species, age, health, offspringCount).</param>
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
