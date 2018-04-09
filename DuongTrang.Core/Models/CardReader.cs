//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DuongTrang.Core.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CardReader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public CardReader()
        {
            this.Borrows = new HashSet<Borrow>();
        }
    
        public System.Guid CardReaderID { get; set; }
        public string ReaderID { get; set; }
        public Nullable<System.DateTime> DateStart { get; set; }
        public Nullable<System.DateTime> DateExpried { get; set; }
        public System.Guid CreateBy { get; set; }
        public System.DateTime CreateDate { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Borrow> Borrows { get; set; }
        public virtual Reader Reader { get; set; }
    }
}
