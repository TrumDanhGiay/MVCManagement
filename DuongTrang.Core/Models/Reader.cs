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
    
    public partial class Reader
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public Reader()
        {
            this.CardReaders = new HashSet<CardReader>();
            this.Returns = new HashSet<Return>();
        }
    
        public string ReaderID { get; set; }
        public string IdUser { get; set; }
        public string Identifier { get; set; }
        public Nullable<System.Guid> JobID { get; set; }
        public Nullable<System.Guid> LevelID { get; set; }
        public Nullable<System.Guid> FacultyID { get; set; }
        public Nullable<System.Guid> TrainingSystemID { get; set; }
        public Nullable<int> YearSchool { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual AspNetUser AspNetUser { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<CardReader> CardReaders { get; set; }
        public virtual Faculty Faculty { get; set; }
        public virtual Job Job { get; set; }
        public virtual Level Level { get; set; }
        public virtual TrainingSystem TrainingSystem { get; set; }
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2227:CollectionPropertiesShouldBeReadOnly")]
        public virtual ICollection<Return> Returns { get; set; }
    }
}
