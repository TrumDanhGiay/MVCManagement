using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuongTrang.Core.CustomModels
{
    public class BorrowViewModels
    {
        public System.Guid BorrowID { get; set; }
        public Nullable<System.Guid> CardReaderID { get; set; }
        public Nullable<System.DateTime> DateBorrow { get; set; }
        public Nullable<int> AmountTotal { get; set; }
        public Nullable<System.Guid> CreateBy { get; set; }
        public Nullable<System.DateTime> CreateDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
        public Nullable<int> PendingStatusID { get; set; }
        public string BorrowCode { get; set; }
    }
}
