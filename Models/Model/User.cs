using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Text;


namespace Models.Model
{
    public class User
    {
        [Key]
        public int ID { get; set; }
        public string Name { get; set; }
        public string LastName { get; set; }

         [Range(18, 18)]
         [Required(ErrorMessage = "CURP IS REQUIRED 18 digits")]
         [Display]
        public string CURP { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email Address is required")]
        public string Email { get; set; }

        public string UserName { get; set; }

        [Required, DataType(DataType.Password)]
        public string Password { get; set; }
        public int? Quantity { get; set; }
        public bool? IsActive { get; set; }
        public int? IdStatus { get; set; }

        [ForeignKey("IdStatus")]
        public virtual Status Status { get; set; }


    }
}
