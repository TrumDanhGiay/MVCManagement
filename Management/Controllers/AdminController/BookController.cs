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
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;

namespace Management.Controllers.AdminController
{
    [Authorize]
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

        [Route("api/Book/PostBookImage/{bookcode}")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostBookImage(string bookcode)
        {
            Dictionary<string, object> dict = new Dictionary<string, object>();
            try
            {

                var httpRequest = HttpContext.Current.Request;

                foreach (string file in httpRequest.Files)
                {
                    HttpResponseMessage response = Request.CreateResponse(HttpStatusCode.Created);

                    var postedFile = httpRequest.Files[file];
                    var extension = "";
                    if (postedFile != null && postedFile.ContentLength > 0)
                    {

                        int MaxContentLength = 1024 * 1024 * 1; //Size = 1 MB  

                        IList<string> AllowedFileExtensions = new List<string> { ".jpg", ".gif", ".png" };
                        var ext = postedFile.FileName.Substring(postedFile.FileName.LastIndexOf('.'));
                        extension = ext.ToLower();
                        if (!AllowedFileExtensions.Contains(extension))
                        {

                            var message = string.Format("Hãy tải lên ảnh có định dạng .jpg,.gif,.png.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else if (postedFile.ContentLength > MaxContentLength)
                        {

                            var message = string.Format("Hãy tải lên ảnh nhỏ hơn 1MB.");

                            dict.Add("error", message);
                            return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
                        }
                        else
                        {

                            var filePath = HttpContext.Current.Server.MapPath("~/BookImage/" + postedFile.FileName + extension);

                            postedFile.SaveAs(filePath);

                        }
                    }
                    if (bookcode != null)
                    {
                        if(_bookRepository.CheckImageCount(bookcode) != new Guid())
                        {
                            var message1 = string.Format("Thành công.");
                            await _bookRepository.SaveBookImageAsync(bookcode, postedFile.FileName + extension, postedFile.ContentLength);
                            return Request.CreateErrorResponse(HttpStatusCode.Created, message1);
                        }
                        else
                        {
                            var message1 = string.Format("Chỉ được thêm 3 ảnh, số ảnh đã tối đa.");
                            return Request.CreateResponse(HttpStatusCode.NotAcceptable, message1);
                        }
                    }
                    else
                    {
                        return Request.CreateResponse(HttpStatusCode.BadRequest, "Không có mã sách"); 
                    };
                }
                var res = string.Format("Hãy chọn ảnh một ảnh.");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.NotFound, dict);
            }
            catch (Exception ex)
            {
                var res = string.Format("Thất bại");
                dict.Add("error", res);
                return Request.CreateResponse(HttpStatusCode.BadRequest, dict);
            }
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