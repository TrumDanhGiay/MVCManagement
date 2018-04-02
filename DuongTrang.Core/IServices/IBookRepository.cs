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
        Book GetSingle(string id);
        IQueryable<object> GetAllBook();
    }
}
