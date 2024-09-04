using System;
using System.Threading;
using System.Threading.Tasks;

internal class Program
{
    static void Main(string[] args)
    {
        // We create our token source
        CancellationTokenSource cts = new CancellationTokenSource();

        var token = cts.Token;
        token.Register(() => { Console.WriteLine("Cancellation sent."); });

        // We have a heartbeat logger t1 that runs infinitely
        // We pass our cancellation token as an argument
        Task t1 = Task.Run(() => HeartbeatLogger(token), token);

        // We have a separate thread that accepts input at anytime
        // We pass in the cancellation token source from where we can trigger the cancel request
        Task t2 = Task.Run(() => InputThread(cts));

        // t1 will run forever and will only stop
        // if we receive an input of "cancel" in t2, 

        t2.Wait();

        // Make it a practice to always dispose of cancellation tokens after using it
        // to avoid potential memory leaks
        cts.Dispose();
    }

    static async Task HeartbeatLogger(CancellationToken token)
    {
        while (true)
        {
            // This is the line that checks if cancellation has been requested.
            // This should throw an exception, but since it is running on a fire-and-forget task
            // it will not be propagated to the caller
            token.ThrowIfCancellationRequested();

            Console.WriteLine($"[{DateTime.UtcNow}] ");
            await Task.Delay(1000);
        }
    }

    static async Task InputThread(CancellationTokenSource tokenSource)
    {
        while (true)
        {
            var input = Console.ReadLine();

            if (input == "cancel")
            {
                // This is where we trigger the cancellation
                tokenSource.Cancel();
            }
            else
            {
                Console.WriteLine($"Received input: {input}");
            }

            await Task.Delay(5);
        }
    }
}