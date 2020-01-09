using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;

namespace Models.Model
{
    public class User
    {   [Key]

        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }
        public string CURP { get; set; }
        public string Email { get; set; }
        public string UserName { get; set; }
        public string Password { get; set; }
        public int? Quantity { get; set; }
        public bool? IsActive { get; set; }

        
        public int? IdStatus { get; set; }
        public virtual ICollection<Loan> Loans { get; set; }

        [ForeignKey("IdStatus")]
        public virtual Status Status { get; set; }

    }
}
