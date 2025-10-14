using System.Collections.Generic;
using SavannaWeb.Models;

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
    }
}