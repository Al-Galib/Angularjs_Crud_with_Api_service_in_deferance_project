using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Angularjs_Eve1.ViewModels
{
    public class BookVm
    {
        public int Id { get; set; }
        [Required, StringLength(40)]
        public string BookName { get; set; }
        [Required, StringLength(40)]
        public string AuthorName { get; set; }
        [Required]
        public DateTime PublishDate { get; set; }
        
        public string Picture { get; set; }
        [StringLength(10)]
        public string ImageType { get; set; }
    }
}