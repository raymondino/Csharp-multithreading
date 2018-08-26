/*
 * locking is very handy to avoid any problems raised by resource sharing. 
 * whenever you have shared resources among threads, locking is almost a must-have,
 * you should think of a right locking strategy
 */

namespace SharedResources
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading;
    using System.Threading.Tasks;

    public class Program
    {
        private static bool isCompleted; // static variable shared among threads
        static readonly object lockCompleted = new object();

        static void Main(string[] args)
        {
            Thread thread = new Thread(HelloWorld);
            thread.Start();

            HelloWorld();
        }

        private static void HelloWorld()
        {
            // the first thread that reaches this line will work,
            // after then, whichever thread reaches to this line of code, it will have to wait the lock to complete
            // when the lock is completed, the lock is released by the first thread, then the following thread will execute the code within the lock
            lock (lockCompleted) // use lock here
            {
                if (!isCompleted)
                {
                    isCompleted = true;
                    Console.WriteLine("hello world print only once");
                }
            }
        }
    }
}
