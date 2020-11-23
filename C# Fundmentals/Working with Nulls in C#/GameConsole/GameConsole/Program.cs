using System;

namespace GameConsole
{
    class Program
    {
        static void Main(string[] args)
        {
            var player = new PlayerCharacter(new DiamondSkinDefence())
            {
                Name = "Muhammed"
            };

            player.Hit(10);
        }
    }
}
