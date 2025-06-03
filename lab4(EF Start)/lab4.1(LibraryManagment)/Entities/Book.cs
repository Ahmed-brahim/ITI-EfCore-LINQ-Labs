using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4._1_LibraryManagment_.Entities
{
    public class Book
    {
        [Key]
        public int BookId { get; set; }
        public string Title { get; set; }
        public DateTime PublishedOn { get; set; }
        public string Publisher { get; set; }
        public int Price { get; set; }
        public string ImageUrl { get; set; }
        public List<BookAuthor> bookAuthors { get; set; }
        public List<Tag> tags { get; set; }
        public List<Review>? Reviews { get; set; }
        public PriceOffer? PriceOffer { get; set; }
        [ForeignKey("PriceOffer")]
        public int? PriceOfferId { get; set; }
    }
}
