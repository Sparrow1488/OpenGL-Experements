﻿using OpenGl.SapperTK.Windows;

namespace OpenGl.SapperTK
{
    internal class Program
    {
        public static void Main(string[] args)
        {
            using var game = new Game();
            game.Run();
        }
    }
}
