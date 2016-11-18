namespace Decent.Minecraft.Client
{
    public class Player : Entity
    {
        public Player(IConnection connection)
            : base(EntityType.ThePlayer, connection, "player")
        {
        }
    }
}