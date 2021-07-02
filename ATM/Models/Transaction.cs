using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class Transaction
    {
        public int Id { get; set; }
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }

        public DateTime transactionDate { get; set; } = DateTime.Now;
        [Required]
        public int CheckingAccountId { get; set; }
        public virtual CheckingAccount CheckingAccount { get; set; }
    }
}