using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace ThreadPoolDemo
{
    class Program
    {
        static void Main(string[] args)
        {
            // see if current thread is in thread pool
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);

            Employee employee = new Employee();
            employee.Name = "Raymond";
            employee.CompanyName = "Microsoft";

            // get into a thread pool
            ThreadPool.QueueUserWorkItem(new WaitCallback(DisplayEmployeeName), employee);
            // set minimum number of working threads and completion port threads
            // usually the min number of threads is the number of your CPU cores
            int workerThread = 0;
            int completionPortThread = 0;
            ThreadPool.GetMinThreads(out workerThread, out completionPortThread);
            ThreadPool.SetMaxThreads(workerThread * 2, completionPortThread * 2);

            Console.ReadKey();
        }

        private static void DisplayEmployeeName(object employee)
        {
            // see if current thread is in thread pool
            Console.WriteLine(Thread.CurrentThread.IsThreadPoolThread);

            Employee emp = employee as Employee;
            Console.WriteLine(emp.Name);
        }
    }

    public class Employee
    {
        public string Name { get; set; }
        public string CompanyName { get; set; }
    }
}
