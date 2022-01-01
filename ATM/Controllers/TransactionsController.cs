using ATM.Models;
using ATM.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace ATM.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();
        // GET: Transactions
        public ActionResult Deposit(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Deposit(Transaction trans) 
        {
            if (ModelState.IsValid)
            {
                db.Transactions.Add(trans);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBALANCE(trans.CheckingAccountId);
            }
            return RedirectToAction("Index","Home");

        }
        public ActionResult Withdraw(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Withdraw(Transaction trans)
        {
            var checkingAccount = db.CheckingAccounts.Find(trans.CheckingAccountId);
            if(checkingAccount.Balance < trans.Amount)
            {
                ModelState.AddModelError("Amount", "You have insufficient Funds!");
            }
            if (ModelState.IsValid)
            { 
                trans.Amount = -trans.Amount;
                db.Transactions.Add(trans);
                db.SaveChanges();
                var service = new CheckingAccountService(db);
                service.UpdateBALANCE(trans.CheckingAccountId);
                return RedirectToAction("Index", "Home");

            }
            return View(trans);
        }
        public ActionResult Transfer(int checkingAccountId)
        {
            return View();
        }

        [HttpPost]
        public ActionResult Transfer(TransferViewModel trans)
        {
            var maincheckingAccount = db.CheckingAccounts.Find(trans.CheckingAccountId);
            if (maincheckingAccount.Balance < trans.Amount)
            {
                ModelState.AddModelError("Amount", "You have insufficient Funds!");
            }

            //Check For secondary account
            var seccheckingAccount = db.CheckingAccounts.Where(c=>c.AccountNumber == trans.DestinationCheckingAccountNum).FirstOrDefault();
            if (seccheckingAccount == null)
            {
                ModelState.AddModelError("DestinationCheckingAccountNum", "Invalid Account!");
            }
            if (ModelState.IsValid)
            {
                //debt transaction for the current user
                db.Transactions.Add(new Transaction { CheckingAccountId = trans.CheckingAccountId, Amount = -trans.Amount});

                // credit transaction
                db.Transactions.Add(new Transaction { CheckingAccountId = seccheckingAccount.Id, Amount = trans.Amount });
                db.SaveChanges();

                var service = new CheckingAccountService(db);
                service.UpdateBALANCE(trans.CheckingAccountId);
                service.UpdateBALANCE(seccheckingAccount.Id);

                return PartialView("_TransferSuccess", trans);
            }
            return PartialView("_TransferForm");

        }
        public ActionResult QuickCash(int checkingAccountId,decimal cash)
        {
            var checkingAccount = db.CheckingAccounts.Find(checkingAccountId);
            var balance = checkingAccount.Balance;
            if(balance < cash)
            {
                return View("QuickCasInsufficientFunds");
            }
            //debt transaction for the current user
            db.Transactions.Add(new Transaction { CheckingAccountId = checkingAccountId, Amount = -cash });
            db.SaveChanges();
            var service = new CheckingAccountService(db);
            service.UpdateBALANCE(checkingAccountId);
            return View();
        }

    }
}