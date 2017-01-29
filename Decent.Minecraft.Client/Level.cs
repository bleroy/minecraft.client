namespace Decent.Minecraft.Client
{
    /// <summary>
    /// The level of a liquid (ex: Water Level)
    /// </summary>
    public enum Level : byte
    {
        ///<summary>Source means a block at the highest level setting and originates liquid flow. 
        /// Only liquid source blocks can be collected in a bucket. </summary>
        Source = 0,
        Highest,
        Higher,
        High,
        Mid,
        Low,
        Lower,
        Lowest = 7
    }
}
