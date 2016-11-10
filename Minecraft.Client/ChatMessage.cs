namespace Decent.Minecraft.Client
{
    public class ChatMessage
    {
        public ChatMessage(int entityId, string message)
        {
            EntityId = entityId;
            Message = message;
        }

        public int EntityId { get; }
        public string Message { get; }
    }
}
