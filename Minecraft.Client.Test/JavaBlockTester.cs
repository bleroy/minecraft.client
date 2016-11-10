using System;
using Decent.Minecraft.Client.Blocks;
using FluentAssertions;
using Xunit;

namespace Decent.Minecraft.Client.Test
{
    public class JavaBlockTester
    {
        public class When_creating_a_stone_block
        {
            [Fact]
            public void It_should_return_the_correct_block()
            {
                var original = new Stone(Stone.StoneVariants.SmoothAndesite);

                var javaBlock = JavaBlock.From(original);

                javaBlock.Type.Should().Be(BlockType.Stone);
                javaBlock.Data.Parse().Should().Be(Stone.StoneVariants.SmoothAndesite);
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