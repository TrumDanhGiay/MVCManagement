using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.IServices
{
    public interface IBorrowRepository : IBaseRepository<Borrow>
    {
        IQueryable<object> GetAllBorrow();
        object GetBorrowInfomation(string borrowCode);
        bool UpdatePendingStatus(string borrowCode, int PendingStatus, Guid modifyby);
        Borrow GetBorrow(string borrowCode);
    }
}
