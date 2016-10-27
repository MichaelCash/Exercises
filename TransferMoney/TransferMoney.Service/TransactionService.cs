using System;
using System.Threading;
using TransferMoney.Service.Data;

namespace TransferMoney.Service
{
    public class TransactionService
    {
        static TimeSpan timeout = TimeSpan.FromSeconds(5);

        public static TransferStatus Transfer(Account from, Account to, decimal amount, int delayTime = 0, bool isOrder = true)
        {
            if (LockOrderAccounts(from, to, delayTime, isOrder))
            {
                var result = CompletedTransfer(from, to, amount);
                UnLockAccounts(from, to);
                return result ? TransferStatus.Successful : TransferStatus.Fail;
            }
            return TransferStatus.DeadLock;
        }

        private static bool CompletedTransfer(Account src, Account des, decimal amount)
        {
            using (var context = new TMEntities1())
            {
                using (var dbContextTransaction = context.Database.BeginTransaction())
                {
                    try
                    {
                        if (AccountService.Widthraw(src, amount, context))
                        {
                            AccountService.Deposit(des, amount, context);
                            context.SaveChanges();
                            dbContextTransaction.Commit();
                            return true;
                        }
                    }
                    catch (Exception ex)
                    {
                        dbContextTransaction.Rollback();
                    }
                }
            }
            return false;
        }

        private static bool LockOrderAccounts(Account from, Account to, int delayTime, bool isOrder)
        {
            if (isOrder)
            {
                if (from.AccountId < to.AccountId)
                {
                    return LockAccounts(from, to, delayTime);
                }
                else
                {
                    return LockAccounts(to, from, delayTime);
                }
            }
            else
            {
                return LockAccounts(from, to, delayTime);
            }
        }


        private static bool LockAccounts(Account from, Account to, int delayTime)
        {
            if (Monitor.TryEnter(from, timeout))
            {
                Console.WriteLine("Lock " + from.Name);
                // Waiting another thread lock From Account
                Thread.Sleep(delayTime);
                if (Monitor.TryEnter(to, timeout))
                {
                    Console.WriteLine("Lock " + to.Name);
                    // Waiting another thead lock To Accmount.
                    Thread.Sleep(delayTime);
                    return true;
                }
                Console.WriteLine(string.Format("Time out, cannot enter account name: {0}", to.Name));
                return false;
            }
            Console.WriteLine(string.Format("Time out, cannot enter account name: {0}", from.Name));
            return false;

        }
        private static void UnLockAccounts(Account from, Account to)
        {
            Monitor.Exit(from);
            Console.WriteLine("Exit " + from.Name);
            Monitor.Exit(to);
            Console.WriteLine("Exit " + to.Name);
        }

    }
}
