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
    
    public partial class OrderDetail
    {
        public int IDOrderDetails { get; set; }
        public Nullable<int> IDOrder { get; set; }
        public Nullable<int> IDPet { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<decimal> TotalMoney { get; set; }
    
        public virtual Order Order { get; set; }
        public virtual Pet Pet { get; set; }
    }
}
