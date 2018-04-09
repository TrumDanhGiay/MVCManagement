using DuongTrang.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Management.Controllers.AdminController
{
    public class BorrowController : ApiController
    {
        private readonly IBorrowRepository borrowRepository;
        public BorrowController(IBorrowRepository _borrowRepository)
        {
            borrowRepository = _borrowRepository;
        }
        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            return borrowRepository.GetAllBorrow();
        }

        // GET api/<controller>/5
        public object Get(string id)
        {
            return borrowRepository.GetSingle(id);
        }

        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}