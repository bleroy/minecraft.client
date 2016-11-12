using System;
using Decent.Minecraft.Client.Blocks;
using FluentAssertions;
using Xunit;

namespace Decent.Minecraft.Client.Test
{
    public class JavaBlockTester
    {
        public class When_serializing_a_stone_block
        {
            [Fact]
            public void It_should_return_a_JavaBlock_with_variants_as_bytes()
            {
                var original = new Stone(Stone.StoneVariants.SmoothAndesite);

                var javaBlock = JavaBlock.From(original);

                javaBlock.Type.Should().Be(BlockType.Stone);
                javaBlock.Data.Parse().Should().Be(Stone.StoneVariants.SmoothAndesite);
            }
        }

        public class When_deserializing_a_stone_block
        {
            [Theory]
            [InlineData((byte)Stone.StoneVariants.Stone, Stone.StoneVariants.Stone)]
            [InlineData((byte)Stone.StoneVariants.Andesite, Stone.StoneVariants.Andesite)]
            [InlineData((byte)Stone.StoneVariants.SmoothGranite, Stone.StoneVariants.SmoothGranite)]
            [InlineData(0x20, Stone.StoneVariants.Stone)]  // unknown variant should return default variant
            [InlineData(null, Stone.StoneVariants.Stone)]  // unknown variant should return default variant
            public void It_should_return_a_stone_block_with_the_variant_specified(byte serializedData, Stone.StoneVariants expected)
            {
                var actual = JavaBlock.Create(BlockType.Stone, serializedData) as Stone;

                actual.Should().NotBeNull("deserializing a Stone block should return a Stone object");
                actual.Type.Should().Be(BlockType.Stone);
                actual.Variant.Should().Be(expected, "the variant should be deserialized correctly");
            }
        }

    }

    public static class ByteExtensions
    {
        public static Stone.StoneVariants Parse(this byte input)
        {
            return (Stone.StoneVariants)Enum.Parse(typeof(Stone.StoneVariants), input.ToString());
        }
    }
}