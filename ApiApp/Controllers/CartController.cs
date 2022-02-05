using Microsoft.AspNetCore.Cors;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text.Json;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
    
    [Route("[controller]")]
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class CartController : ControllerBase
    {
        string LastUsedItemType;
        List<DataBaseItemClass> CachedItems = new List<DataBaseItemClass>();
        List<DataBaseItemClass> returnItems = new List<DataBaseItemClass>();
        List<DataBaseNewsItemClass> returnNews = new List<DataBaseNewsItemClass>();
        [HttpGet]
        public string Get()
        {
          using(InfoContext db = new InfoContext())
            {
                foreach(var item in db.ItemsDataBase)
                {
                    returnItems.Add(item);
                }
            }

            return JsonSerializer.Serialize(returnItems);
        }
        [Route("/advget")]
        [HttpGet]
        public string AdvancedGet(int SegmentIndex, int SegmentSize, string ItemType )
        {
            if (String.IsNullOrEmpty(ItemType))
            {
                using (InfoContext db = new InfoContext())
                {

                    foreach (var element in db.ItemsDataBase.Skip(SegmentIndex * SegmentSize).Take(SegmentSize))
                    {
                        returnItems.Add(element);
                    }
                }
            }
            else
            {
                using (InfoContext db = new InfoContext())
                {
                    if (LastUsedItemType != ItemType)
                    {
                        LastUsedItemType = ItemType;
                        CachedItems.Clear();
                    }
                    var queryResult = from i in db.ItemsDataBase
                                      where i.ItemType == LastUsedItemType
                                      select i;
                    CachedItems = queryResult.ToList();
                    foreach (var element in CachedItems.Skip(SegmentIndex * SegmentSize).Take(SegmentSize))
                    {
                        returnItems.Add(element);
                    }
                }
            }

            return JsonSerializer.Serialize(returnItems);
        }
        [Route("/advGetNews")]
        [HttpGet]
        public string AdvancedGetNews(int SegmentIndex, int SegmentSize)
        {
               using (InfoContext db = new InfoContext())
                {

                    foreach (var element in db.NewsDataBase.Skip(SegmentIndex * SegmentSize).Take(SegmentSize))
                    {
                        returnNews.Add(element);
                    }
                }
            return JsonSerializer.Serialize(returnNews);
        }
        [Route("/getNewscount")]
        [HttpGet]
        public string GetNewsCount()
        {
            using (InfoContext db = new InfoContext())
            {

                return JsonSerializer.Serialize(db.NewsDataBase.Count());
            }


        }


        [Route("/getdbcount")]
        [HttpGet]
        public string GetCount()
        {
            using (InfoContext db = new InfoContext())
            {

                return JsonSerializer.Serialize(db.ItemsDataBase.Count()); 
            }

            
        }
        [Route("/TestReq")]
        [HttpPost]
        public async void TestRequest(string input)
        {
            var reader = new StreamReader(HttpContext.Request.Body);
            var body = reader.ReadToEndAsync();
            System.Diagnostics.Debug.WriteLine(await body);
        }
        //[httpget]
        //public list<cartitems> get()
        //{
        //    for (int i = 0; i < 10; i++)
        //    {
        //        var tempitem = new cartitems();
        //        tempitem.amount = rand.next(1, 20);
        //        tempitem.price = rand.next(500, 2000);
        //        tempitem.itemname = randomnames[rand.next(4)];
        //        returnitems.add(tempitem);
        //    }
        //    return returnitems;
        //}
    }
}
