using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuongTrang.Core.CustomModels;
using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;

namespace DuongTrang.Core.DAL
{
    public class BookRepository : BaseRepository<DATNEntities, Book>, IBookRepository
    {

        /// <summary>
        /// Lấy sách theo mã sách
        /// </summary>
        /// <param name="code">Mã sách</param>
        /// <returns>Thông tin sách được lấy</returns>
        public object GetSingle(string code)
        {
            ArrayList book = new ArrayList();
            var bookinfo = Context.Books
               .Join(Context.Companies, a => a.CompanyPublishID, b => b.CompanyID, (a, b) => new { Book = a, Company = b })
               .Join(Context.Kinds, a => a.Book.KindID, c => c.KindID, (a, c) => new { a.Book, a.Company, Kind = c })
               .Join(Context.Languages, a => a.Book.LanguageID, d => d.LanguageID, (a, d) => new { a.Book, a.Company, a.Kind, Language = d })
               .Join(Context.Categories, a => a.Book.CategoryID, e => e.CategoryID, (a, e) => new { a.Book, a.Company, a.Kind, a.Language, Category = e })
               .Join(Context.Images, a => a.Book.BookID, f => f.BookID, (a, f) => new { a.Book, a.Company, a.Kind, a.Language, a.Category, Image = f }).DefaultIfEmpty()
               .Where(a => a.Book.IsDelete == false && a.Book.BookCode.Trim().Equals(code.Trim()))
               .Select(a => new
               {
                   a.Book.BookID,
                   a.Book.BookCode,
                   a.Book.BookName,
                   a.Book.Author,
                   a.Company.CompanyName,
                   a.Book.YearPublish,
                   a.Kind.Kind1,
                   a.Language.Language1,
                   a.Category.CategoryName,
                   a.Book.BookFormat,
                   a.Book.Price,
                   a.Book.Content,
                   a.Book.AddDate,
                   a.Book.Keyword
               }).FirstOrDefault();
            var bookimage = Context.Images.Where(x => x.BookID == bookinfo.BookID).Select(u => new
            {
                u.Image1,
                u.Size
            });
            book.Add(bookinfo);
            book.Add(bookimage);
            return book;
        }

        /// <summary>
        /// Lấy danh sách sách 
        /// </summary>
        /// <returns>IEnumrable</returns>
        public IQueryable<object> GetAllBook()
        {
            //Join LINQ Method
            var listBook = Context.Books
                .Join(Context.Companies, a => a.CompanyPublishID, b => b.CompanyID, (a, b) => new { Book = a, Company = b })
                .Join(Context.Kinds, a => a.Book.KindID, c => c.KindID, (a, c) => new { a.Book, a.Company, Kind = c })
                .Join(Context.Languages, a => a.Book.LanguageID, d => d.LanguageID, (a, d) => new { a.Book, a.Company, a.Kind, Language = d })
                .Join(Context.Categories, a => a.Book.CategoryID, e => e.CategoryID, (a, e) => new { a.Book, a.Company, a.Kind, a.Language, Category = e })
                .Join(Context.Images, a => a.Book.BookID, f => f.BookID, (a, f) => new { a.Book, a.Company, a.Kind, a.Language, a.Category, Image = f }).DefaultIfEmpty()
                .Where(a => a.Book.IsDelete == false)
                .Select(a => new
                {
                    a.Book.BookCode,
                    a.Book.BookName,
                    a.Book.Author,
                    a.Company.CompanyName,
                    a.Book.YearPublish,
                    a.Kind.Kind1,
                    a.Language.Language1,
                    a.Category.CategoryName,
                    a.Book.BookFormat,
                    a.Book.Price,
                    a.Book.Content,
                    a.Book.AddDate,
                    a.Book.Keyword,
                    Image = "/BookImage/"+ a.Image.Image1
                }).Take(1);
            return listBook;
        }


        public Task<int> SaveBookImageAsync(string bookcode, string url, int size)
        {

            var bookid = Context.Books.Where(x => x.BookCode.Trim().Equals(bookcode.Trim())).Select(u => u.BookID).FirstOrDefault();
            var list = Context.Images.Count(x => x.BookID == bookid);
            Context.Images.Add(new Image
            {
                ID = Guid.NewGuid(),
                BookID = bookid,
                Image1 = url,
                Size = size
            });
            return Context.SaveChangesAsync();
        }

        public Guid CheckImageCount(string bookcode)
        {
            var bookid = Context.Books.Where(x => x.BookCode.Trim().Equals(bookcode.Trim())).Select(u => u.BookID).FirstOrDefault();
            if (Context.Images.Count(x => x.BookID == bookid) < 3)
            {
                return bookid;
            }
            else
            {
                return new Guid();
            }
        }
    }
}
