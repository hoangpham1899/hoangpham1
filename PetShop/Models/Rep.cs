//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace PetShop.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class Rep
    {
        public int IDRep { get; set; }
        public Nullable<int> IDComments { get; set; }
        public Nullable<int> IDUser { get; set; }
        public string Reply { get; set; }
        public Nullable<System.DateTime> DatePost { get; set; }
    
        public virtual Comment Comment { get; set; }
        public virtual User User { get; set; }
    }
}
