using DuongTrang.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace Management.Controllers
{
    public class ValuesController : ApiController
    {
        IDataFirstRepository dataFirstRepository;
        public ValuesController(IDataFirstRepository _dataFirstRepository)
        {
            this.dataFirstRepository = _dataFirstRepository;
        }
        // GET api/<controller>
        public object Get()
        {
            var dataFist = dataFirstRepository.getDataFirst();
            return dataFirstRepository.getDataFirst();
        }

        // GET api/<controller>/5
        public string Get(int id)
        {
            return "value";
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