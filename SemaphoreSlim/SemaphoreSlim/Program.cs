using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Semaphores
{
    class Program
    {
        static SemaphoreSlim semaphoreSlim = new SemaphoreSlim(3); // only allow 3 threads to access the resource
        static void Main(string[] args)
        {
            for (int i = 0; i < 10; i++)
            {
                new Thread(EnterSemaphore).Start(i + 1); // i+1 indicates the id number of the thread, passed into EnterSemaphore(object id) below
            }
        }

        private static void EnterSemaphore(object id)
        {
            Console.WriteLine(id + " is waiting to be part of the club");
            semaphoreSlim.Wait(); // will make the thread wait IF there are already 3 threads using the resource
            Console.WriteLine(id + " part of the club"); // if the wait is over, then the thread can use the resource
            Thread.Sleep(1000 / (int)id);
            Console.WriteLine(id + " left the club");
        }
    }
}
