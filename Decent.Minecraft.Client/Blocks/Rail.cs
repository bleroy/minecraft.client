namespace Decent.Minecraft.Client.Blocks
{
    /// <summary>
    /// <a href="http://minecraft.gamepedia.com/Rail">Gamepedia link</a>.
    /// </summary>
    public class Rail : IBlock
    {
        public Rail(RailDirections directions = RailDirections.NorthSouth)
        {
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
        }

        public RailDirections Directions { get; }
            
        public bool IsTurning { get; }

        public bool IsAscending { get; }
    }

    public class PoweredRail : Rail, IActivatableRail
    {
        public PoweredRail(RailDirections directions = RailDirections.NorthSouth, bool isActive = false) : base(directions)
        {
            if ((int)directions > 5)
            {
                throw new System.ArgumentOutOfRangeException(nameof(directions), $"Only simple rails can turn.");
            }

            IsActive = isActive;
        }
        public bool IsActive { get; }
    }
    public class ActivatorRail : Rail, IActivatableRail
    {
        public ActivatorRail(RailDirections directions = RailDirections.NorthSouth, bool isActive = false) : base(directions)
        {
            if ((int)directions > 5)
            {
                throw new System.ArgumentOutOfRangeException(nameof(directions), $"Only simple rails can turn.");
            }

            IsActive = isActive;
        }
        public bool IsActive { get; }
    }
    public class DetectorRail : Rail, IActivatableRail
    {
        public DetectorRail(RailDirections directions = RailDirections.NorthSouth, bool isActive = false) : base(directions)
        {
            if ((int)directions > 5)
            {
                throw new System.ArgumentOutOfRangeException(nameof(directions), $"Only simple rails can turn.");
            }

            IsActive = isActive;
        }
        public bool IsActive { get; }
    }

    public interface IActivatableRail
    {
        bool IsActive { get; }

        RailDirections Directions { get; }

        bool IsTurning { get; }

        bool IsAscending { get; }
    }
}
