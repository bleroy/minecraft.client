namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Rail">Gamepedia link</a>.
    /// </summary>
    public class Rail : IBlock
    {
        public Rail(RailDirections directions = RailDirections.NorthSouth, RailType type = RailType.Simple, bool isActive = false)
        {
            if ((int)directions > 5 && (int)type > 0)
            {
                throw new System.ArgumentOutOfRangeException(nameof(directions), $"Only simple rails can turn.");
            }

            if ((int)type < 1 && isActive)
            {
                throw new System.ArgumentOutOfRangeException(nameof(directions), $"Simple rails cannot be active.");
            }

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

            Directions = directions;
            IsActive = isActive;
            Type = type;
        }

        public RailDirections Directions { get; }
            
        public bool IsTurning { get; }

        public bool IsAscending { get; }

        public RailType Type { get; }

        public bool IsActive { get; }
    }
}
