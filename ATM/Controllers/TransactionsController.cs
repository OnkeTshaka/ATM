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
    }
}