using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // This demonstrates the behavior of fire-and-forget with regards to traceability when try-catching
        //
        // It is crucial to understand this behavior as this can lead to exceptions being absorbed by
        // the fire-and-forget tasks making it uncatchable

        try
        {
            await LimitedTask("Task A");
        }
        catch (Exception ex)
        {
            // We will be able to catch the exception since we awaited the task
            Console.WriteLine(ex.ToString());
        }

        try
        {
            var _ = LimitedTask("Task B");
        }
        catch (Exception ex)
        {
            // We will not be able to catch the exception because we did a fire-and-forget
            Console.WriteLine(ex.ToString());
        }

        Console.ReadKey();
    }

    static async Task LimitedTask(string taskName)
    {
        for (int i = 0; i < 5; i++)
        {
            Console.WriteLine($"Task running {taskName}...");
            await Task.Delay(1000);
        }

        throw new Exception($"{taskName} encountered an exception.");
    }
}