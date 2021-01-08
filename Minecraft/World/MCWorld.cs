using Quest.Minecraft.Events;
using System;
using System.Threading.Tasks;

namespace Quest.Minecraft.World
{
    public class MCWorld :IWorld
    {
        internal MCWorld(IConnection connection)
        {
            Connection = connection;
        }

        private IConnection Connection { get; }
        public IPlayer Player { get; private set; }

        public event EventHandler<ChatEvent> PostedToChat;

        /// <summary>
        /// Setting connection
        /// </summary>
        /// <param name="address">Ip adress</param>
        /// <param name="port">The port of the server</param>
        public static MCWorld Connect(string address = "localhost", int port = 4711)
        {
            var connection = new ConnectWorld(address: address, port: port);
            var world = new MCWorld(connection);
            connection.Open();
            world.Player = new WorldPlayer(connection);
            return world;
        }

        public void Dispose()
        {
            Connection.Close();
        }

        /// <summary>
        /// Enter into chat
        /// </summary>
        public async Task<IWorld> EnterIntoChatAsync(string message)
        {
            Console.WriteLine(message);
            await Connection.SendAsync("chat.post", message);
            return this;
        }

        public IWorld EnterIntoChat(string message)
        {
            EnterIntoChatAsync(message).Wait();
            return this;
        }
    }
}
