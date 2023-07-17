using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Models
{
    public class AddAccountModel
    {
        public int? id { get; set; }
        public string username { get; set; }
        public float initialBalance { get; set; }
    }
}
