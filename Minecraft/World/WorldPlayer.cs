using System;

namespace Quest.Minecraft.World
{
    public class WorldPlayer : WorldEntity, IPlayer
    {
        public WorldPlayer(IConnection connection, int? idOfPlayer = null)
            : base(EntityType.ThePlayer, connection, "player", idOfPlayer)
        {
            Console.WriteLine("Getting player on connection");
            Console.ForegroundColor = ConsoleColor.White;
        }
    }
}
