using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Buffs.Minions;
using TerrariaParadox.Content.Projectiles.Summon.Minions;

namespace TerrariaParadox.Content.Items.Weapons.Summon.Minions
{
    public class Suspicious8Ball : ModdedMinionItem
    {
        public const float MinionSlots = 1;
        public const float SummonTagMult = 100;
        public override float SlotsRequired => MinionSlots;
        public override int BaseDmg => 35;
        public override float BaseKnockback => 4f;
        public override int ItemRarity => ItemRarityID.LightRed;
        public override int ItemValue => PriceByRarity.fromItem(Item);
        public override int ManaCost => 17;
        public override SoundStyle UseSound => SoundID.Item2;
        public override int MinionBuffType => ModContent.BuffType<Suspicious8BallBuff>();
        public override int MinionProjectileType => ModContent.ProjectileType<Suspicious8BallProjectile>();
        public override float SummonTagDmgPercentage => SummonTagMult;

        public override void AddRecipes()
        {
            CreateRecipe().
                AddIngredient(ItemID.SoulofNight, 5).
                AddIngredient(ItemID.JojaCola).
                AddRecipeGroup("AnyCobaltBar", 5).
                AddTile(TileID.Anvils).
                Register();
        }
    }
}