using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

using TransferMoney.Service.Data;

namespace TransferMoney.WebApi.Models
{
    public class TransferMoneyModel
    {
        public string FromAccount { get; set; }
        public string ToAccount { get; set; }
        public decimal Amount { get; set; }
    }
}