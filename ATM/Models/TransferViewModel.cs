using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace ATM.Models
{
    public class TransferViewModel
    {
        [DataType(DataType.Currency)]
        public decimal Amount { get; set; }
        [Required]
        public int CheckingAccountId { get; set; }
        public DateTime transactionDate { get; set; } = DateTime.Now;
        [Required]
        [Display(Name ="To Account #")]
        public string DestinationCheckingAccountNum { get; set; }
    }
}