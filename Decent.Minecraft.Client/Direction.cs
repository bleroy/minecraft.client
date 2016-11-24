namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Cardinal points, or 2D directions.
    /// For 3D directions, you may use `Orientation` or `Facing`.
    /// Some APIs also use continuous directions in the form of `Vector3`.
    /// </summary>
    public enum Direction : byte
    {
        South = 0,
        West,
        North,
        East
    }
}
