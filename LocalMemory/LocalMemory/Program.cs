using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LocalMemory
{
    class Program
    {
        static void Main(string[] args)
        {
            new Thread(Print1To30).Start();
            Print1To30();
        }

        private static void Print1To30()
        {
            for(int i = 1; i < 31; i++) // i is local varaible, which will be only accessed by its own thread.
            {
                Console.Write(i + " ");
            }
        }
    }
}
