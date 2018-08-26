/*
 * So if you notice this is a situation of a deadlock. 
 * And it's only happening because of the way this execution is. 
 * A lot of times when we have nested locks we forget the impact it can create if some other thread is going to access a nested lock. 
 * So nested locks are extremely powerful but at the same time nested locks could be misused by another developer who may not know that you have it as one of the nested locks.
 * So anytime you add a nested lock you have to be very very careful and be sure to protect that lock.
 * So if I were to do control f5 you'll notice that the thread is blocked right there and we won't really see any other thing happen.
 */


namespace DeadLock
{
    using System;
    using System.Threading;

    class Program
    {
        static void Main(string[] args)
        {
            object lock1 = new object();
            object lock2 = new object();

            new Thread(
                () => {
                    lock (lock1) {
                        Console.WriteLine("lock1 obtained");
                        Thread.Sleep(2000);
                        lock (lock2) { // the worker thread will be blocked because main thread hasn't released lock2
                            Console.WriteLine("lock2 obtained");
                        }
                    }
                }
            ).Start();

            lock(lock2)
            {
                Console.WriteLine("main thread lock2 obtained");
                Thread.Sleep(2000);
                lock (lock1) // main thread will be blocked because worker thread hasn't released lock1
                {
                    Console.WriteLine("lock1 obtained");
                }
            }
        }
    }
}
