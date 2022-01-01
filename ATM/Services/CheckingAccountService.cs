using ATM.Models;
using System.Linq;

namespace ATM.Services
{
    public class CheckingAccountService
    {
        private ApplicationDbContext applicationDbContext;
        public CheckingAccountService(ApplicationDbContext applicationDbContext)
        {
            this.applicationDbContext = applicationDbContext;
        }

        public void CreateCheckingAccount(string firstName, string lastName,string userId,decimal initialBalance)
        {
           
            var chk = (123456 + applicationDbContext.CheckingAccounts.Count()).ToString().PadLeft(10, '0');
            var ca = new CheckingAccount { AccountNumber = chk, FirstName = firstName, LastName = lastName, Balance = initialBalance, ApplicationUserId = userId };
            applicationDbContext.CheckingAccounts.Add(ca);
            applicationDbContext.SaveChanges();
        }
        public void UpdateBALANCE(int checkingAccountId)
        {
            var checkingAccount = applicationDbContext.CheckingAccounts.Where(e => e.Id == checkingAccountId).First();
            checkingAccount.Balance = applicationDbContext.Transactions.Where(e => e.CheckingAccountId == checkingAccountId).Sum(c => c.Amount);
            applicationDbContext.SaveChanges();
        }

    }
}