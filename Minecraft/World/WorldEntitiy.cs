using System;

namespace Quest.Minecraft.World
{
    public partial class WorldEntity : IEntity
    {
        internal WorldEntity(EntityType type, IConnection connection, string prefix = "entity", int? id = null)
        {
            this.id = id;
            Type = type;
            Connection = connection;
            Prefix = prefix;
            Console.ForegroundColor = ConsoleColor.DarkMagenta;
            Console.WriteLine("Setting world entity");
        }

        public int? id { get; }
        public EntityType Type { get; }
        protected IConnection Connection { get; }
        protected string Prefix { get; }
    }
}
