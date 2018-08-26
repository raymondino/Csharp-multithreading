using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace LockAndMonitor
{
    internal class Account
    {
        Object aLock = new object();
        int balance;
        Random random = new Random();

        public Account(int InitialBalance)
        {
            this.balance = InitialBalance;
        }

        int Withdraw(int amount)
        {
            if(balance < 0)
            {
                throw new Exception("not enough balance");
            }

            Monitor.Enter(aLock); // this is where the lock begins
            try
            {
                if(this.balance >= amount)
                {
                    Console.WriteLine("Amount drawn: " + amount);
                    this.balance -= amount;
                    return this.balance;
                }
            }
            finally
            {
                Monitor.Exit(aLock); // finish the lock
            }
            return 0;
        }

        public void WithdrawRandomly()
        {
            for (int i = 0; i < 100; i++)
            {
                int balance = Withdraw(random.Next(2000, 5000));
                if (balance > 0)
                {
                    Console.WriteLine(Task.CurrentId + " Balance left: " + balance);

                }
            }
        }
    }
}
