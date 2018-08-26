using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ReaderWriterlocks
{
    class Program
    {
        static ReaderWriterLockSlim readerWriterLockSlim = new ReaderWriterLockSlim();
        // a static variable that's shared among threads, so we need to use read write lock to make sure the data is not messed up
        static Dictionary<int, string> persons = new Dictionary<int, string>();
        static Random random = new Random();
        static void Main(string[] args)
        {
            var task1 = Task.Factory.StartNew(Read);
            var task2 = Task.Factory.StartNew(Write, "raymond");
            var task3 = Task.Factory.StartNew(Write, "msft");
            var task4 = Task.Factory.StartNew(Read);
            var task5 = Task.Factory.StartNew(Read);
            Task.WaitAll(task1, task2, task3, task4, task5);
        }

        static void Read()
        {
            for (int i = 0; i < 10; i++)
            {
                readerWriterLockSlim.EnterReadLock(); // read lock
                Thread.Sleep(50);
                readerWriterLockSlim.ExitReadLock();
            }
        }

        static void Write(object user)
        {
            for (int i = 0; i < 10; i++)
            {
                int id = GetRandom();
                readerWriterLockSlim.EnterWriteLock(); // write lock
                var person = "Person " + i;
                persons.Add(id, person);
                readerWriterLockSlim.ExitWriteLock();
                Console.WriteLine(user + " added " + person);
                Thread.Sleep(250);
            }
        }

        static int GetRandom()
        {
            lock (random)
            {
                return random.Next(2000, 5000);
            }
        }
    }
}
