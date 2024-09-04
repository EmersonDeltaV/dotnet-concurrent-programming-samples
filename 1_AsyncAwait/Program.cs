using System;
using System.Threading.Tasks;

class Program
{
    static async Task Main()
    {
        // Task 1 will run for one second
        await DelayWrite("Task1", 1000);

        // Since Task 1 is awaited, Task 2 wait for Task 1 to finish
        // before running for 3 seconds
        await DelayWrite("Task2", 3000);                

        // The following tasks are not individually awaited
        // They will all run at the same time.
        Task task3 = DelayWrite("Task3", 2000);
        Task task4 = DelayWrite("Task4", 6000);
        Task task5 = DelayWrite("Task5", 2000);

        // Here is where we await all the tasks above by referencing their variable
        // This will only proceed when all of them has finished executing
        await Task.WhenAll(task3, task4, task5);

        // Task 6 will only run when all of the previously awaited tasks are done.
        await DelayWrite("Task6", 3000);

        Console.ReadKey();
    }

    static async Task DelayWrite(string id, int delay)
    {
        Console.WriteLine($"[{DateTime.UtcNow}] {id} Started");
        await Task.Delay(delay);
        Console.WriteLine($"[{DateTime.UtcNow}] {id} Done");
    }
}