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
        public int StartHealth { get; set; }
        public int Strength { get; set; }
        public int Stamina { get; set; }
        public int StartStamina { get; set; }
        public int Victories { get; set; }
        public bool Knockedout { get => Health <= 0; }

        public BoxerRegenResult Regen()
        {
            var regen = Utils.CalculateRegen(this);
            var minHealth = (int)Math.Ceiling((double)StartHealth / 2.0);
            Health +=  Math.Clamp(regen, minHealth, StartHealth);
            Stamina -=  Math.Clamp(regen, 0, StartStamina);
            return new BoxerRegenResult { NewHealth = Health, RegenAmount = regen };
        }

        public BoxerDamageResult Damage(int modifier)
        {
            var damage = Utils.CalculateDamage(this, modifier);
            Health -= damage;
            return new BoxerDamageResult { NewHealth = Health, Damage = damage };
        }
    }
}
