using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DuongTrang.Core.IServices;

namespace Management.Controllers.AdminController
{
    public class ReaderController : ApiController
    {
        private readonly ICardReaderRepository cardReaderRepository;
        public ReaderController(ICardReaderRepository _cardReaderRepository)
        {
            cardReaderRepository = _cardReaderRepository;
        }
        // GET api/<controller>
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/<controller>/5
        public object Get(string id)
        {
            return cardReaderRepository.GetReaderInfo(Guid.Parse(id));
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