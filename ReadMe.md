# How Threading Works
1. multithreading is managed internally by a thread scheduler
2. .NET Common Language Runtime (CLR) delegates thread scheduling task to the operating systems

# What happens to the shared resources of threads inside of one process/application
1. CLR assigns each thread its own local memory stack to keep local variables separate
2. a separate copy of local variables is created on each thread's memory stack
3. to share variable among threads, one possible way is to use static variables

# Thread Schedulers
1. it ensures all active threads are allocated appropriate execution time

# Preempted Thread
1. is a thread that has execution interrupted, usually by an external factor such as timeslicing
2. thread has no control over when and where it is preempted

# Threads vs Tasks
1. threads:
    * threads are the lowest-level constructs of multi-threading
    * working with threads can be challenging
    * it's a complicated process to return a value from a separate thread
        * by using Thread.join() to allow threads to wait for another thread's completion
        * as well as need some shared memory (which could potentially be changed by different threads)
2. tasks:
    * tasks are a higher-level abstractions
    * tasks are capable of returning values
    * tasks can be chained
    * tasks may use a thread pool
    * tasks may be used for I/O bound operation
# Task Chaining
0. some times we need to do something right after a task completes, that's the scenario for task chaining
1. Continuation: asynchronous task that is invoked by another task called an antecedent (antecedent --> continuation)
2. by using task chaining, we can:
    * pass data from antecedent to continuation task
    * pass exceptions from antecedent to continuation task
    * precisely control how the continuation is invoked
    * can cancel a continuation, not only before it starts, but even while it's running
    * can invoke multiple continuations from the same antecedent
    * can invoke continuation based on the completion of any antecedents (you can do that when all the antecdents complete, too)

# Threads vs Process
1. processes run in parallel in the computer
2. processes are fully isolated from each other 
3. threads run in parallel in a single process 
4. threads have a limited degree of isolation
    * they share heap memory with other threads running in the same application
    * they isolate their local memories where the local variables are stored, and cannot be accessed by other threads

# Thread Pool
1. every thread requires a certain overhead 
    * a few hundred miliseconds including time spent in:
        * creating a flash local variable stack
        * spawning off the thread
    * cosumes roughly around one megabyte of memory
2. one of major benefits of using a thread pool is to reduce the performance penalty by sharing and recycling threads
3. thread pool only creates background threads
    * one difference from a forground thread: it will die if the main thread dies
    * to create a background thread, we simply create a new thread and set its background property to true
4. thread pool limits the number of threads that can run simultaneously
5. when the limit is reached, all jobs form a queue and begin only when another job finishes
6. use Thread.CurrentThread.IsThreadPoolThread property to determine if execution is happening on a pool thread
7. ways to enter a thread pool
    * Task parallel library
    * Asynchronous delegate
    * Background work
    * Call ThreadPool.QueueUserWorkItem

# CPU bound vs I/O bound
1. CPU bound:
    * Use resources of a local machine/CPU
    * computation-intensive operations (like calculating fibonacci value)
2. I/O bound
    * out-of-process calls, something like call a database/API/web server
    * operations can take an indeterminant amount of time because we are waiting on something external to us.
    * we want to make a call, then release the resources, and keep a call back, which will be called when we get the results back from that call
    * for I/O bound operations, tasks can use TaskCompletionSource, which makes our lives easier. 

# Synchronization
1. act of coordinating actions of multiple threads or tasks running concurrently
2. necessary when running multiple threads to get predictable outcomes
3. methods to achieve synchronization
    * blocking methods (sleep, join, Task.Wait()): stop the execution util the task/thread is completed
        * blocked threads do not consume CPU, but do consume memory
        * blocking v.s. spinning (spinning consumes the CPU)
    * locks
        * limit the number of threads
        * exclusive lock
            * allow only one thread to access a certain section piece of code
            * alternative is to use Monitor.Enter/Moniter.Exit, it's just a syntax difference, behind the scenes, it has the same implementation as locks
            * two kinds of exclusive locks
                * lock
                    * nexted locks: only the parental lock does the work (but watch out for dead locks!)
                * mutex
        * nonexclusive lock
            * semaphore
            * semaphoreSlim
            * Reader/writer
            * they allow multiple threads to access a resource
    * signals
        * signaling constructs: let the threads pause utill they receive a signal from another thread
        * two methods: Event wait handles, and monitor's wait/pause methods
        * .net framework 4 also introduced CountDownEvent and Barrier classes
    * nonblocking constructs
        * Thread.MemoryBarrier
        * Thread.VolatileRead
        * Thread.VolatileWrite
        * the volatile keyboard
        * the interlock class
        * protect access to a common field.