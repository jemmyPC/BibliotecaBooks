using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;


using Microsoft.EntityFrameworkCore;
using FailsBooks;
using System.Data.Entity.Core.Objects;

namespace DebsExec
{
  public  class BooksIntegonLibrary
    {
        
        static BibliotecaEntities db = new BibliotecaEntities();

        public static void checkDate()
        {
            try
            {

                var loans = db.Loan.Where(d => d.StatusId == 3).ToList().Where(l => l.DateCreate.Value.AddDays(5) >= DateTime.Now);

                foreach (var loan in loans)
                {

                   db.Entry(loan).State = System.Data.Entity.EntityState.Modified;
                }
                db.SaveChanges();

                Console.WriteLine("se actualizaron Loans" + loans.Count());
            }
            catch (Exception ee) {
                Console.WriteLine(ee); }
        }
    }

}

