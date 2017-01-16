namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Rail">Gamepedia link</a>.
    /// </summary>
    public class Rail : IBlock
    {
        public Rail(RailDirections directions = RailDirections.NorthSouth)
        {
            Directions = directions;
            if ((int)directions < 2)
            {
                IsAscending = false;
                IsTurning = false;
            }
            else if ((int)directions < 6)
            {
                IsAscending = true;
                IsTurning = false;
            }
            else
            {
                IsAscending = false;
                IsTurning = true;
            }
        }

        public RailDirections Directions { get; }
            
        public bool IsTurning { get; }

        public bool IsAscending { get; }
    }
}
