using System;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// An exception that is thrown when the connection to a Minecraft instance failed.
    /// </summary>
    public class FailedToConnectToMinecraftEngine : Exception
    {
        public FailedToConnectToMinecraftEngine(Exception innerException)
            : base("Failed to connect Minecraft. Is MineCraft running?", innerException)
        { }
    }
}
