using System;
using System.Runtime.InteropServices;
using System.Threading.Tasks;

namespace Auction.Client
{
    static class ConsoleHelper
    {
        const int STD_INPUT_HANDLE = -10;

        [DllImport("kernel32.dll", SetLastError = true)]
        internal static extern IntPtr GetStdHandle(int nStdHandle);

        [DllImport("kernel32.dll", SetLastError = true)]
        static extern bool CancelIoEx(IntPtr handle, IntPtr lpOverlapped);

        public static ConsoleKey ReadKeyWithTimeout()
        {
            // Start the timeout
            var read = false;
            Task.Delay(25000).ContinueWith(_ =>
            {
                if (!read)
                {
                    // Timeout => cancel the console read
                    var handle = GetStdHandle(STD_INPUT_HANDLE);
                    CancelIoEx(handle, IntPtr.Zero);
                }
            });

            ConsoleKeyInfo key = new();

            try
            {
                // Start reading from the console
                key = Console.ReadKey();
                read = true;
            }
            // Handle the exception when the operation is canceled
            catch
            {
            }

            return key.Key;
        }
    }
}
