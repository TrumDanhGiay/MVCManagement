using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using DuongTrang.Core.IServices;
using Management.APICustomAuthorize;
using Management.Property;
using System.Threading.Tasks;
using System.Web;
using System.Diagnostics;
using Microsoft.AspNet.Identity;
using Management.Const;
using DuongTrang.Core.CustomModels;

namespace Management.Controllers.AdminController
{
    /// <summary>
    /// Sách API
    /// </summary>
    [Authorize]
    public class BookController : ApiController
    {
        /// <summary>
        /// Dependency Injection
        /// </summary>
        private readonly IBookRepository _bookRepository;
        private readonly IGetIdByName _getIdByName;
        public BookController(IBookRepository bookRepository, IGetIdByName getIdByName)
        {
            _bookRepository = bookRepository;
            _getIdByName = getIdByName;
        }
        /// <summary>
        /// Lấy danh sách sách
        /// </summary>
        /// <returns>Dánh sách sách</returns>
        // GET api/<controller>
        public IEnumerable<object> Get()
        {
            return _bookRepository.GetAllBook();
        }

        /// <summary>
        /// Lấy sách theo mã sách
        /// </summary>
        /// <param name="id">Mã sách</param>
        /// <returns>Thông tin sách và danh sách ảnh</returns>
        // GET api/<controller>/5
        public object Get(string id)
        {
            if (id != null)
            {
                if (_bookRepository.CheckImageCount(id) == Guid.Parse("C56A4180-65AA-42EC-A945-5FD21DEC0538"))
                {
                    var message1 = string.Format("Hãy lưu sách trước khi upload ảnh.");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, message1);
                }
            }
            else
            {
                var message1 = string.Format("Hãy nhập mã sách.");
                return Request.CreateResponse(HttpStatusCode.NotFound, message1);
            }
            return _bookRepository.GetSingle(id);
        }

        /// <summary>
        /// Upload ảnh cho từng sách
        /// </summary>
        /// <param name="bookcode">Mã sách</param>
        /// <returns>Kết quả lưu</returns>
        [Route("api/Book/PostBookImage/{bookcode}")]
        [HttpPost]
        [AllowAnonymous]
        public async Task<HttpResponseMessage> PostBookImage(string bookcode)
        {
            if(bookcode != null)
            {
                if (_bookRepository.CheckImageCount(bookcode) == Guid.Parse("C56A4180-65AA-42EC-A945-5FD21DEC0538"))
                {
                    var message1 = string.Format("Hãy lưu sách trước khi upload ảnh.");
                    return Request.CreateResponse(HttpStatusCode.BadRequest, message1);
                }
            }
            else
            {
                var message1 = string.Format("Hãy nhập mã sách.");
                return Request.CreateResponse(HttpStatusCode.NotFound, message1);
            }
            
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

        /// <summary>
        /// Thêm sách - Roles : Administrator
        /// </summary>
        /// <param name="bookViewModels">Thông tin sách thêm</param>
        /// <returns></returns>
        [AuthorizeRole(ListRoles.Administrator)]
        // POST api/<controller>
        public async Task<IHttpActionResult> Post([FromBody]BookViewModels bookViewModels)
        {
            _bookRepository.Insert(new DuongTrang.Core.Models.Book {
                BookID = Guid.NewGuid(),
                AddDate = DateTime.Now,
                Author = bookViewModels.Author,
                BookCode = bookViewModels.BookCode,
                BookName = bookViewModels.BookName,
                CategoryID = _getIdByName.GetCategoryID(bookViewModels.Category),
                CompanyPublishID = _getIdByName.GetCompanyID(bookViewModels.CompanyPublishName),
                Content = bookViewModels.Content,
                CreateBy = Guid.Parse(User.Identity.GetUserId()),
                Keyword = bookViewModels.Keyword,
                IsDelete = false,
                Price = bookViewModels.Price,
                LanguageID = _getIdByName.GetLanguageID(bookViewModels.Language),
                KindID = _getIdByName.GetKindID(bookViewModels.Kind),
                YearPublish = DateTime.Parse(bookViewModels.YearPublish)
            });
            await _bookRepository.SaveChangesAsync();
            return Ok(MConst.SuccessInsertAPI);
        }

        /// <summary>
        /// Sửa sách - Roles : Administrator
        /// </summary>
        /// <param name="id">ID sách sửa</param>
        /// <param name="bookViewModels">Thông tin sửa sách</param>
        /// <returns></returns>
        [AuthorizeRole(ListRoles.Administrator)]
        // PUT api/<controller>/5
        public async Task<IHttpActionResult> Put(string id, [FromBody]BookViewModels bookViewModels)
        {
            if(id != null)
            {
                _bookRepository.Update(new DuongTrang.Core.Models.Book
                {
                    BookID = _bookRepository.GetBookIdByBookCode(bookViewModels.BookCode),
                    AddDate = DateTime.Now,
                    Author = bookViewModels.Author,
                    BookCode = bookViewModels.BookCode,
                    BookName = bookViewModels.BookName,
                    CategoryID = _getIdByName.GetCategoryID(bookViewModels.Category),
                    CompanyPublishID = _getIdByName.GetCompanyID(bookViewModels.CompanyPublishName),
                    Content = bookViewModels.Content,
                    CreateBy = Guid.Parse(User.Identity.GetUserId()),
                    Keyword = bookViewModels.Keyword,
                    IsDelete = false,
                    Price = bookViewModels.Price,
                    LanguageID = _getIdByName.GetLanguageID(bookViewModels.Language),
                    KindID = _getIdByName.GetKindID(bookViewModels.Kind),
                    YearPublish = DateTime.Parse(bookViewModels.YearPublish)
                });
                try
                {
                    await _bookRepository.SaveChangesAsync();
                    return Ok(MConst.SuccessEditAPI);
                }
                catch (Exception e)
                {
                    return BadRequest(MConst.ErrorAPI + e.Message);
                }
            }
            else
            {
                return BadRequest(MConst.NullID);
            }
        }

        /// <summary>
        /// Xóa sách - Roles : Administrator
        /// </summary>
        /// <param name="id">Mã sách</param>
        /// <returns></returns>
        [AuthorizeRole(ListRoles.Administrator)]
        // DELETE api/<controller>/5
        public async Task<IHttpActionResult> Delete(string id)
        {
            if (id != null)
            {
                _bookRepository.DeleteBook(id);
                try
                {
                    await _bookRepository.SaveChangesAsync();
                    return Ok(MConst.SuccessDeleteAPI);
                }
                catch (Exception e)
                {
                    return BadRequest(MConst.ErrorAPI + e.Message);
                }
            }
            else
            {
                return BadRequest(MConst.NullID);
            }
        }
    }
}