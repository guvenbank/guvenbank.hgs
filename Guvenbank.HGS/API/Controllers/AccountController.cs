using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Models;
using Business.Abstract;
using Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        IAccountService accountService;

        public AccountController(IAccountService accountService)
        {
            this.accountService = accountService;
        }

        // GET: api/Account/5
        [HttpGet("{no}", Name = "Get")]
        public IActionResult Get(int no)
        {
            Account account = accountService.Get(no);

            if (account == null) return Ok(new { status = "failed", message = "Lütfen HGS numaranızı kontrol edin." });

            return Ok(new { status = "success", account.HgsNo, account.Balance, createdDate = account.Date, account.TcNo });
        }

        [HttpPost]
        public IActionResult Post([FromBody] TcModel tcModel)
        {
            Account accountCheck = accountService.Get(tcModel.TcNo);
            if (accountCheck == null)
            {
                int totalCount = accountService.TotalCount();
                string hgnNo = Convert.ToString(9001.ToString() + totalCount + 1001.ToString());
                int accountNo = Convert.ToInt32(hgnNo);

                Account account = new Account();
                account.Balance = 0.0m;
                account.HgsNo = accountNo;
                account.Date = DateTime.Now;
                account.TcNo = tcModel.TcNo;

                accountService.Add(account);

                return Ok(new { status = "success", account.HgsNo, account.Balance, createdDate = account.Date, account.TcNo });
            }
            else
            {
                return Ok(new { status = "failed", message = "Bu TC Kimlik No üzerine kayıtlı bir HGS vardır !"});
            }
           
        }

        // POST: api/Account/find
        [HttpPost("find")]
        public IActionResult Find([FromBody] TcModel tcModel)
        {
            Account account = accountService.Get(tcModel.TcNo);

            if (account == null) return Ok(new { status = "failed", message = "Bu TC Kimlik No üzerine kayıtlı bir HGS yoktur!" });

            else return Ok(new { status = "success", account.HgsNo });
        }

        // POST: api/Account/Deposit
        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] AccountModel accountModel)
        {
            Account account = accountService.Get(accountModel.HgsNo);

            if (account == null) return Ok(new { status = "failed", message = "Lütfen HGS numaranızı kontrol edin." });

            account.Balance += accountModel.Balance;

            accountService.Update(account);

            return Ok(new { status = "success", account.HgsNo, account.Balance, createdDate = account.Date, account.TcNo });
        }
    }
}