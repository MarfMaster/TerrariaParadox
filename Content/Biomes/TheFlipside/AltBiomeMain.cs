using System.Collections;
using System.Collections.Generic;
using AltLibrary;
using AltLibrary.Common.AltBiomes;
using Microsoft.Xna.Framework;
using Terraria.GameContent.Personalities;
using Terraria.ID;
using Terraria.Localization;
using Terraria.ModLoader;
using TerrariaParadox.Content.Items.Materials;
using TerrariaParadox.Content.Items.Tiles.Bars;
using TerrariaParadox.Content.Items.Tiles.Blocks;
using TerrariaParadox.Content.Items.Tiles.Plants;
using TerrariaParadox.Content.Items.Weapons.Melee;
using TerrariaParadox.Content.Tiles.Blocks;
using TerrariaParadox.Content.Tiles.Misc;
using TerrariaParadox.Content.Tiles.Plants;
using TerrariaParadox.Content.Tiles.Walls;

namespace TerrariaParadox.Content.Biomes.TheFlipside;

public class AltBiomeMain : AltBiome
{
    public override string WorldIcon => "TerrariaParadox/Textures/UI/World/SaveFlipsideIcon";
    public override string IconSmall => "TerrariaParadox/Content/Biomes/TheFlipside/BiomeMainSurface_Icon";
    public override string OuterTexture => "TerrariaParadox/Textures/UI/World/GenFlipsideProgressBar";
    public override Color OuterColor => new Color(35, 198, 138);
    public override IShoppingBiome Biome => ModContent.GetInstance<TheFlipside.BiomeMainSurface>();

    public override string LocalizationCategory => "Biomes.TheFlipside";
    public override Color NameColor => OuterColor;
    public override void SetStaticDefaults()
    {
        BiomeType = BiomeType.Evil;
        
        AddTileConversion(ModContent.TileType<FlippedGrassBlock>(), TileID.Grass);
        AddTileConversion(ModContent.TileType<FlippedJungleGrassBlock>(), TileID.JungleGrass);
        AddTileConversion(ModContent.TileType<AssecstoneBlockTile>(), TileID.Stone);
        AddTileConversion(ModContent.TileType<AssecsandBlockTile>(), TileID.Sand);
        AddTileConversion(ModContent.TileType<AssecsandstoneBlockTile>(), TileID.Sandstone);
        AddTileConversion(ModContent.TileType<HardenedAssecsandBlockTile>(), TileID.HardenedSand);
        AddTileConversion(ModContent.TileType<MurkyIceBlockTile>(), TileID.IceBlock);

        GERunnerConversion.Add(TileID.Silt, ModContent.TileType<AssecsandBlockTile>());
        
        //BiomeFlesh = TileID.SilverBrick;
        //BiomeFleshWall = WallID.SilverBrick;
        
        //FleshDoorTile = TileID.ClosedDoor;
        //FleshChairTile = TileID.Chairs;
        //FleshTableTile = TileID.Tables;
        //FleshChestTile = TileID.Containers;
        //FleshDoorTileStyle = 7;
        //FleshChairTileStyle = 7;
        //FleshTableTileStyle = 7;
        //FleshChestTileStyle = 7;
        
        //FountainTile = TileID.WaterFountain;
        //FountainTileStyle = 1;
        //FountainTile = TileID.WaterFountain;
        //Fountain = TileID.WaterFountain;

        BiomeGrass = ModContent.TileType<FlippedGrassBlock>();
        BiomeJungleGrass  = ModContent.TileType<FlippedJungleGrassBlock>();
        //BiomeThornBush = ModContent.TileType<AssecsandBlockTile>();
        SeedType = ModContent.ItemType<FlippedSeeds>();
        BiomeOre = ModContent.TileType<ChitiniteOreTile>();
        BiomeOreItem = ModContent.ItemType<ChitiniteBar>();
        //BiomeOreBrick = ModContent.TileType<Lost_Brick>();
        AltarTile = ModContent.TileType<OothecaAltar>();
        
        //BiomeChestItem = ModContent.ItemType<Missing_File>();
        //BiomeChestTile = ModContent.TileType<Defiled_Dungeon_Chest>();
        //BiomeChestTileStyle = 1;
        //BiomeKeyItem = ModContent.ItemType<Defiled_Key>();
        
        //MimicType = ModContent.NPCType<Defiled_Mimic>();
        
        //BloodBunny = ModContent.NPCType<Defiled_Mite>();
        //BloodPenguin = ModContent.NPCType<Bile_Thrower>();
        //BloodGoldfish = ModContent.NPCType<Shattered_Goldfish>();
        
        AddWallConversions<AssecstoneWallTileUnsafe>
        (
            WallID.Stone,
            WallID.CaveUnsafe,
            WallID.Cave2Unsafe,
            WallID.Cave3Unsafe,
            WallID.Cave4Unsafe,
            WallID.Cave5Unsafe,
            WallID.Cave6Unsafe,
            WallID.Cave7Unsafe,
            WallID.Cave8Unsafe
        );
        
        AddWallConversions<HardenedAssecsandWallTileUnsafe>
        (
            WallID.HardenedSand
        );
        
        AddWallConversions<AssecsandstoneWallTileUnsafe>
        (
            WallID.Sandstone
        );
        
        //EvilBiomeGenerationPass = new Defiled_Wastelands_Generation_Pass();
    }

    public IEnumerable<int> ProvideItemObtainability()
    {
        yield return BiomeChestItem.Value;
    }		
    
    public override AltMaterialContext MaterialContext 
    {
        get 
        {
            AltMaterialContext context = new AltMaterialContext();
            context.SetEvilHerb(ModContent.ItemType<FlippedHerb>());
            context.SetEvilBar(ModContent.ItemType<ChitiniteBar>());
            context.SetEvilOre(ModContent.ItemType<ChitiniteOre>());
            context.SetVileInnard(ModContent.ItemType<EggCluster>());
            context.SetVileComponent(ModContent.ItemType<Stickler>());
            context.SetEvilBossDrop(ModContent.ItemType<BioluminescentGoop>());
            context.SetEvilSword(ModContent.ItemType<TarsalSaber>());
            return context;
        }
    }
}
