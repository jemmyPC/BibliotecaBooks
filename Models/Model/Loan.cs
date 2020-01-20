using Models.Model;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models
{
   public class Loan
    {
        [Key]
        public int Id { get; set; }
        public int UserID { get; set; }
        public DateTime? DateCreate { get; set; }
        public DateTime? DateFinish { get; set; }
        public int? StatusId { get; set; }
        public int? Debt { get; set; }
        public int IdBook { get; set; }
        public bool IsActive { get; set; }

        public IEnumerable<Book> books;
        public IEnumerable<User> users;

        [ForeignKey("IdBook")]
        public virtual Book Book { get; set; }
        [ForeignKey("StatusId")]
        public virtual Status Status { get; set; }
        [ForeignKey("UserID")]
        public virtual User User { get; set; }

    }
}
