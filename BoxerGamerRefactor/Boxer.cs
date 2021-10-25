using System;

namespace BoxerGamerRefactor
{
    public class BoxerDamageResult
    {
        public int NewHealth { get; set; }
        public int Damage { get; set; }
    }

    public class BoxerRegenResult
    {
        public int NewHealth { get; set; }
        public int RegenAmount { get; set; }
    }

    public class Boxer
    {
        public string Name { get; set; }
        public int Health { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int Victories { get; set; }

        private Random random;

        public Boxer(Random random)
        {
            this.random = random;
        }

        public BoxerRegenResult Regen()
        {
            var regen = CalculateRegen();
            Health += regen;
            return new BoxerRegenResult { NewHealth = Health, RegenAmount = regen };
        }

        public BoxerDamageResult Damage(int modifier)
        {
            var damage = CalculateDamage(modifier);
            Health -= damage;
            return new BoxerDamageResult { NewHealth = Health, Damage = damage };
        }

        private int CalculateRegen()
        {
            return random.Next(0, Stamina); 
        }

        private int CalculateDamage(int modifier)
        {
            return random.Next(Strength + modifier);
        }
    }
}
