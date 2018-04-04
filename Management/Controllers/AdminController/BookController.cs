using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;
using Management.APICustomAuthorize;
using Management.Property;
using DuongTrang.Core.CustomModels;

namespace Management.Controllers.AdminController
{
    public class BookController : ApiController
    {
        private readonly IBookRepository _bookRepository;
        public BookController(IBookRepository bookRepository)
        {
            _bookRepository = bookRepository;
        }
        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            return _bookRepository.GetAllBook();
        }

        // GET api/<controller>/5
        public object Get(string id)
        {
            return _bookRepository.GetSingle(id);
        }

        [AuthorizeRole(ListRoles.Administrator)]
        // POST api/<controller>
        public void Post([FromBody]string value)
        {
        }

        [AuthorizeRole(ListRoles.Administrator)]
        // PUT api/<controller>/5
        public void Put(int id, [FromBody]string value)
        {
        }

        [AuthorizeRole(ListRoles.Administrator)]
        // DELETE api/<controller>/5
        public void Delete(int id)
        {
        }
    }
}