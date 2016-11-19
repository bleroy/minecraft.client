using System.Collections;
using FluentAssertions;
using Xunit;

namespace Decent.Minecraft.Client.Test
{
    public class Serialization
    {
        [Theory,
            InlineData(new object[] { }, ""),
            InlineData(new[] { 1, 2, 3 }, "1,2,3"),
            InlineData("foo", "foo"),
            InlineData(new object[] { 1, "foo", 2.3 }, "1,foo,2.3"),
            InlineData(new object[] {
                1, new[] { 2.1, 2.2, 2.3 }, new object[] {3.1, new[] { 3.21, 3.22 } }, 4},
                "1,2.1,2.2,2.3,3.1,3.21,3.22,4")]
        public void FlattenListGivesCorrectString(IEnumerable list, string expectedOutput)
        {
            expectedOutput.Should().Be(list.FlattenToString());
        }
    }
}
