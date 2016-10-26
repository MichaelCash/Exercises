using System;
using System.Threading;
using TransferMoney.Service;
using TransferMoney.Service.Data;
namespace ConsoleTest
{
    class Program
    {
        static void Main(string[] args)
        {

            AccountService.Save(new Account() { Name = "Account1", Number ="11111",  Balance = 1000 });
            AccountService.Save(new Account() { Name = "Account2", Number = "22222", Balance = 2000 });


            var result1 = TransferStatus.Successful;
            var result2 = TransferStatus.Successful;

            Thread t1 = new Thread(new ThreadStart(() => { result1 = TransferMoney("Account1", "Account2", 10); }));
            Thread t2 = new Thread(new ThreadStart(() => { result2 = TransferMoney("Account2", "Account1", 20); }));

            t1.Start();
            t2.Start();

            // waiting until threads done.
            t1.Join();
            t2.Join();

            Console.WriteLine(string.Format("T1 = {0}, T2 = {1}", result1.ToString(), result2.ToString()));


            var accounts = AccountService.GetAccounts();
            foreach (var a in accounts)
            {
                Console.WriteLine(string.Format("Name = {0}, Balance = {1}", a.Name, a.Balance));
            }
        //    AccountService.ResetAccountDatabase();
        }

        public static TransferStatus TransferMoney(string fromName, string toName, decimal amount)
        {
            var src = AccountService.GetAccount(fromName);
            var des = AccountService.GetAccount(toName);
            return TransactionService.Transfer(src, des, amount, 500, true);
        }
    }
}
