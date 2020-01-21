using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Models.Model
{
   public class Status
    {
        [Key]
        public int Id { get; set; }

        public string status { get; set; }
    }
}
