using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;

namespace DuongTrang.Core.DAL
{
    public class BookRepository : BaseRepository<DATNEntities, Book>, IBookRepository
    {
        /// <summary>
        /// Lấy sách theo ID
        /// </summary>
        /// <param name="id">ID Sách</param>
        /// <returns>Sách</returns>
        public Book GetSingle(string id)
        {
            return Context.Books.FirstOrDefault(x => x.BookName == id);
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
                    a.Book.Image
                });
            return listBook; 
        }
    }
}
