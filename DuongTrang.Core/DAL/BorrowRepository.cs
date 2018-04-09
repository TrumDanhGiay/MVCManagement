using DuongTrang.Core.IServices;
using DuongTrang.Core.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.DAL
{
    public class BorrowRepository : BaseRepository<DATNEntities, Borrow>, IBorrowRepository
    {
        public IQueryable<object> GetAllBorrow()
        {
            var listborrow = Context.Borrows
                .Join(Context.AspNetUsers, a => a.CreateBy.ToString(), b => b.Id, (a, b) => new { Borrow = a, AspNetUser = b })
                .Join(Context.PendingStatus, a => a.Borrow.PendingStatusID, c => c.PendingID, (a, c) => new { a.Borrow, a.AspNetUser, PendingStatu = c})
                .Where(a => a.Borrow.IsDelete == false)
                .Select(u => new
                {
                    u.Borrow.BorrowCode,
                    u.AspNetUser.FullName,
                    u.Borrow.DateBorrow,
                    u.Borrow.AmountTotal,
                    u.PendingStatu.PendingStatusName
                });
            return listborrow;
        }

        public object GetSingle(string borrowCode)
        {
            var borrowId = Context.Borrows.FirstOrDefault(x => x.BorrowCode.Trim().Equals(borrowCode.Trim()));
            var borrow = Context.BorrowDetails
                .Join(Context.Books, a => a.BookID, b => b.BookID, (a, b) => new { BorrowDetail = a, Book = b })
                .Where(x => x.BorrowDetail.BorrowID == borrowId.BorrowID)
                .Select(u => new
                {
                    u.Book.BookName,
                    u.BorrowDetail.DateExpried
                });
            return borrow;
        }
    }
}
