using Core;
using System;
using System.Collections.Generic;
using System.Text;

namespace Entities
{
    public class Account : IEntity
    {
        public Guid Id { get; set; }
        public int HgsNo { get; set; }
        public string TcNo { get; set; }
        public DateTime Date { get; set; }
        public decimal Balance { get; set; }
    }
}
