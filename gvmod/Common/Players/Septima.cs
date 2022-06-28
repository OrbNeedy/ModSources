using Terraria;

namespace gvmod.Common.Players
{
    public abstract class Septima
    {
        public AdeptPlayer adept;
        public Player player;
        public int secondaryDuration;
        protected Septima(AdeptPlayer adept, Player player)
        {
            this.adept = adept;
            this.player = player;
        }

        public abstract void FirstAbilityEffects();

        public abstract void FirstAbility();

        public abstract void SecondAbilityEffects();

        public abstract void SecondAbility();

        public abstract void MiscEffects();

        public abstract void Updates();

        public abstract float SpUsage { get; set; }

        public abstract string Name { get; }

        public abstract int SecondaryCooldownTime { get; set; }
    }
}
