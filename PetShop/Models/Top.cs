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
    
    public partial class Top
    {
        public int IDTop { get; set; }
        public Nullable<int> IDPet { get; set; }
        public Nullable<int> Quanity { get; set; }
    
        public virtual Pet Pet { get; set; }
    }
}
