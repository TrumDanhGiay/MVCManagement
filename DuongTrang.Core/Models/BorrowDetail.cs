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
    
    public partial class BorrowDetail
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public BorrowDetail()
        {
            this.Infringes = new HashSet<Infringe>();
        }
    
        public System.Guid BorrowDetailID { get; set; }
        public System.Guid BorrowID { get; set; }
        public System.Guid BookID { get; set; }
        public System.DateTime DateExpried { get; set; }
        public System.DateTime CreateDate { get; set; }
        public System.Guid CreateBy { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual Borrow Borrow { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Infringe> Infringes { get; set; }
    }
}
