using System;

namespace TicTacToe.Server
{
    class Program
    {
        static void Main()
        {
            try
            {
                var server = new GameServer();

                server.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! Exception: {ex.Message}");

                Console.ReadKey();
            }
        }
    }
}