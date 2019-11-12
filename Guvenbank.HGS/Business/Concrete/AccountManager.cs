using Business.Abstract;
using DataAccess.Abstract;
using DataAccess.Concrete;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Concrete
{
    public class AccountManager : IAccountService
    {
        IAccountDal accountDal;

        public AccountManager(IAccountDal accountDal)
        {
            this.accountDal = accountDal;
        }

        public void Add(Account account)
        {
            accountDal.Add(account);
        }

        public Account Get(int HGS)
        {
            return accountDal.Get(x => x.HgsNo == HGS);
        }

        public Account Get(string TC)
        {
            return accountDal.Get(x => x.TcNo == TC);
        }

        public int TotalCount()
        {
            return accountDal.GetList().Count;
        }

        public bool Update(Account account)
        {
            accountDal.Update(account);

            return true;
        }
    }
}
