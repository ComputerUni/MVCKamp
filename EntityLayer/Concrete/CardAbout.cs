using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EntityLayer.Concrete
{
    public class CardAbout
    {
        [Key]
        public int CardAboutID { get; set; }
        public string CardAboutName { get; set; }
        public string CardAboutTitle { get; set; }
        public string CardImageUrl { get; set; }
    }
}
