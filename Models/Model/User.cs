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

        [Required(ErrorMessage = "Se Requiere Nombre")]
        public string Name { get; set; }

        [Required(ErrorMessage = "Se Requiere User LastName")]
        public string LastName { get; set; }

        [StringLength (18, MinimumLength=18)]
        [Required(ErrorMessage = "CURP IS REQUIRED 18 digits")]
        [Display(Name = "CURP")]
        public string CURP { get; set; }

        [EmailAddress]
        [Required(ErrorMessage = "Email Address is required")]
        [Display(Name = "Email")]
        public string Email { get; set; }

        [Required(ErrorMessage = "Se Requiere User Name")]
        public string UserName { get; set; }


       
        [Required, DataType(DataType.Password)]
        public string Password { get; set; }



        [Range(0, 3,
             ErrorMessage = "Must be between 0 and 3 Books on Loan")]
        public int? Quantity { get; set; }
        public bool? IsActive { get; set; }
        public int? IdStatus { get; set; }

        [ForeignKey("IdStatus")]
        public virtual Status Status { get; set; }


    }
}
