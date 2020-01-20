using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Model
{
   public class Book
    {
        [Key]
        public int Id { get; set; }

        [RegularExpression("([0-9]{13})", ErrorMessage = "ISBN should have 13 digits")]
        public long ISBN { get; set; }

        [Required(ErrorMessage = "Autor is requiered")]
        public string Autor { get; set; }

        [Required(ErrorMessage = "Title is requiered")]
        public string Title { get; set; }

        public int? NumPages { get; set; }
        [Required(ErrorMessage = "Edition is requiered")]
        public string Edition { get; set; }

   
        public string Description { get; set; }

        [Range(1, 10,
           ErrorMessage = "Quantity must be between 1 and 10")]
        public int? Quantity { get; set; }

        public bool? IsActive { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
