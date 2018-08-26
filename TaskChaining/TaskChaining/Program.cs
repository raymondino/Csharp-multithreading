/*
 * this example demonstrates how tasks are chained, using antecedent and continuation
 */
namespace TaskChaining
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;

    class Program
    {
        static void Main(string[] args)
        {
            Task<string> antecedent = Task.Run(() => 
                {
                    Task.Delay(2000); // recommend to use this from Task library if it's a task. Don't use Thread.sleep()
                    return DateTime.Today.ToShortDateString();
                });
            Task<string> continuation = antecedent.ContinueWith(x => 
                {
                    return "Today is " + antecedent.Result;
                });
            Console.WriteLine("this will print before the result");
            Console.WriteLine(continuation.Result);
        }
    }
}
