# dotnet-concurrent-programming-samples
This repository contains simple code examples to demonstrate concurrent programming concepts and behaviors. This can serve as the first step in understanding one of the complex yet commonly-used programming concept, or serve as a refresher of the foundational concepts for experienced developers.

## Projects Summary

### Project 1: AsyncAwait
Demonstrates asynchronous programming patterns using the Task class and the async and await keywords. Shows control over task execution order and concurrency, how tasks can be run in sequence or in parallel, and how to synchronize concurrent tasks before proceeding with further operations.

### Project 2: CancellationToken
Demonstrates the use of cancellation tokens with asynchronous tasks to implement a controlled shutdown of background operations. It consists of two primary components: a heartbeat logger and an input listener, both running on separate tasks. This intends to serve as a practical example of managing long-running tasks and gracefully terminating them upon user request.

### Project 3: Lock
Demonstrates the use of lock to synchronize access to a shared resource. Shows an example of accessing a shared buffer with and without locking and its effect on the buffer contents.

### Project 4: Mutex
Demonstrates the use of a named Mutex to synchronize access to a shared resource across processes. Comprises of two projects with relatively the same code that is intended to be run at the same time.

*Note: This example requires two projects to be run at the same time: 4_Mutex.csproj and 4_Mutex2.csproj. Configure multiple startup projects to run and debug at the same time or simply run the built executable files at the same time.*

### Project 5: Semaphore
Demonstrates the use of a semaphore to control access to a shared resource among multiple asynchronous tasks. It showcases how to limit the number of tasks that can simultaneously modify a shared resource, ensuring thread-safe operations without explicit locks.

### Project 6: FireAndForget
Demonstrates the difference in exception handling between awaited tasks and fire-and-forget tasks in an asynchronous context. It aims to highlight the importance of understanding how exceptions are propagated in asynchronous operations, especially when tasks are not awaited, leading to potential unhandled exceptions.