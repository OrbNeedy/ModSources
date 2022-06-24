using Terraria.ModLoader;

namespace gvmod.Common.Systems
{
	public class KeybindSystem : ModSystem
	{
		public static ModKeybind primaryAbility { get; private set; }
		public static ModKeybind secondaryAbility { get; private set; }

		public override void Load()
		{
			primaryAbility = KeybindLoader.RegisterKeybind(Mod, "Primary septimal ability", "F");
			secondaryAbility = KeybindLoader.RegisterKeybind(Mod, "Show SP stats", "Q");
		}

		public override void Unload()
		{
			primaryAbility = null;
		}
	}
}
