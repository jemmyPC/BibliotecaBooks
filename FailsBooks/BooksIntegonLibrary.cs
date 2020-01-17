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
                var loans = db.Loan.Where(d => d.DateFinish == null).ToList();
                foreach (var loan in loans)
                {
                    int diff = (DateTime.Now - loan.DateCreate).Value.Days;
                    if(diff > 5)
                    {
                        loan.StatusId = 4;
                        loan.Price = 10 * diff;
                        db.Loan.Add(loan);
                        db.Entry(loan).State = System.Data.Entity.EntityState.Modified;
                        db.SaveChanges();
                    }

                }
                

                Console.WriteLine("Loans updated" + loans.Count());
            }
            catch (Exception ee) { Console.WriteLine(ee); }
        }
    }

}