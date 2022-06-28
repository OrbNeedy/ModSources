using Terraria.ModLoader;

namespace gvmod.Common.Systems
{
	public class KeybindSystem : ModSystem
	{
		public static ModKeybind primaryAbility { get; private set; }
		public static ModKeybind secondaryAbility { get; private set; }
		public static ModKeybind barReposition { get; private set; }

		public override void Load()
		{
			primaryAbility = KeybindLoader.RegisterKeybind(Mod, "Primary septimal ability", "F");
			secondaryAbility = KeybindLoader.RegisterKeybind(Mod, "Secondary septimal ability/cooldown time", "Q");
			barReposition = KeybindLoader.RegisterKeybind(Mod, "Bar reposition mode(WIP)", "P");
		}

		public override void Unload()
		{
			primaryAbility = null;
			secondaryAbility = null;
			barReposition = null;
		}
	}
}
