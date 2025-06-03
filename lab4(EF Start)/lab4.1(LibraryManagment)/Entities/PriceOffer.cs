using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace lab4._1_LibraryManagment_.Entities
{
    public class PriceOffer
    {
        [Key]
        public int PriceOfferId { get; set; }
        public int NewPrice { get; set; }
        public string PromotionalText { get; set; }
        public Book? Book { get; set; }

        [ForeignKey("Book")]
        public int? BookId { get; set; }
    }
}
