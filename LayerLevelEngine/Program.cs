using System;

namespace LayerLevelEngine
{
#if WINDOWS || XBOX
    static class Program
    {
        /// <summary>
        /// The main entry point for the application.
        /// </summary>
        static void Main(string[] args)
        {
            using (LevelTest game = new LevelTest())
            {
                game.Run();
            }
        }
    }
#endif
}

