using System.Collections.Generic;
using SavannaWeb.Models;
using System.Text.Json;

namespace SavannaWeb.Logic
{
    public static class SavannaGridHelper
    {
        /// <summary>
        /// Returns a list of initial animals with their species and starting positions on the grid.
        /// This can be used to populate the Savanna grid at the start of the simulation.
        /// </summary>
        /// <returns>A list of Animal objects with predefined species and coordinates.</returns>
        public static List<Animal> GetInitialAnimals()
        {
            return new List<Animal>
            {
                new Animal { Species = "Lion", X = 2, Y = 3 },
                new Animal { Species = "Zebra", X = 5, Y = 7 },
                new Animal { Species = "Elephant", X = 1, Y = 1 }
            };
        }

        public static (int gridWidth, int gridHeight, List<Animal> animals) GetGridData(string saveData)
        {
            // This assumes saveData is a JSON object with gridWidth, gridHeight, and animals properties
            if (string.IsNullOrEmpty(saveData))
                return (10, 10, new List<Animal>());

            var doc = JsonDocument.Parse(saveData);
            int gridWidth = doc.RootElement.GetProperty("gridWidth").GetInt32();
            int gridHeight = doc.RootElement.GetProperty("gridHeight").GetInt32();
            var animals = JsonSerializer.Deserialize<List<Animal>>(doc.RootElement.GetProperty("animals").GetRawText())
                          ?? new List<Animal>();
            return (gridWidth, gridHeight, animals);
        }
    }
}