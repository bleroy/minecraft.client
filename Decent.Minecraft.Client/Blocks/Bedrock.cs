namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// Bedrock is a indestructible block.<br />
    /// The bottom of the overworld is made out of Bedrock to prevent falling into death.
    /// In the nether both the bottom of the world and the top are covered with Bedrock.<br />
    /// <a href="http://minecraft.gamepedia.com/Bedrock">Gamepedia link</a>.
    /// </summary>
    public class Bedrock : Block
    {
        public Bedrock() : base(BlockType.Bedrock)
        {
        }
    }
}
