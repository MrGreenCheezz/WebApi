using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace ApiApp
{
    public class DataBaseItemClass
    {
        public DataBaseItemClass()
        {

        }
        public DataBaseItemClass(string inId, string inItemName, int inCurrentCount, int inPrice , string inPicLink, string inItemType)
        {
            Id = inId;
            ItemName = inItemName;
            CurrentCount = inCurrentCount;
            Price = inPrice;
            PicLink = inPicLink;
            ItemType = inItemType;
        }

        public string Id { get; set; }
        public string ItemName {get; set;}
        public int CurrentCount { get; set; }
        public int Price { get; set; }
        public string PicLink { get; set; }
        public string ItemType { get; set; }
    }
}
