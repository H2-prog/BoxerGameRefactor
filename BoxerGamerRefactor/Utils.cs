using System;
using System.Linq;
using System.Collections.Generic;

namespace BoxerGamerRefactor
{
    public static class Utils
    {
        private static Random _random = new Random();

        public static int GetRandomIndexOfCollection<T>(IEnumerable<T> collection)
        {
            return _random.Next(0, collection.Count());
        }

        public static double CalculatePercent(double a, double b)
        {
            return Math.Ceiling(a / b * 100); 
        }

        public static int CalculateRegen(Boxer boxer)
        {
            var stamina = Math.Clamp(boxer.Stamina, 0, boxer.StartStamina);
            return _random.Next(0, stamina);
        }

        public static int CalculateDamage(Boxer boxer, int modifier)
        {
            return _random.Next(boxer.Strength + modifier);
        }
    }
}
