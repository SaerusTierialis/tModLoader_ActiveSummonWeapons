using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ActiveSummonWeapons.Projectiles
{
    class Helpers
    {
        public static void HomeOnto(Projectile projectile, Vector2 target, float velocity_adjust, float velocity_max)
        {
            int direction;

            direction = projectile.position.X < target.X ? 1 : -1;
            projectile.velocity.X += direction * velocity_adjust;
            if (projectile.velocity.X > velocity_max) projectile.velocity.X = velocity_max;
            if (projectile.velocity.X < -velocity_max) projectile.velocity.X = -velocity_max;


            direction = projectile.position.Y < target.Y ? 1 : -1;
            projectile.velocity.Y += direction * velocity_adjust;
            if (projectile.velocity.Y > velocity_max) projectile.velocity.Y = velocity_max;
            if (projectile.velocity.Y < -velocity_max) projectile.velocity.Y = -velocity_max;
        }
    }
}
