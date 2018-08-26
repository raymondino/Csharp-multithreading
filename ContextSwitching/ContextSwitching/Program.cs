/*
 * One process can contain multiple threads that are sharing the resources within the processes. 
 * It's totally dependent on the operating system to determine which thread takes over.
 * In computing, a context switch is the process of storing the state of a process or of a thread, 
 * so that it can be restored and execution resumed from the same point later.
 * https://en.wikipedia.org/wiki/Context_switch
 */

namespace ContextSwitching
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args) // the main thread
        {
            // step 1: declare a thread, haven't firing this thread yet
            Thread thread = new Thread(WriteUsingNewThread); // pass in the name of a function

            // step 2: let the thread scheduler know to spawn off this thread
            thread.Start(); // a worker thread starts to work
            thread.Name = "worker thread1"; // give a worker thread a name for easy debugging

            // the following code runs in the main thread
            Thread.CurrentThread.Name = "main thread"; // give current thread, which is the main thread, a name
            for (int i = 0; i < 1000; i++)
            {
                Console.WriteLine(string.Format(" A {0} ", i));
            }
        }

        private static void WriteUsingNewThread()
        {
            for(int i = 0; i < 1000; i++)
            {
                Console.WriteLine(string.Format(" Z {0} ", i));
            }
        }
    }
}
