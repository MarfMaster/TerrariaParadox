using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using Terraria;
using Terraria.Audio;
using Terraria.DataStructures;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using Terraria.ObjectData;
using TerrariaParadox.Content.Dusts.Tiles.Blocks;

namespace TerrariaParadox.Content.Tiles.Misc;

public class OothecaAltar : ModTile
{
    public override void SetStaticDefaults()
    {
        Main.tileLighted[Type] = true;
        Main.tileFrameImportant[Type] = true;
        Main.tileLavaDeath[Type] = false;
        Main.tileHammer[Type] = true;
        
        VanillaFallbackOnModDeletion = TileID.DemonAltar;

        TileObjectData.newTile.CopyFrom(TileObjectData.Style3x2);
        TileObjectData.addTile(Type);

        LocalizedText name = CreateMapEntryName();
        AddMapEntry(new Color(64, 129, 119), name);
        
        DustType = ModContent.DustType<AssecstoneDust>();
        
        HitSound = SoundID.NPCHit9;
        
        MineResist = 2f;
    }		
    
    // This method allows you to change the sound a tile makes when hit
    public override bool KillSound(int i, int j, bool fail) 
    {
        if (!fail) //Item177 for poo sound
        {
            SoundEngine.PlaySound(SoundID.NPCHit8, new Vector2(i, j).ToWorldCoordinates());
            return false;
        }
        return base.KillSound(i, j, fail);
    }

    public override bool CanKillTile(int i, int j, ref bool blockDamaged)
    {
        Vector2 TileCoordinates = new Vector2(i, j).ToWorldCoordinates();
        Player player = Main.player[Player.FindClosest(TileCoordinates, 1, 1)];
        if (player.HeldItem.hammer >= 80 && Main.hardMode)
        {
            return true;
        }
        else
        {
            player.Hurt(PlayerDeathReason.LegacyDefault(), player.statLife / 2, (TileCoordinates.X < player.position.X).ToDirectionInt());
            return false;
        }
    }

    public override void KillMultiTile(int i, int j, int frameX, int frameY)
    {
        WorldGen.SmashAltar(i, j);
    }
}