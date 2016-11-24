namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Defines the contract for colored blocks such as wool, stained glass, stained clay, or carpet.
    /// </summary>
    public interface IColoredBlock
    {
        /// <summary>
        /// The color of the block.
        /// </summary>
        Color Color { get; }
    }
}
