using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp
{
    public class CartItems
    {
        public string ItemName{get;set;}

        public int Amount { get; set; }

        public int Price { get; set; }

        public string ItemType { get; set; }
    }
}
