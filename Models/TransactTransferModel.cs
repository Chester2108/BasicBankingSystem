using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class TransactTransferModel
    {
        
        public Guid sourceAccountID { get; set; }
        public Guid? destinationAccountID { get; set; }
        public float transferAmount { get; set; }
    }
}
