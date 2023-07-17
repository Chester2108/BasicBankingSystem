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
    public class AccountController : Controller
    {
        private readonly BankingApiDbContext dbContext;
        public AccountController(BankingApiDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        [HttpGet("/api/GetAccounts")]
        public async Task<IActionResult> GetAccounts()
        {
            //var query = await (from a in dbContext.Account
            //                   join b in dbContext.AccountTransaction
            //                   on a.id equals b.destinationAccountID into joinedGroup
            //                   from b in joinedGroup.DefaultIfEmpty()
            //                   group b by new { a.id, a.username } into grouped
            //                   select new
            //                   {
            //                       id = grouped.Key.id,
            //                       username = grouped.Key.username,
            //                       initialBalance = grouped.Sum(i => i.transferAmount)
            //                   }).ToListAsync();

            //var subqueryB = from b in dbContext.AccountTransaction
            //                group b by b.destinationAccountID into g
            //                select new
            //                {
            //                    DestinationAccountID = g.Key,
            //                    TransferAmount = g.Sum(x => x.transferAmount)
            //                };

            //var subqueryC = from c in dbContext.AccountTransaction
            //                group c by c.sourceAccountID into g
            //                select new
            //                {
            //                    SourceAccountID = g.Key,
            //                    TransferAmount = g.Sum(x => x.transferAmount)
            //                };

            //var query = from a in dbContext.Account
            //            join b in subqueryB
            //            on a.id equals b.DestinationAccountID into joinedB
            //            from b in joinedB.DefaultIfEmpty()
            //            join c in subqueryC
            //            on a.id equals c.SourceAccountID into joinedC
            //            from c in joinedC.DefaultIfEmpty()
            //            group new { a, b, c } by new { a.id, a.username } into grouped
            //            select new
            //            {
            //                Id = grouped.Key.id,
            //                Username = grouped.Key.username,
            //                InitialBalance = grouped.Sum(i => (i.b.TransferAmount) - (i.c.TransferAmount))
            //            };


            var subqueryB = from b in dbContext.AccountTransaction
                            group b by b.destinationAccountID into g
                            select new
                            {
                                DestinationAccountID = g.Key,
                                TransferAmount = g.Sum(x => x.transferAmount)
                            };

            var subqueryC = from c in dbContext.AccountTransaction
                            group c by c.sourceAccountID into g
                            select new
                            {
                                SourceAccountID = g.Key,
                                TransferAmount = g.Sum(x => x.transferAmount)
                            };

            var query = from a in dbContext.Account
                        join b in subqueryB
                        on a.id equals b.DestinationAccountID into joinedB
                        from b in joinedB.DefaultIfEmpty()
                        join c in subqueryC
                        on a.id equals c.SourceAccountID into joinedC
                        from c in joinedC.DefaultIfEmpty()
                        select new
                        {
                            Id = a.id,
                            Username = a.username,
                            InitialBalance = ((float?)b.TransferAmount??0) - ((float?)c.TransferAmount ??0)
                        };
            //var account = await dbContext.Account.ToListAsync();
            return Ok(query);
        }

        [HttpPost("/api/CreateAccount")]
        public async Task<IActionResult> CreateAccount([FromBody] AddAccountModel addAccountModel)
        {
            var checkAccount = dbContext.Account.Where(x => x.username == addAccountModel.username).Count();

            if (checkAccount != 0)
            {
                return Json(new
                {
                    status = "Exists"
                });
            }

            var account = new AccountModel()
            {
                id = Guid.NewGuid(),
                username = addAccountModel.username,
                initialBalance = addAccountModel.initialBalance

            };

            

            var transact = new TransferModel()
            {
                destinationAccountID = account.id,
                transferAmount = account.initialBalance
            };


            await dbContext.Account.AddAsync(account);
            await dbContext.AccountTransaction.AddAsync(transact);
                    await dbContext.SaveChangesAsync();

            return Ok(account);

        }
    }
}
