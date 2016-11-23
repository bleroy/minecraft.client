using System;

namespace Decent.Minecraft.Client
{
    public class ChatEventArgs : EventArgs
    {
        public ChatEventArgs(int entityId, string message) : base()
        {
            EntityId = entityId;
            Message = message;
        }

        public int EntityId { get; }
        public string Message { get; }
    }
}
