using System;

namespace Decent.Minecraft.Client
{
    public class FailedToConnectToMinecraftEngine : Exception
    {
        public FailedToConnectToMinecraftEngine(Exception innerException)
            : base("Failed to connect Minecraft. Is MineCraft running?", innerException)
        { }
    }
}
