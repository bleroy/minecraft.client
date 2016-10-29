using System.Collections.Generic;
using System.Linq;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Describes a Minecraft block that can be sent to Minecraft.setBlock(s).
    /// </summary>
    public class Block
    {
        public BlockType Type { get; }
        public IList<int> Data { get; }

        public Block(BlockType type)
        {
            Type = type;
            Data = new int[] { };
        }

        public Block(BlockType type, IEnumerable<int> data)
        {
            Data = data.ToList();
        }

        public Block(BlockType type, params int[] data)
        {
            Data = data;
        }

        public static Block Air = new Block(BlockType.Air);
        public static Block Stone = new Block(BlockType.Stone);
        public static Block Grass = new Block(BlockType.Grass);
        public static Block Dirt = new Block(BlockType.Dirt);
        public static Block Cobblestone = new Block(BlockType.Cobblestone);
        public static Block WoodPlanks = new Block(BlockType.WoodPlanks);
        public static Block Sapling = new Block(BlockType.Sapling);
        public static Block Bedrock = new Block(BlockType.Bedrock);
        public static Block WaterFlowing = new Block(BlockType.WaterFlowing);
        public static Block Water = new Block(BlockType.Water);
        public static Block WaterStationary = new Block(BlockType.WaterStationary);
        public static Block LavaFlowing = new Block(BlockType.LavaFlowing);
        public static Block Lava = new Block(BlockType.Lava);
        public static Block LavaStationary = new Block(BlockType.LavaStationary);
        public static Block Sand = new Block(BlockType.Sand);
        public static Block Gravel = new Block(BlockType.Gravel);
        public static Block GoldOre = new Block(BlockType.GoldOre);
        public static Block IronOre = new Block(BlockType.IronOre);
        public static Block CoalOre = new Block(BlockType.CoalOre);
        public static Block Wood = new Block(BlockType.Wood);
        public static Block Leaves = new Block(BlockType.Leaves);
        public static Block Glass = new Block(BlockType.Glass);
        public static Block LapisLazuliOre = new Block(BlockType.LapisLazuliOre);
        public static Block LapisLazuliBlock = new Block(BlockType.LapisLazuliBlock);
        public static Block Sandstone = new Block(BlockType.Sandstone);
        public static Block Bed = new Block(BlockType.Bed);
        public static Block Cobweb = new Block(BlockType.Cobweb);
        public static Block GrassTall = new Block(BlockType.GrassTall);
        public static Block Wool = new Block(BlockType.Wool);
        public static Block FlowerYellow = new Block(BlockType.FlowerYellow);
        public static Block FlowerCyan = new Block(BlockType.FlowerCyan);
        public static Block MushroomBrown = new Block(BlockType.MushroomBrown);
        public static Block MushroomRed = new Block(BlockType.MushroomRed);
        public static Block GoldBlock = new Block(BlockType.GoldBlock);
        public static Block IronBlock = new Block(BlockType.IronBlock);
        public static Block StoneSlabDouble = new Block(BlockType.StoneSlabDouble);
        public static Block StoneSlab = new Block(BlockType.StoneSlab);
        public static Block BrickBlock = new Block(BlockType.BrickBlock);
        public static Block TNT = new Block(BlockType.TNT);
        public static Block Bookshelf = new Block(BlockType.Bookshelf);
        public static Block MossStone = new Block(BlockType.MossStone);
        public static Block Obsidian = new Block(BlockType.Obsidian);
        public static Block Torch = new Block(BlockType.Torch);
        public static Block Fire = new Block(BlockType.Fire);
        public static Block StairsWood = new Block(BlockType.StairsWood);
        public static Block Chest = new Block(BlockType.Chest);
        public static Block DiamondOre = new Block(BlockType.DiamondOre);
        public static Block DiamondBlock = new Block(BlockType.DiamondBlock);
        public static Block CraftingTable = new Block(BlockType.CraftingTable);
        public static Block Farmland = new Block(BlockType.Farmland);
        public static Block FurnaceInactive = new Block(BlockType.FurnaceInactive);
        public static Block FurnaceActive = new Block(BlockType.FurnaceActive);
        public static Block DoorWood = new Block(BlockType.DoorWood);
        public static Block Ladder = new Block(BlockType.Ladder);
        public static Block StairsCobblestone = new Block(BlockType.StairsCobblestone);
        public static Block DoorIron = new Block(BlockType.DoorIron);
        public static Block RedstoneOre = new Block(BlockType.RedstoneOre);
        public static Block Snow = new Block(BlockType.Snow);
        public static Block Ice = new Block(BlockType.Ice);
        public static Block SnowBlock = new Block(BlockType.SnowBlock);
        public static Block Cactus = new Block(BlockType.Cactus);
        public static Block Clay = new Block(BlockType.Clay);
        public static Block SugarCane = new Block(BlockType.SugarCane);
        public static Block Fence = new Block(BlockType.Fence);
        public static Block GlowstoneBlock = new Block(BlockType.GlowstoneBlock);
        public static Block BedrockInvisible = new Block(BlockType.BedrockInvisible);
        public static Block StoneBrick = new Block(BlockType.StoneBrick);
        public static Block GlassPane = new Block(BlockType.GlassPane);
        public static Block Melon = new Block(BlockType.Melon);
        public static Block FenceGate = new Block(BlockType.FenceGate);
        public static Block GlowingObsidian = new Block(BlockType.GlowingObsidian);
        public static Block NetherReactorCore = new Block(BlockType.NetherReactorCore);
    }
}
