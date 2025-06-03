using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4._1_LibraryManagment_.Entities
{
    public class Review
    {
        [Key] 
        public int ReviewId { get; set; }
        public string Voter {  get; set; }
        public int NumStars { get; set; }
        public string Comment { get; set; }
        public Book Book { get; set; }

        [ForeignKey("Book")]
        public int BookId { get; set; }
    }
}
