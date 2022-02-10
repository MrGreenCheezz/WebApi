using Microsoft.AspNetCore.Mvc;
using System;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Cors;

namespace ApiApp.Controllers
{
    
    [ApiController]
    [EnableCors("AllowOrigin")]
    public class EditInfoBaseController : ControllerBase
    {
        [Route("/Products/addProduct")]
        [HttpPost]
        public void AddNewElement(IFormCollection form)
        {
            DataBaseItemClass newItem = new DataBaseItemClass();
            newItem.ItemName = form["ItemName"];
            newItem.CurrentCount = Int32.Parse(form["Count"]);
            newItem.Price = Int32.Parse(form["Price"]);
            newItem.ItemType = form["ItemType"];
            try
            {              
                using (var fileStream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/" + "Images" + "/" + form.Files[0].FileName))
                {
                    form.Files[0].CopyTo(fileStream);
                }
                newItem.PicLink = "http://mrgreencheezz.ddns.net:90/PictureApi/getPic?PicName=" + form.Files[0].FileName;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("No file!");
            }

            using (InfoContext db = new InfoContext())
            {
                newItem.Id = (db.ItemsDataBase.Count() + 1).ToString();
                db.ItemsDataBase.Add(newItem);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                   System.Diagnostics.Debug.WriteLine(ex.InnerException);
                }
            }
        }
        [Route("/News/AddItem")]
        [HttpPost]
        public void AddNewsItem(IFormCollection form)
        {
            DataBaseNewsItemClass newItem = new DataBaseNewsItemClass();
            System.Diagnostics.Debug.WriteLine(form["Content"]);
            newItem.NewsContent = form["Content"];
            newItem.NewsHeader = form["NewsHeader"];
            try
            {
                using (var fileStream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/" + "Images" + "/" + form.Files[0].FileName))
                {
                    form.Files[0].CopyTo(fileStream);
                }
                newItem.NewsPictureLink = "http://mrgreencheezz.ddns.net:90/PictureApi/getPic?PicName=" + form.Files[0].FileName;
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("No file!");
            }
            using (InfoContext db = new InfoContext())
            {
                newItem.Id = (db.NewsDataBase.Count() + 1).ToString();
                db.NewsDataBase.Add(newItem);
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.InnerException);
                }
            }

        }

       [Route("/Pictures/FetchPicture")]
       [HttpGet]
        public ActionResult GetPicture(string PicName)
        {
            return base.PhysicalFile(Directory.GetCurrentDirectory() + "/" + "Images"+"/"+PicName, "image/jpeg");
        }
        [Route("/Pictures/AddPicture")]
        [HttpPost]
        public void AddPic(IFormFile fileToUpload)
        {
            IFormFile file;
            try
            {
                file = HttpContext.Request.Form.Files[0];
                using (var fileStream = System.IO.File.Create(Directory.GetCurrentDirectory() + "/" + "Images" + "/" + file.FileName))
                {
                    file.CopyTo(fileStream);
                }
            }
            catch
            {
                System.Diagnostics.Debug.WriteLine("No file!");
            }
           
        }
        [Route("/Products/EditElement")]
        [HttpPost]
        public void EditElement(DataBaseItemClass newItem, string itemID)
        {
            using (InfoContext db = new InfoContext())
            {
                var item = db.ItemsDataBase.Find(itemID);
                item.ItemName = newItem.ItemName;
                item.CurrentCount = newItem.CurrentCount;
                item.Price = newItem.Price;
                item.ItemType = newItem.ItemType;
                try
                {
                    db.SaveChanges();
                }
                catch (Exception ex)
                {
                    System.Diagnostics.Debug.WriteLine(ex.Message);
                }
            }
        }
        

    }
}
