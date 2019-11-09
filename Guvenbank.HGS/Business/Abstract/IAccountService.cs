using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business.Abstract
{
    public interface IAccountService
    {
        void Add(Account account);

        Account Get(int HGS);

        bool Update(Account account);

        int TotalCount();

    }
}
