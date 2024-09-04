using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Lock
{
    internal class Program
    {
        // Critical resource
        private static string _buffer = "";

        private static object _lock = new object();

        static async Task Main(string[] args)
        {
            Task t0 = Task.Run(() => BufferPrinter());

            // The tasks below are run, printing to buffer WITHOUT locking
            // Observe that t2 writes into the critical resource without waiting for t1 to finish
            Task t1 = Task.Run(() => WriteHello());
            await Task.Delay(5);
            Task t2 = Task.Run(() => WriteWorld());

            await Task.WhenAll(t1, t2);
            await Task.Delay(2000);

            _buffer = "";

            // The tasks below are run, printing to buffer WITH locking
            // Observe that t4 will have to wait for the lock to be released by t3 before it is able to write
            Task t3 = Task.Run(() => WriteHelloWithLock());
            await Task.Delay(5);
            Task t4 = Task.Run(() => WriteWorldWithLock());

            await Task.WhenAll(t3, t4);
            await Task.Delay(2000);
        }

        // This is the version of WriteHello with Lock
        static async Task WriteHelloWithLock()
        {
            lock (_lock)
            {
                string hello = "HELLO";
                foreach (var letter in hello)
                {
                    _buffer += letter;
                    Thread.Sleep(1000);
                }
            }

            await Task.CompletedTask;
        }


        // This is the version of WriteWorld with Lock
        static async Task WriteWorldWithLock()
        {
            lock (_lock)
            {
                string world = "WORLD";
                foreach (var letter in world)
                {
                    _buffer += letter;
                    Thread.Sleep(1000);
                }
            }
            await Task.CompletedTask;
        }


        // This is the version of WriteHello without Lock
        static async Task WriteHello()
        {
            string hello = "HELLO";
            foreach (var letter in hello)
            {
                _buffer += letter;
                Thread.Sleep(1000);
            }

            await Task.CompletedTask;
        }

        // This is the version of WriteWorld without Lock
        static async Task WriteWorld()
        {
            string world = "WORLD";
            foreach (var letter in world)
            {
                _buffer += letter;
                Thread.Sleep(1000);
            }

            await Task.CompletedTask;
        }

        // The buffer printer which prints the content of our critical resource buffer
        static async Task BufferPrinter()
        {
            while (true)
            {
                Console.Clear();
                Console.WriteLine(_buffer);
                await Task.Delay(200);
            }
        }
    }
}
