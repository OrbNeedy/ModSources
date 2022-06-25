using Terraria;
using Terraria.GameInput;

namespace gvmod.Common.Players
{
    public abstract class Septima
    {
        protected Player player = null;
        protected AdeptPlayer adept = null;

        public Septima(Player player, AdeptPlayer adept)
        {
            this.player = player;
            this.adept = adept;
        }

        public abstract void FirstAbilityEffects();

        public abstract void FirstAbility();

        public abstract void SecondAbilityEffects();

        public abstract void SecondAbility();

        public abstract float SpUsage { get; }

        public abstract string Name { get; }
    }
}
