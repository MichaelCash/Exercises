using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Helpers;
using System.Web.Http;
using System.Web.Mvc;

using TransferMoney.Service.Data;
using TransferMoney.Service;
using TransferMoney.WebApi.Models;

namespace TransferMoney.WebApi.Controllers
{
    public class TransferMoneyController : ApiController
    {
        // GET api/values

        // POST api/values
        public string Post(TransferMoneyModel model)
        {
            var from = AccountService.GetAccount(model.FromAccount);
            var to = AccountService.GetAccount(model.ToAccount);
            var result = TransactionService.Transfer(from, to, model.Amount);
            return result.ToString();
        }

        // PUT api/values/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/values/5
        public void Delete(int id)
        {
        }
    }
}
