namespace Decent.Minecraft.Client
{
    public class JavaPlayer : JavaEntity, IPlayer
    {
        public JavaPlayer(IConnection connection)
            : base(EntityType.ThePlayer, connection, "player")
        {
        }
    }
}