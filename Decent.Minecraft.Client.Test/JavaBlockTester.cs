using System;
using Decent.Minecraft.Client.Blocks;
using Decent.Minecraft.Client.Java;
using FluentAssertions;
using Xunit;

namespace Decent.Minecraft.Client.Test
{
    public class JavaBlockTester
    {
        public class For_a_stone_block
        {
            public class When_serializing
            {
                [Fact]
                public void It_should_return_a_JavaBlock_with_variants_as_bytes()
                {
                    var original = new Stone(Mineral.SmoothAndesite);

                    var javaBlock = JavaBlock.From(original);

                    javaBlock.TypeId.Should().Be(JavaBlockTypes.Id<Stone>());
                    javaBlock.Data.Parse().Should().Be(Mineral.SmoothAndesite);
                }
            }

            public class When_deserializing
            {
                [Theory]
                [InlineData((byte)Mineral.Stone, Mineral.Stone)]
                [InlineData((byte)Mineral.Andesite, Mineral.Andesite)]
                [InlineData(Mineral.SmoothGranite, Mineral.SmoothGranite)]
                [InlineData(0x20, (Mineral)0x20)]
                [InlineData(null, Mineral.Stone)]
                public void It_should_return_a_stone_block_with_the_variant_specified(byte serializedData, Mineral? expected)
                {
                    var actual = JavaBlock.Create(JavaBlockTypes.Id<Stone>(), serializedData) as Stone;
                    actual.Should().NotBeNull("deserializing a Stone block should return a Stone object");
                    actual.Mineral.Should().Be(expected, "the variant should be deserialized correctly");
                }
            }
        }

        public class For_an_end_stone
        {
            public class When_serializing
            {
                [Fact]
                public void It_should_return_a_JavaBlock_for_the_end_stone()
                {
                    var original = new EndStone();

                    var javaBlock = JavaBlock.From(original);

                    javaBlock.TypeId.Should().Be(JavaBlockTypes.Id<EndStone>());
                }
            }

            public class When_deserializing
            {
                [Fact]
                public void It_should_return_an_end_stone()
                {
                    var blockType = JavaBlockTypes.Id<EndStone>();

                    var deserialized = JavaBlock.Create(blockType, 0) as EndStone;

                    deserialized.Should().NotBeNull();
                }
            }
        }

        public class For_a_glowstone
        {
            public class When_serializing
            {
                [Fact]
                public void It_should_return_a_JavaBlock_for_the_glowstone()
                {
                    var original = new Glowstone();

                    var javaBlock = JavaBlock.From(original);

                    javaBlock.TypeId.Should().Be(JavaBlockTypes.Id<Glowstone>());
                }
            }

            public class When_deserializing
            {
                [Fact]
                public void It_should_return_a_glowstone()
                {
                    var blockType = JavaBlockTypes.Id<Glowstone>();

                    var deserialized = JavaBlock.Create(blockType, 0) as Glowstone;

                    deserialized.Should().NotBeNull();
                }
            }
        }

    }

    public static class ByteExtensions
    {
        public static Mineral Parse(this byte input)
        {
            return (Mineral)Enum.Parse(typeof(Mineral), input.ToString());
        }
    }
}