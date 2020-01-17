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
                var loans = db.Loan.Where(d => d.DateCreate.Value.AddDays(5) >= DateTime.Now && d.DateFinish == null);
                foreach (var loan in loans)
                {
                    loan.StatusId = 4;
                    loan.Price = 10 * ((int)(loan.DateCreate.Value.AddDays(5) - DateTime.Now).TotalDays);
                    db.Loan.Add(loan);
                    db.Entry(loan).State = System.Data.Entity.EntityState.Modified;
                    db.SaveChanges();
                }
                

                Console.WriteLine("Loans updated" + loans.Count());
            }
            catch (Exception ee) { Console.WriteLine(ee); }
        }
    }

}