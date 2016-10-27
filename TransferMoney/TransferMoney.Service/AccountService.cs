using System;
using System.Collections.Generic;
using System.Linq;
using TransferMoney.Service.Cache;
using TransferMoney.Service.Data;
namespace TransferMoney.Service
{
    public class AccountService
    {
        public static bool ChangeBalance(Account account, decimal amount, TMEntities1 context)
        {
                var entity = context.Accounts.FirstOrDefault(p => p.AccountId == account.AccountId);
                if (entity != null)
                {
                    if (entity.Balance >= amount * -1)
                    {
                        entity.Balance += amount;
                        return true;
                    }
                }
                return false;
        }

        public static bool Widthraw(Account account, decimal amount, TMEntities1 context)
        {
            amount = amount * -1;
            return ChangeBalance(account, amount, context);
        }

        public static bool Deposit(Account account, decimal amount, TMEntities1 context)
        {
            return ChangeBalance(account, amount, context);
        }

        public static List<Account> GetAccounts()
        {
            return new TMEntities1().Accounts.ToList();
        }

        public static Account Save(Account account) {
            using (var context = new TMEntities1()) {
                var entity = context.Accounts.FirstOrDefault(p => p.Name.Equals(account.Name));
                if (entity != null)
                {
                    entity.Balance = account.Balance;
                }
                else
                {
                    if(string.IsNullOrEmpty(account.Number))
                    {
                        account.Number = "11111";
                    }
                    account.Token = account.Number;
                    account.CreatedDate = DateTime.Now;
                    context.Accounts.Add(account);
                }
                context.SaveChanges();
                return account;
            }
        }

        public static void ResetAccountDatabase() {
            using (var context = new TMEntities1())
            {
                context.Database.ExecuteSqlCommand("TRUNCATE TABLE [Accouunt]");
                context.SaveChanges();
            }
        }


        public static Account GetAccount(string name)
        {
            return MemCache.GetOrSet(name, 3*60, () => GetAccountFromDB(name));
        }

        public static Account GetAccountFromDB(string name) {
            var account =  new TMEntities1().Accounts.FirstOrDefault(p => p.Name.Equals(name) );
            if (account == null)
            {
                account = new Data.Account() { Name = name, Balance = 1000, CreatedDate = DateTime.Now, Number = "11111", Token = "11111" };
                Save(account);
            }
            return account;

        }

    }
}
