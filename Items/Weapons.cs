using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ActiveSummonWeapons.Items
{
	public class BubbleWand : ModItem
	{
        private const int BASE_DAMAGE = 10;
        private const int BASE_SPEED = 15;

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Spirit Wand");
			Tooltip.SetDefault("Summons spirits that travel to the targeted location!\nDamage and cast speed scale uniquely with summon damage.\n(Damage updates when held)");
			Item.staff[item.type] = true;
		}
		public override void SetDefaults()
		{
            item.damage = BASE_DAMAGE;
			item.summon = true;
			item.mana = 5;
			item.width = 40;
			item.height = 40;
			item.useTime = BASE_SPEED;
			item.useAnimation = BASE_SPEED;
			item.useStyle = 5;
			item.noMelee = true; //so the item's animation doesn't do damage
			item.knockBack = 5;
			item.value = 10000;
			item.rare = 2;
			item.UseSound = SoundID.Item20;
			item.autoReuse = true;
            item.shoot = mod.ProjectileType<Projectiles.SpiritProj>();
            item.shootSpeed = 5f;
		}

        public override void GetWeaponDamage(Player player, ref int damage)
        {
            item.damage = (int)(Math.Pow(1 + ((player.minionDamage-1)*0.6), 2) * BASE_DAMAGE);
            item.useTime = (int)(Math.Max(5, Math.Round(BASE_SPEED / player.minionDamage)));

            base.GetWeaponDamage(player, ref damage); 
        }

        public override bool Shoot(Player player, ref Vector2 position, ref float speedX, ref float speedY, ref int type, ref int damage, ref float knockBack)
        {
            if (player.direction > 0)
                position = player.Right;
            else
                position = player.Left;

            Vector2 vel = new Vector2(speedX, speedY);
            Vector2 target = Main.MouseWorld;
            int id = Projectile.NewProjectile(position, vel, type, damage, knockBack, player.whoAmI, target.X, target.Y);

            return false; //prevent default
        }

        public override void AddRecipes()
		{
			ModRecipe recipe = new ModRecipe(mod);
			recipe.AddIngredient(ItemID.Wood, 10);
            recipe.AddIngredient(ItemID.Gel, 1);
            recipe.AddTile(TileID.WorkBenches);
			recipe.SetResult(this);
			recipe.AddRecipe();
		}
    }
}
