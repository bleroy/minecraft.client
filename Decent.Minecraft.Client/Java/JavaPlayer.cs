namespace Decent.Minecraft.Client.Java
{
    public class JavaPlayer : JavaEntity, IPlayer
    {
        public JavaPlayer(IConnection connection, int? playerId = null)
            : base(EntityType.ThePlayer, connection, "player", playerId)
        {
        }
    }
}