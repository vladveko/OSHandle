using System;
using System.Threading;
using System.Runtime.InteropServices;

namespace HandleLab
{
    class Program
    {
        [DllImport("Kernel32.dll", SetLastError = true)]
        private static extern IntPtr GetStdHandle(int nStdHandle);

        private const int nStdHandle = -11;

        private static Mutex mutex = new Mutex();
        static void Main(string[] args)
        {
            new Thread(RunLoop).Start();
            new Thread(RunLoop).Start();

            Thread.Sleep(500);
            OSHandle cHandle = new OSHandle(GetStdHandle(nStdHandle));
            Console.WriteLine("Closing Handle..");
            cHandle.Dispose();
        }

        private static void RunLoop()
        {
            while (true)
            {
                mutex.Lock();
                try
                {
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Run");
                    Console.WriteLine($"Thread {Thread.CurrentThread.ManagedThreadId} - Run");
                }
                finally
                {
                    mutex.Unlock();
                }

                Thread.Sleep(10);
            }
        }
    }
}
