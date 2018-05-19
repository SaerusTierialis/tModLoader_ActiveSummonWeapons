using Microsoft.Xna.Framework;
using Terraria;
using Terraria.ID;
using Terraria.ModLoader;

namespace ActiveSummonWeapons.Projectiles
{
	public class SpiritProj : HomingProjPosition
    {
        public override string Texture
        {
            get
            {
                return mod.Name + "/Projectiles/Placeholder";
            }
        }

        public override void SetStaticDefaults()
		{
			DisplayName.SetDefault("Summoned Spirit");
            //ProjectileID.Sets.Homing[projectile.type] = true;
        }

		public override void SetDefaults()
		{
			projectile.width = 15;
			projectile.height = 15;
			projectile.timeLeft = 300;
			projectile.penetrate = 1; 
            projectile.friendly = true;
			projectile.ignoreWater = true;
            projectile.tileCollide = true;
        }

        public override void AI()
        {
            int i = Dust.NewDust(projectile.Center, 0, 0, DustID.WitherLightning, 0, 0, 0, Color.Purple);
            Main.dust[i].noGravity = true;
            base.AI();
        }
    }

    public abstract class HomingProjPosition : ModProjectile
    {
        public Vector2 target;
        public float velocity_adjust = 0.5f;
        public float velocity_max = 5f;
        public bool initialized = false;

        public override void AI()
        {
            if (!initialized)
            {
                target = new Vector2(projectile.ai[0], projectile.ai[1]);
                initialized = true;
            }
            else
            {
                Helpers.HomeOnto(projectile, target, velocity_adjust, velocity_max);
            }
        }
    }

    public abstract class HomingProjReturn : HomingProj
    {
        public override void AI()
        {
            if (base.target == null)
            {
                //follow owner
                Helpers.HomeOnto(projectile, Main.player[projectile.owner].position, velocity_adjust, velocity_max);
            }
            base.AI();
        }
    }


    public abstract class HomingProj : ModProjectile
    {
        public NPC target;
        public float homing_range = 400f;
        public float velocity_adjust = 0.5f;
        public float velocity_max = 5f;

        public override void SetStaticDefaults()
        {
            ProjectileID.Sets.Homing[projectile.type] = true;
            target = null;
        }

        public override void AI()
        {
            Player owner = Main.player[projectile.owner];
            if (target == null)
            {
                //try to get new target
                NPC check;
                float min_distance = homing_range;
                float distance;
                for (int i=0; i<Main.npc.Length; i++)
                {
                    check = Main.npc[i];
                    if (!check.friendly & check.active)
                    {
                        distance = owner.Distance(check.position);
                        if (distance <= min_distance)
                        {
                            target = check;
                            min_distance = distance;
                        }
                    }
                }
            }
            else
            {
                //target still alive and nearby?
                if (target.active & owner.Distance(target.position) < homing_range)
                {
                    //follow target
                    Helpers.HomeOnto(projectile, target.position, velocity_adjust, velocity_max);
                }
                else
                {
                    //need new target
                    target = null;
                }
            }
        }
    }
}