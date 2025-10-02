using System;
using System.Collections.Generic;
using SavannaWeb.Models;

namespace SavannaWeb.Logic
{
    public class AnimalMovementService
    {
        private static readonly Random _random = new();

        public void MoveAnimals(List<Animal> animals, int maxX, int maxY)
        {
            foreach (var animal in animals)
            {
                // Randomly move animal by -1, 0, or 1 in X and Y
                int dx = _random.Next(-1, 2);
                int dy = _random.Next(-1, 2);

                animal.X = Math.Clamp(animal.X + dx, 0, maxX - 1);
                animal.Y = Math.Clamp(animal.Y + dy, 0, maxY - 1);
            }
        }
    }
}