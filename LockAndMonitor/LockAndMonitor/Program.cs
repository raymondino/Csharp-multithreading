using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LockAndMonitor
{
    class Program
    {
        static void Main(string[] args)
        {
            Account account = new Account(20000);
            Task task1 = Task.Factory.StartNew(() => account.WithdrawRandomly());
            Task task2 = Task.Factory.StartNew(() => account.WithdrawRandomly());
            Task task3 = Task.Factory.StartNew(() => account.WithdrawRandomly());
            Task.WaitAll(new Task[] { task1, task2, task3 });
            Console.WriteLine("all tasks completed");
        }
    }
}
