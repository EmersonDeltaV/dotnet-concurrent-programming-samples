using System;
using System.IO;
using System.Threading;

class Program2
{
    static void Main(string[] args)
    {
        // This has the same code as 4_Mutex Project. 
        // To observe the behavior, configure multiple startup projects to run 4_Mutex and 4_Mutex2 at the same time

        // Passing in a second parameter makes this a "named" mutex allowing it to operate at kernel level and share it with other processes
        using (Mutex mutex = new Mutex(false, "MyMutex"))
        {
            Console.WriteLine("Program 2 waiting to enter Mutex");

            // Replace this with a valid accessible path in your system
            string fullOutputFilePath = "D:\\mutexexample.txt";

            // Whichever grabs the Mutex last will have to wait until the other releases it before it can proceed.
            mutex.WaitOne();
            Console.WriteLine("Program 2 has entered the Mutex");

            string hello = "HELLO";
            foreach (var letter in hello)
            {
                File.AppendAllText(fullOutputFilePath, letter.ToString());
                Thread.Sleep(2000);
            }

            mutex.ReleaseMutex();
            Console.WriteLine("Program 2 released the Mutex");
        }
    }
}