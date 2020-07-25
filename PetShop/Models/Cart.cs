using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PetShop.Models
{
    public class Cart
    {
        public int IDPet { get; set; }
        public string PetName { get; set; }
        public string Image { get; set; }
        public decimal Price { get; set; }
        public int Quanity { get; set; }
        public double Total 
        {
            get
            {
                return (double)Price * (double)Quanity;
            }
        }
    }
}