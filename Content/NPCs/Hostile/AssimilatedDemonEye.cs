using MLib.Common.NPCs;
using Terraria;
using Terraria.Audio;
using Terraria.ID;
using Terraria.ModLoader;
using TerrariaParadox.Content.Biomes.TheFlipside;
using TerrariaParadox.Content.Items.Tiles.Banners;
using TerrariaParadox.Content.Tiles.Blocks;

namespace TerrariaParadox.Content.NPCs.Hostile;

public class AssimilatedDemonEye : ModdedHostileNPC
{
    public override int TotalAnimationFrames => 2;
    public override int Width => 38;
    public override int Height => 24;
    public override int BaseLifePoints => 70;
    public override int BaseDefense => 0;
    public override float BaseKnockbackReceivedMultiplier => 0f;
    public override int BaseContactDamage => 15;
    public override SoundStyle OnHitSound => SoundID.NPCDeath9;
    public override SoundStyle OnDeathSound => SoundID.NPCDeath11;
    public override int Value => 100;
    public override int BannerItemType => ModContent.ItemType<AssimilatedDemonEyeBanner>();
    
    public override void CustomSetStaticDefaults()
    {
        ParadoxSystem.FlippedBlockSpawnChance[Type] = 1f;
    }

    public override void CustomSetDefaults()
    {
        NPC.noGravity = true;
        NPC.spriteDirection = 1;
    }

    public override void AI()
    {
        NPC.TargetClosest(false);
    }

    public override void FindFrame(int frameHeight)
    {
        FrameDuration = 10;

        NPC.frameCounter++;
        if (NPC.frameCounter >= FrameDuration)
        {
            NPC.frame.Y += frameHeight;
            NPC.frameCounter = 0;

            if (NPC.frame.Y >= Main.npcFrameCount[Type] * frameHeight) NPC.frame.Y = (int)Frame.First;
        }
        NPC.rotation = NPC.velocity.ToRotation();
    }
    
    private enum ActionState
    {
        DemonEye,
        EaterOfSouls
    }

    private enum Frame
    {
        First,
        Second
    }
}