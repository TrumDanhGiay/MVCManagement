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
    
    public partial class ChildMenu
    {
        public System.Guid ID { get; set; }
        public System.Guid MenuID { get; set; }
        public string ChildMenuName { get; set; }
        public string LinkSPA { get; set; }
        public string ChildMenuIcon { get; set; }
        public Nullable<bool> IsDelete { get; set; }
    
        public virtual Menu Menu { get; set; }
    }
}
