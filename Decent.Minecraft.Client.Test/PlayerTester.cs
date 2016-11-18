using System.Numerics;
using System.Threading.Tasks;
using Decent.Minecraft.Client.Test.Fakes;
using FluentAssertions;
using Xunit;

namespace Decent.Minecraft.Client.Test
{
    public class PlayerTester
    {
        public class When_moving
        {
            public class Towards_a_direction
            {
                [Theory]
                [InlineData(Direction.North, "Z", -1)]
                [InlineData(Direction.South, "Z", 1)]
                [InlineData(Direction.East, "X", 1)]
                [InlineData(Direction.West, "X", -1)]
                public async Task The_player_should_move_one_block_in_the_given_direction(
                    Direction towards, 
                    string axe,
                    int expectedPosition)
                {
                    var connection = new FakeConnection();
                    var player = new Player(connection);
                    await player.SetPositionAsync(new Vector3(0, 0, 0));

                    await player.MoveAsync(towards);

                    var setPosCommand = connection.GetLastCommand("player.setPos");
                    var axeIndex = axe == "X" ? 0 : 2;
                    setPosCommand.Value[axeIndex].Should().Be(expectedPosition,
                        $"moving {towards} means decrementing along the {axe}-axes");
                }
            }
        }
    }
}