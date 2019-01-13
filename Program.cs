using System;

namespace AlmostFlappyBird
{
    public static class Program
    {
        static void Main()
        {
            using (var game = new AlmostFlappy())
                game.Run();
        }
    }

}
