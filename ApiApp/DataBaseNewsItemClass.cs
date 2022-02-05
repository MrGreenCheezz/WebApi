using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ApiApp
{
   
    public class DataBaseNewsItemClass
    {
       public DataBaseNewsItemClass()
        {

        }

        public DataBaseNewsItemClass(string inputHeader, string inputContent, string inputLink, string inId)
        {
            NewsHeader = inputHeader;
            NewsContent = inputContent;
            NewsPictureLink = inputLink;
            Id = inId;
        }
        public string Id { get; set; }
        public string NewsHeader { get; set; }
        [Column(TypeName="text")]
        public string NewsContent { get; set; }
        public string NewsPictureLink { get; set; }
    }
}
