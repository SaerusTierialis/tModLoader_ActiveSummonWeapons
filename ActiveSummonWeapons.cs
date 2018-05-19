using Terraria.ModLoader;

namespace ActiveSummonWeapons
{
	class ActiveSummonWeapons : Mod
	{
		public ActiveSummonWeapons()
		{
			Properties = new ModProperties()
			{
				Autoload = true,
				AutoloadGores = true,
				AutoloadSounds = true
			};
		}
	}
}
