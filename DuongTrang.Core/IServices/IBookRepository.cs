using DuongTrang.Core.CustomModels;
using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.IServices
{
    public interface IBookRepository : IBaseRepository<Book>
    {
        object GetSingle(string bookcode);
        IQueryable<object> GetAllBook();
        Task<int> SaveBookImageAsync(string bookcode, string url, int size);
        Guid CheckImageCount(string bookcode);
        void DeleteBook(string id);
        Guid GetBookIdByBookCode(string bookcode);
    }
}
