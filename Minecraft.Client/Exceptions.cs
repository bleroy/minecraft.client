using System;

namespace Decent.Minecraft.Client
{
    public class FailedToConnectToMineCraftEngine : Exception
    {
        public FailedToConnectToMineCraftEngine(Exception innerException)
            : base("Failed to connect MineCraft. Is MineCraft running?", innerException)
        { }
    }
}
