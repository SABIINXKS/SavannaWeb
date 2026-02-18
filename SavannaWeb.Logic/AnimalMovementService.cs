using System;
using System.Collections.Generic;
using SavannaWeb.Models;

namespace SavannaWeb.Logic
{
    public class AnimalMovementService
    {
        private static readonly Random _random = new();

        /// <summary>
        /// Moves each animal in the list by a random step (-1, 0, or 1) in both X and Y directions.
        /// Ensures animals stay within the grid boundaries defined by maxX and maxY.
        /// </summary>
        /// <param name="animals">The list of animals to move.</param>
        /// <param name="maxX">The width of the grid (maximum X coordinate + 1).</param>
        /// <param name="maxY">The height of the grid (maximum Y coordinate + 1).</param>
        public void MoveAnimals(List<Animal> animals, int maxX, int maxY)
        {
            foreach (var animal in animals)
            {
                int dx = _random.Next(-1, 2);
                int dy = _random.Next(-1, 2);

                animal.X = Math.Clamp(animal.X + dx, 0, maxX - 1);
                animal.Y = Math.Clamp(animal.Y + dy, 0, maxY - 1);
            }
        }
    }
}