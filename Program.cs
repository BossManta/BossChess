using System;

namespace BossChess
{
    public static class Program
    {
        [STAThread]
        static void Main()
        {
            using (var game = new BossChess())
                game.Run();
        }
    }
}
