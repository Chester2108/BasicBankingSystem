using BankingSystem.Data;
using BankingSystem.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BankingSystem.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TransferController : Controller
    {
        private readonly BankingApiDbContext dbContext;
        public TransferController(BankingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("/api/GetTransferTo")]
        public async Task<IActionResult> GetTransferTo(string username)
        {
            var account = await dbContext.Account.Where(x=> x.username != username).ToListAsync();
            return Ok(account);
        }
        [HttpPost("/api/TransferTransaction")]
        public async Task<IActionResult> TransferTransaction(TransactTransferModel transfer)
        {
            var transact = new TransferModel()
            {
                sourceAccountID = transfer.sourceAccountID,
                destinationAccountID = transfer.destinationAccountID,
                transferAmount = transfer.transferAmount
            };

            await dbContext.AccountTransaction.AddAsync(transact);
            await dbContext.SaveChangesAsync();

            return Ok(transact);

        }
      
    }
}
