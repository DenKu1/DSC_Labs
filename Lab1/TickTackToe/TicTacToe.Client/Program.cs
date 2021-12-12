using System;

namespace TicTacToe.Client
{
    class Program
    {
        static void Main()
        {
            try
            {
                var client = new GameClient();

                client.Start();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error! Exception: {ex.Message}");

                Console.ReadKey();
            }
        }
    }
}
