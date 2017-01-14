using System;
using System.Collections.Generic;
using Decent.Minecraft.Client.Blocks;

namespace Decent.Minecraft.Client.Java
{
    /// <summary>
    /// A registry of Java Minecraft block types.
    /// The TypeId values correspond to actual Minecraft block IDs.
    /// <a href="http://minecraft.gamepedia.com/Data_values/Block_IDs">Gamepedia link</a>.
    /// </summary>
    public static class JavaBlockTypes
    {
        private static BlockRegistry _registry { get; } = new BlockRegistry();

        /// <summary>
        /// The list of known block types and their type ids.
        /// </summary>
        public static IList<BlockType> Types
        {
            get
            {
                return _registry.Types;
            }
        }

        /// <summary>
        /// Get the type from its id.
        /// </summary>
        /// <param name="typeId">The id of the type</param>
        /// <returns>The type, or UnknownBlock if not found</returns>
        public static Type GetType(int typeId)
        {
            return _registry.GetType(typeId);
        }

        /// <summary>
        /// Gets the id for a block type.
        /// </summary>
        /// <typeparam name="TBlock">The block type</typeparam>
        /// <returns>The type id</returns>
        public static int Id<TBlock>() where TBlock : IBlock
        {
            return GetTypeId(typeof(TBlock));
        }

        /// <summary>
        /// Get the id for a type.
        /// </summary>
        /// <param name="type">The type</param>
        /// <returns>The id for the type</returns>
        public static int GetTypeId(Type type)
        {
            return _registry.GetTypeId(type);
        }

        // Constants for blocks that are represented by other block types.
        public static byte StationaryWater = 9;
        public static byte StationaryLava = 11;
        public static byte SnowLayer = 78;
        public static byte AcaciaAndDarkOakWood = 162;

        public static byte OakWoodenDoor = 64;
        public static byte SpruceWoodenDoor = 193;
        public static byte BirchWoodenDoor = 194;
        public static byte JungleWoodenDoor = 195;
        public static byte AcaciaWoodenDoor = 196;
        public static byte DarkOakWoodenDoor = 197;

