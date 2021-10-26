using System;

namespace BoxerGamerRefactor
{
    public static class Utils
    {
        private static Random _random = new Random();

        public static int CalculateRegen(Boxer boxer)
        {
            return _random.Next(0, boxer.Stamina);
        }

        public static int CalculateDamage(Boxer boxer, int modifier)
        {
            return _random.Next(boxer.Strength + modifier);
        }
    }
}
