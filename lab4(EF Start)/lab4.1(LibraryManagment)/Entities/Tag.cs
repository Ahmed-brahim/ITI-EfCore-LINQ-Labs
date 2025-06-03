using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4._1_LibraryManagment_.Entities
{
    public class Tag
    {
        [Key]
        public int TagId { get; set; }
        public List<Book> Books { get; set; }
    }
}