        static JavaBlockTypes()
        {
            _registry
                .Register<Air>(0)
                .Register<Stone>(1)
                .Register<Grass>(2)
                .Register<Dirt>(3)
                .Register<Cobblestone>(4)
                .Register<WoodPlanks>(5)
                .Register<Sapling>(6)
                .Register<Bedrock>(7)
                .Register<Water>(8)
                //.Register<WaterStationary>(9) // No BlockType for StationaryWater, it's all handled in Water
                .Register<Lava>(10)
                //.Register<LavaStationary>(11) // No BlockType for StationaryLava, it's all handled in Lava
                .Register<Sand>(12)
                //.Register<Gravel>(13)
                .Register<GoldOre>(14)
                .Register<IronOre>(15)
                .Register<CoalOre>(16)
                .Register<Wood>(17)
                .Register<Leaves>(18)
                //.Register<Sponge>(19)
                .Register<Glass>(20)
                //.Register<LapisLazuliOre>(21)
                .Register<LapisLazuli>(22)
                //.Register<Dispenser>(23)
                //.Register<Sandstone>(24)
                //.Register<Note>(25)
                .Register<Bed>(26)
                //.Register<PoweredRail>(27)
                //.Register<DetectorRail>(28)
                //.Register<StickyPiston>(29)
                .Register<Cobweb>(30)
                //.Register<GrassTall>(31)
                //.Register<DeadBush>(32)
                //.Register<Piston>(33)
                //.Register<PistonHead>(34)
                .Register<Wool>(35)
                //.Register<PistonExtension>(36)
                //.Register<Dandelion>(37)
                //.Register<Poppy>(38)
                //.Register<MushroomBrown>(39)
                //.Register<MushroomRed>(40)
                .Register<Gold>(41)
                .Register<Iron>(42)
                //.Register<StoneSlabDouble>(43)
                //.Register<StoneSlab>(44)
                .Register<Bricks>(45)
                .Register<TNT>(46)
                .Register<Bookshelf>(47)
                .Register<MossStone>(48)
                .Register<Obsidian>(49)
                .Register<Torch>(50)
                .Register<Fire>(51)
                //.Register<MobSpawner>(52)
                //.Register<StairsWood>(53)
                .Register<Chest>(54)
                //.Register<RedstoneWire>(55)
                .Register<DiamondOre>(56)
                .Register<Diamond>(57)
                .Register<CraftingTable>(58)
                //.Register<Wheat>(59)
                .Register<Farmland>(60)
                //.Register<FurnaceInactive>(61)
                //.Register<FurnaceActive>(62)
                //.Register<StandingSign>(63)
                .Register<WoodenDoor>(64)
                //.Register<Ladder>(65)
                //.Register<Rail>(66)
                //.Register<StairsCobbleStone>(67)
                //.Register<WallSign>(68)
                //.Register<Lever>(69)
                //.Register<PressurePlate>(70)
                .Register<IronDoor>(71)
                //.Register<WoodenPressurePlate>(72)
                //.Register<RedstoneOre>(73)
                //.Register<GlowingRedstoneOre>(74)
                //.Register<UnlitRedstoneTorch>(75)
                //.Register<RedstoneTorch>(76)
                //.Register<StoneButton>(77)
                //.Register<SnowLayer>(78) // No class for snow layer, as it's all rolled into Snow.
                .Register<Ice>(79)
                .Register<Snow>(80)
                .Register<Cactus>(81)
                .Register<Clay>(82)
                //.Register<SugarCane>(83)
                .Register<Jukebox>(84)
                .Register<Fence>(85)
                .Register<Pumpkin>(86)
                .Register<Netherrack>(87)
                .Register<SoulSand>(88)
                .Register<Glowstone>(89)
                //.Register<NetherPortal>(90)
                .Register<JackOLantern>(91)
                //.Register<Cake>(92)
                //.Register<RedstoneRepeater>(93)
                //.Register<RedstoneRepeaterActive>(94)
                .Register<StainedGlass>(95)
                //.Register<Trapdoor>(96)
                //.Register<MonsterEgg>(97)
                .Register<StoneBricks>(98)
                //.Register<BrownMushroomBlock>(99)
                //.Register<RedMushroomBlock>(100)
                .Register<IronBars>(101)
                //.Register<GlassPane>(102)
                .Register<Melon>(103)
                //.Register<PumpkinStem>(104)
                //.Register<MelonStem>(105)
                //.Register<Vine>(106)
                .Register<FenceGate>(107)
                //.Register<BrickStairs>(108)
                //.Register<StoneBrickStairs>(109)
                //.Register<Mycelium>(110)
                .Register<WaterLily>(111)
                //.Register<NetherBrick>(112)
                //.Register<NetherBrickFence>(113)
                //.Register<NetherBrickStairs>(114)
                //.Register<NetherWart>(115)
                //.Register<EnchantingTable>(116)
                //.Register<BrewingStand>(117)
                //.Register<Cauldron>(118)
                //.Register<EndPortal>(119)
                //.Register<EndPortalFrame>(120)
                .Register<EndStone>(121)
                //.Register<DragonEgg>(122)
                //.Register<RedstoneLamp>(123)
                //.Register<LitRedstoneLamp>(124)
                //.Register<DoubleWoodenSlab>(125)
                //.Register<WoodenSlab>(126)
                //.Register<Cocoa>(127)
                //.Register<SandstoneStairs>(128)
                .Register<EmeraldOre>(129)
                //.Register<EnderChest>(130)
                //.Register<TripwireHook>(131)
                //.Register<Tripwire>(132)
                .Register<Emerald>(133)
                //.Register<SpruceWoodStairs>(134)
                //.Register<BirchWoodStairs>(135)
                //.Register<JungleWoodStairs>(136)
                //.Register<CommandBlock>(137)
                //.Register<Beacon>(138)
                //.Register<CobblestoneWall>(139)
                //.Register<FlowerPot>(140)
                //.Register<Carrot>(141)
                //.Register<Potato>(142)
                //.Register<WoodenButton>(143)
                //.Register<Skull>(144)
                //.Register<Anvil>(145)
                //.Register<TrappedChest>(146)
                //.Register<LightWeightedPressurePlate>(147)
                //.Register<HeavyWeightedPressurePlate>(148)
                //.Register<UnpoweredComparator>(149)
                //.Register<PoweredComparator>(150)
                //.Register<DaylightDetector>(151)
                //.Register<Redstone>(152)
                .Register<QuartzOre>(153)
                //.Register<Hopper>(154)
                .Register<Quartz>(155)
                //.Register<QuartzStairs>(156)
                //.Register<ActivatorRail>(157)
                //.Register<Dropper>(158)
                .Register<StainedClay>(159)
                //.Register<StainedGlassPane>(160)
                //.Register<AcaciaLeaves>(161)
                //.Register<AcaciaWood>(162) // No Class for AcaciaWood as its all rolled into Wood.
                //.Register<AcaciaStairs>(163)
                //.Register<DarkOakStairs>(164)
                //.Register<Slime>(165)
                //.Register<Barrier>(166)
                //.Register<IronTrapdoor>(167)
                //.Register<Prismarine>(168)
                //.Register<SeaLantern>(169)
                .Register<Hay>(170)
                //.Register<Carpet>(171)
                .Register<HardenedClay>(172)
                .Register<Coal>(173)
                //.Register<PackedIce>(174)
                //.Register<LargeFlower>(175)
                //.Register<StandingBanner>(176)
                //.Register<WallBanner>(177)
                //.Register<InvertedDaylightSensor>(178)
                //.Register<RedSandstone>(179)
                //.Register<RedSandstoneStairs>(180)
                //.Register<DoubleRedSandstoneSlab>(181)
                //.Register<RedSandstoneSlab>(182)
                //.Register<SpruceFenceGate>(183)
                //.Register<BirchFenceGate>(184)
                //.Register<JungleFenceGate>(185)
                //.Register<DarkOakFenceGate>(186)
                //.Register<AcaciaFenceGate>(187)
                //.Register<SpruceGate>(188)
                //.Register<BirchFence>(189)
                //.Register<JungleFence>(190)
                //.Register<DarkOakFence>(191)
                //.Register<AcaciaFence>(192)
                //.Register<SpruceDoor>(193)
                //.Register<BirchDoor>(194)
                //.Register<JungleDoor>(195)
                //.Register<AcaciaDoor>(196)
                //.Register<DarkOakDoor>(197)
                //.Register<EndRod>(198)
                //.Register<ChorusPlant>(199)
                //.Register<ChorusFlower>(200)
                //.Register<Purpur>(201)
                //.Register<PurpurPillar>(202)
                //.Register<PurpurStairs>(203)
                //.Register<PurpurDoubleSlab>(204)
                //.Register<PurpurSlab>(205)
                //.Register<EndBricks>(206)
                //.Register<Beetroot>(207)
                //.Register<GrassPath>(208)
                //.Register<EndGateway>(209)
                //.Register<RepeatingCommandBlock>(210)
                //.Register<ChainCommandBlock>(211)
                //.Register<FrostedIce>(212)
                .Register<Magma>(213)
                .Register<NetherWartBlock>(214)
                //.Register<RedNetherBrick>(215)
                .Register<Bone>(216)
                //.Register<StructureVoid>(217)
                //.Register<Observer>(218)
                //.Register<WhiteShulkerBox>(219)
                //.Register<OrangeShulkerBox>(220)
                //.Register<MagentaShulkerBox>(221)
                //.Register<LightBlueShulkerBox>(222)
                //.Register<YellowShulkerBox>(223)
                //.Register<LimeShulkerBox>(224)
                //.Register<PinkShulkerBox>(225)
                //.Register<GrayShulkerBox>(226)
                //.Register<LightGrayShulkerBox>(227)
                //.Register<CyanShulkerBox>(228)
                //.Register<PurpleShulkerBox>(229)
                //.Register<BlueShulkerBox>(230)
                //.Register<BrownShulkerBox>(231)
                //.Register<GreenShulkerBox>(232)
                //.Register<RedShulkerBox>(233)
                //.Register<BlackShulkerBox>(234)
                //.Register<StructureBlock>(255)
                ;
        }
    }
}