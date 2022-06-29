using Terraria.ModLoader;

namespace gvmod.Common.Systems
{
	public class KeybindSystem : ModSystem
	{
		public static ModKeybind primaryAbility { get; private set; }
		public static ModKeybind secondaryAbility { get; private set; }
		public static ModKeybind abilityMenu { get; private set; }

		public override void Load()
		{
			primaryAbility = KeybindLoader.RegisterKeybind(Mod, "Primary septimal ability", "F");
			secondaryAbility = KeybindLoader.RegisterKeybind(Mod, "Secondary septimal ability/cooldown time", "Q");
			abilityMenu = KeybindLoader.RegisterKeybind(Mod, "Ability menu(Soon)", "P");
		}

		public override void Unload()
		{
			primaryAbility = null;
			secondaryAbility = null;
			abilityMenu = null;
		}
	}
}
