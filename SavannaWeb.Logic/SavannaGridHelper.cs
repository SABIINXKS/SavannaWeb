using System.Collections.Generic;
using SavannaWeb.Models;

namespace SavannaWeb.Logic
{
    public static class SavannaGridHelper
    {
        public static (int gridWidth, int gridHeight, List<Animal> animals) GetGridData()
        {
            int gridWidth = 10;
            int gridHeight = 10;
            var animals = new List<Animal>
            {
                new Animal { Species = "Lion", X = 2, Y = 3 },
                new Animal { Species = "Zebra", X = 5, Y = 7 },
                new Animal { Species = "Elephant", X = 1, Y = 1 }
            };

            return (gridWidth, gridHeight, animals);
        }
    }
}