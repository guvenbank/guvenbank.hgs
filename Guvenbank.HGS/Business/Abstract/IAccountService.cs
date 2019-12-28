using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAccountService
    {
        void Add(Account account);

        Account GetWithHgsNo(string HGS);

        Account Get(string TC);

        bool Update(Account account);

        int TotalCount();

        List<Account> GetList(string TC);

    }
}
