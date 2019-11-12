using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Models
{
    public class AccountModel
    {
        public int HgsNo { get; set; }
        public TcModel TCModel { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
    }
}
