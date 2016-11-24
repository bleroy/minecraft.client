using System;

namespace Decent.Minecraft.Client
{
    /// <summary>
    /// Information about a chat event: who sent the message, and the message itself.
    /// </summary>
    public class ChatEventArgs : EventArgs
    {
        public ChatEventArgs(int entityId, string message) : base()
        {
            EntityId = entityId;
            Message = message;
        }

        /// <summary>
        /// The id of the entity that sent the message.
        /// </summary>
        public int EntityId { get; }

        /// <summary>
        /// The message that was sent to the chat.
        /// </summary>
        public string Message { get; }
    }
}
