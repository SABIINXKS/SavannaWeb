using System.Collections.Generic;
using SavannaWeb.Models;

namespace SavannaWeb.Logic
{
    public static class SavannaGridHelper
    {
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