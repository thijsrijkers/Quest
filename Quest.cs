using Quest.Minecraft.World;
using System;

namespace Quest
{
    class Quest
    {
        private const string logo =
          "\n" +
          "                                 \n" +
          " ▄█▀▄  █ █  █▀▀  ▄▀▀▀▄ ▄█▀▀█     \n" +
          "▐█▌ ▐  █ ▐ ▐█▀▀   ▀▄▄    ▐       \n" +
          " ▀█▄▀▄ ▐█▌  █▄▄▀ ▄▄▄▀   ▐▌       \n" +
          "                                 \n";
        static void Main(string[] args)
        {
            Console.ForegroundColor = ConsoleColor.White;
            Console.WriteLine(logo);
            Console.ForegroundColor = ConsoleColor.Cyan;

            var world = MCWorld.Connect("localhost", 4711);
            Console.ForegroundColor = ConsoleColor.White;
            world.EnterIntoChat("Quest connected");
        }
    }
}

