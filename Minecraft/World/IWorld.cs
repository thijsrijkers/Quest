using System;
using System.Threading.Tasks;

namespace Quest.Minecraft.World
{
    public interface IWorld : IDisposable
    {
        /// <summary>
        /// The player.
        /// </summary>
        IPlayer Player { get; }

        IWorld EnterIntoChat(string message);

        Task<IWorld> EnterIntoChatAsync(string message);
    }
}
