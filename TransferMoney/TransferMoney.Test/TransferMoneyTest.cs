using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using TransferMoney.Service;
using TransferMoney.Service.Data;
using System.Threading;
using System.Threading.Tasks;

namespace TransferMoney.Test
{
    [TestClass]
    public class TransferMoneyTest
    {
        private double delta = 0.00001;
  
        [TestMethod]
        public void TransferNoDeadlockTest()
        {
            // Using Lock Order and Thead.Sleep to prove without deadlock.
            // Thead1 Lock A and sleep, waiting thead 2 Lock B.

            AccountService.Save(new Account() { Name = "Michael", Balance = 1000 });
            AccountService.Save(new Account() { Name = "Thor", Balance = 2000 });


            var result1 = TransferStatus.Fail;
            var result2 = TransferStatus.Fail;

            Thread tid1 = new Thread(new ThreadStart(() => { result1 = Transfer("Michael", "Thor", 10, 500, true); }));
            Thread tid2 = new Thread(new ThreadStart(() => { result2 = Transfer("Thor", "Michael", 20, 500, true); }));

            tid1.Start();
            tid2.Start();

            // waiting util thread done.
            tid1.Join();
            tid2.Join();
            Assert.AreEqual(result1, TransferStatus.Successful);
            Assert.AreEqual(result2, TransferStatus.Successful);
        }

        [TestMethod]
        public void TransferDeadLockTest()
        {

            AccountService.Save(new Account() { Name = "Michael", Balance = 1000 });
            AccountService.Save(new Account() { Name = "Thor", Balance = 2000 });

            var result1 = TransferStatus.Fail;
            var result2 = TransferStatus.Fail;

            // Without order.
            Thread tid1 = new Thread(new ThreadStart(() => { result1 = Transfer("Michael", "Thor", 10, 500, false); }));
            Thread tid2 = new Thread(new ThreadStart(() => { result2 = Transfer("Thor", "Michael", 20, 500, false); }));

            tid1.Start();
            tid2.Start();

            // waiting util thread done.
            tid1.Join();
            tid2.Join();
            Assert.AreEqual(result1, TransferStatus.DeadLock);
            Assert.AreEqual(result2, TransferStatus.DeadLock);
        }


        // Test transfer money with auto multi thread
        [TestMethod]
        public void TransferParallelTest()
        {
            decimal balance = 100000;

            AccountService.Save(new Account() { Name = "Account1", Number = "11111", Balance = balance });
            AccountService.Save(new Account() { Name = "Account2", Number = "22222", Balance = balance });

            int numbers = 20;

            // Test mutli thread parallel
            Parallel.For(0, numbers, index =>
            {
                // first time
                Transfer("Account1", "Account2", 10);
                Transfer("Account2", "Account1", 20);

                // second time
                Transfer("Account1", "Account2", 20);
                Transfer("Account2", "Account1", 30);
            });

            var a1 = AccountService.GetAccountFromDB("Account1");
            var a2 = AccountService.GetAccountFromDB("Account2");

            Assert.AreEqual(a1.Balance, balance + 2 * (numbers * 10));
            Assert.AreEqual(a2.Balance, balance - 2 * (numbers * 10));
        }

        [TestMethod]
        public void TransferThreadTest()
        {
            decimal balance = 100000;

            AccountService.Save(new Account() { Name = "Account11", Balance = balance });
            AccountService.Save(new Account() { Name = "Account22", Balance = balance });
            AccountService.Save(new Account() { Name = "Account33", Balance = balance });
            AccountService.Save(new Account() { Name = "Account44", Balance = balance });



            var result1 = TransferStatus.Fail;
            var result2 = TransferStatus.Fail;
            var result3 = TransferStatus.Fail;
            var result4 = TransferStatus.Fail;

            // Test mutli thread parallel
            Thread t1 = new Thread(new ThreadStart(() => { result1 = Transfer("Account11", "Account22", 10); }));
            Thread t2 = new Thread(new ThreadStart(() => { result2 = Transfer("Account22", "Account33", 20); }));
            Thread t3 = new Thread(new ThreadStart(() => { result3 = Transfer("Account33", "Account44", 30); }));
            Thread t4 = new Thread(new ThreadStart(() => { result4 = Transfer("Account44", "Account11", 40); }));

            t1.Start();
            t2.Start();
            t3.Start();
            t4.Start();

            //waiting 

            t1.Join();
            t2.Join();
            t3.Join();
            t4.Join();

            // Check Successful
            Assert.AreEqual(result1, TransferStatus.Successful);
            Assert.AreEqual(result2, TransferStatus.Successful);
            Assert.AreEqual(result3, TransferStatus.Successful);
            Assert.AreEqual(result4, TransferStatus.Successful);

            var a1 = AccountService.GetAccountFromDB("Account11");
            var a2 = AccountService.GetAccountFromDB("Account22");
            var a3 = AccountService.GetAccountFromDB("Account33");
            var a4 = AccountService.GetAccountFromDB("Account44");

            Assert.AreEqual(a1.Balance, balance - 10 + 40);
            Assert.AreEqual(a2.Balance, balance - 20 + 10);
            Assert.AreEqual(a3.Balance, balance - 30 + 20);
            Assert.AreEqual(a4.Balance, balance - 40 + 30);
        }
        // Test Transfer money with manual thread

        private TransferStatus Transfer(string from, string to, decimal amount, int timeOut = 0, bool isOrder = true) {
            var src = AccountService.GetAccount(from);
            var des = AccountService.GetAccount(to);
            return TransactionService.Transfer(src, des, amount, timeOut, isOrder);
        }

    }
}
