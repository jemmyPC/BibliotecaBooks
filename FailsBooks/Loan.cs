//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace FailsBooks
{
    using System;
    using System.Collections.Generic;
    
    public partial class Loan
    {
        public int Id { get; set; }
        public int UserID { get; set; }
        public Nullable<System.DateTime> DateCreate { get; set; }
        public Nullable<System.DateTime> DateFinish { get; set; }
        public Nullable<int> Quantity { get; set; }
        public Nullable<int> StatusId { get; set; }
        public Nullable<int> Price { get; set; }
        public int IdBook { get; set; }
        public int ISBN { get; set; }
    
        public virtual Book Book { get; set; }
        public virtual Status Status { get; set; }
        public virtual User User { get; set; }
        public virtual User User1 { get; set; }
    }
}
