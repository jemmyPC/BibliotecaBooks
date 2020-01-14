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
        public int ISBN { get; set; }
        public string Autor { get; set; }
        public string Title { get; set; }
        public int? NumPages { get; set; }
        public string Edition { get; set; }
        public string Description { get; set; }
        public int? Quantity { get; set; }

        public bool? IsActive { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }
    }
}
