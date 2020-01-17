using Microsoft.EntityFrameworkCore;
using Models;
using Models.Model;
using System;
using System.Collections.Generic;
using System.Text;

namespace ContextDB
{
  public  class AppDbContext : DbContext
    {
      

        public AppDbContext(DbContextOptions options): base(options) { }


        public  DbSet<Book> Book { get; set; }
        public  DbSet<Loan> Loan { get; set; }
        public  DbSet<Status> Status { get; set; }
        public  DbSet<User> User { get; set; }




    }
}
