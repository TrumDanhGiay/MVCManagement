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
        /// <summary>
        /// Lấy danh sách mượn
        /// </summary>
        /// <returns>Danh sách mượn</returns>
        public IQueryable<object> GetAllBorrow()
        {
            var listborrow = Context.Borrows
                .Join(Context.AspNetUsers, a => a.CreateBy.ToString(), b => b.Id, (a, b) => new { Borrow = a, AspNetUser = b })
                .Join(Context.PendingStatus, a => a.Borrow.PendingStatusID, c => c.PendingID, (a, c) => new { a.Borrow, a.AspNetUser, PendingStatu = c})
                .Join(Context.BorrowDetails, a => a.Borrow.BorrowID, d => d.BorrowID, (a, d) => new { a.Borrow, a.AspNetUser, a.PendingStatu, BorrowDetail = d})
                .Where(a => a.Borrow.IsDelete == false)
                .Select(u => new
                {
                    u.Borrow.BorrowCode,
                    u.Borrow.CardReaderID,
                    u.AspNetUser.FullName,
                    u.Borrow.DateBorrow,
                    u.Borrow.AmountTotal,
                    u.PendingStatu.PendingStatusName
                }).Take(1);
            return listborrow;
        }

        /// <summary>
        /// Lấy yêu cầu mượn và danh sách sách mượn theo mã yêu cầu
        /// </summary>
        /// <param name="borrowCode">mã yêu cầu</param>
        /// <returns></returns>
        public object GetBorrowInfomation(string borrowCode)
        {
            var borrowId = Context.Borrows.FirstOrDefault(x => x.BorrowCode.Trim().Equals(borrowCode.Trim()));
            if (borrowId.PendingStatusID == 1 || borrowId.PendingStatusID == 3 || borrowId.PendingStatusID == 5)
            {
                var borrow = Context.BorrowDetails
                .Join(Context.Books, a => a.BookID, b => b.BookID, (a, b) => new { BorrowDetail = a, Book = b })
                .Where(x => x.BorrowDetail.BorrowID == borrowId.BorrowID)
                .Select(u => new
                {
                    u.BorrowDetail.Borrow.BorrowCode,
                    u.Book.BookCode,
                    u.Book.BookName,
                    u.Book.Author,
                    borrowId.PendingStatu.PendingStatusName,
                    borrowId.TimeToGet,
                    borrowId.DateBorrow,
                    borrowId.DateExpried
                });
                return borrow;
            }
            else
            {
                return borrowId.PendingStatusID;
            }
        }

        /// <summary>
        /// Lấy yêu cầu mượn dạng entity
        /// </summary>
        /// <param name="borrowCode">Mã yêu cầu</param>
        /// <returns></returns>
        public Borrow GetBorrow(string borrowCode)
        {
            var borrow = Context.Borrows.FirstOrDefault(x => x.BorrowCode.Trim().Equals(borrowCode.Trim()));
            return borrow;
        }

        /// <summary>
        /// Cập nhật trạng thái mượn
        /// </summary>
        /// <param name="borrowCode">Mã yêu cầu mượn</param>
        /// <param name="PendingStatus">Mã trạng thái mượn</param>
        /// <returns></returns>
        public bool UpdatePendingStatus(string borrowCode, int PendingStatus, Guid modifyby)
        {
            var borrowTemp = Context.Borrows.FirstOrDefault(x => x.BorrowCode.Trim().Equals(borrowCode.Trim()));
            if (borrowTemp != null)
            {
                Context.BorrowLogs.Add(new BorrowLog
                {
                    BorrowID = borrowTemp.BorrowID,
                    BorrowCode = borrowTemp.BorrowCode,
                    CardReaderID = borrowTemp.CardReaderID,
                    DateBorrow = borrowTemp.DateBorrow,
                    DateExpried = borrowTemp.DateExpried,
                    AmountTotal = borrowTemp.AmountTotal,
                    AcceptBy = borrowTemp.AcceptBy,
                    TimeToGet = borrowTemp.TimeToGet,
                    CreateBy = borrowTemp.CreateBy,
                    CreateDate = borrowTemp.CreateDate,
                    ModifyBy = modifyby,
                    ModifyDate = DateTime.Now,
                    PendingStatusID = borrowTemp.PendingStatusID,
                    IsDelete = borrowTemp.IsDelete
                });
                borrowTemp.PendingStatusID = PendingStatus;
                return true;
            }
            else return false;
        }
    }
}
