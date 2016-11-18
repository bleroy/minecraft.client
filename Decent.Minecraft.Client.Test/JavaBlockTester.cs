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
                var original = new Stone(Mineral.SmoothAndesite);

                var javaBlock = JavaBlock.From(original);

                javaBlock.Type.Should().Be(BlockType.Stone);
                javaBlock.Data.Parse().Should().Be(Mineral.SmoothAndesite);
            }
        }

        public class When_deserializing_a_stone_block
        {
            [Theory]
            [InlineData((byte)Mineral.Stone, Mineral.Stone)]
            [InlineData((byte)Mineral.Andesite, Mineral.Andesite)]
            [InlineData(Mineral.SmoothGranite, Mineral.SmoothGranite)]
            [InlineData(0x20, (Mineral)0x20)]
            [InlineData(null, Mineral.Stone)]
            public void It_should_return_a_stone_block_with_the_variant_specified(byte serializedData, Mineral? expected)
            {
                var actual = JavaBlock.Create(BlockType.Stone, serializedData) as Stone;
                actual.Should().NotBeNull("deserializing a Stone block should return a Stone object");
                actual.Type.Should().Be(BlockType.Stone);
                actual.Mineral.Should().Be(expected, "the variant should be deserialized correctly");
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