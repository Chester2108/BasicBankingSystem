using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class AccountModel
    {
        [Key]
        public Guid id { get; set; }
        public string username { get; set; }
        public float initialBalance { get; set; }
    }
}
