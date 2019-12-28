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
        public IActionResult Get(string no)
        {
            Account account = accountService.GetWithHgsNo(no);

            if (account == null) return Ok(new { status = "failed", message = "Lütfen HGS numaranızı kontrol edin." });

            return Ok(new { status = "success", account.HgsNo, account.Balance, createdDate = account.Date, account.TcNo });
        }

        // POST: api/Account
        [HttpPost]
        public IActionResult Post([FromBody] TcModel tcModel)
        {

            int totalCount = accountService.TotalCount();
            string hgsNo = Convert.ToString(9001.ToString() + totalCount + 1001.ToString());

            Account account = new Account();
            account.Balance = 0.0m;
            account.HgsNo = hgsNo;
            account.Date = DateTime.Now;
            account.TcNo = tcModel.TcNo;

            accountService.Add(account);

            return Ok(new { status = "success", account.HgsNo, account.Balance, createdDate = account.Date, account.TcNo });   
        }

        // POST: api/Account/find
        [HttpPost("find")]
        public IActionResult Find([FromBody] TcModel tcModel)
        {
            List<Account> accounts = accountService.GetList(tcModel.TcNo);

            if (accounts == null && accounts.Count <= 0) return Ok(new { status = "failed", message = "Bu TC Kimlik No üzerine kayıtlı bir HGS yoktur!" });

            else
            {
                List<object> accountObjects = new List<object>();

                foreach (Account account in accounts)
                {
                    accountObjects.Add(new { account.HgsNo });
                }

                return Ok(new { status = "success", accounts = accountObjects });
            } 
        }

        // POST: api/Account/Deposit
        [HttpPost("deposit")]
        public IActionResult Deposit([FromBody] AccountModel accountModel)
        {
            Account account = accountService.GetWithHgsNo(accountModel.HgsNo);

            if (account == null) return Ok(new { status = "failed", message = "Lütfen HGS numaranızı kontrol edin." });

            account.Balance += accountModel.Balance;

            accountService.Update(account);

            return Ok(new { status = "success", account.HgsNo, account.Balance, createdDate = account.Date, account.TcNo });
        }
    }
}