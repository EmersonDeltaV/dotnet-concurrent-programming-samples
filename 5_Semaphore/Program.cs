using System;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;

class Program
{
    // Critical resource
    private static string _buffer = "";

    // Setting to allow three accessors at a time
    private static Semaphore semaphore = new Semaphore(3, 3);

    static async Task Main(string[] args)
    {
        // All of the following will start at the same time
        // But only three of them will be able to operate on the critical resource at a time
        // Only when one of the exisitng accessors do a release can the waiters gain access to the resource
        Task t6 = BufferPrinter();
        Task t1 = WriteLetter("A", 6);
        Task t2 = WriteLetter("B", 3);
        Task t3 = WriteLetter("C", 6);
        Task t4 = WriteLetter("D", 9);
        Task t5 = WriteLetter("E", 3);

        await Task.WhenAll(t1, t2, t3, t4, t5, t6);
        Console.ReadKey();
    }

    static async Task WriteLetter(string letter, int repeat)
    {
        semaphore.WaitOne();

        for (int i = 0; i < repeat; i++)
        {
            _buffer += letter;
            await Task.Delay(1000);
        }

        semaphore.Release();
    }

    static async Task BufferPrinter()
    {
        int iteration = 1;
        while (iteration < 15)
        {
            Console.WriteLine($"[{iteration++}] " +
                $"A:{_buffer.Count(ch => ch == 'A')} " +
                $"B:{_buffer.Count(ch => ch == 'B')} " +
                $"C:{_buffer.Count(ch => ch == 'C')} " +
                $"D:{_buffer.Count(ch => ch == 'D')} " +
                $"E:{_buffer.Count(ch => ch == 'E')}");
            await Task.Delay(1000);
        }
        Console.ReadKey();
    }
}