using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace TaskIO
{
    class Program
    {
        static void Main(string[] args)
        {
            // Task.Factory is another way to create a Task
            Task<string> task = Task.Factory.StartNew<string>(
                    () => GetPosts("https://jsonplaceholder.typicode.com/posts")
                );

            SomethingElse();
            task.Wait(); // I want to control this task to wait, if I don't want to control it, I can remove it
            try
            {
                // task.Result property will make the task to wait, if no Task.Wait() is called. 
                // but you still can use Task.Wait() if you want to control when and where to wait for the task.
                Console.WriteLine(task.Result);
            }
            // important to wrap this exception for task.Result, which is an AggregateException
            catch(AggregateException ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        private static void SomethingElse()
        {
            // doing some else
        }

        private static string GetPosts(string url)
        {
            // use using statement to dispose object automatically after we've made the call
            using (var client = new System.Net.WebClient())
            {
                return client.DownloadString(url);
            }
        }
    }
}
