using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

using Biblioteca;
using Microsoft.EntityFrameworkCore;

namespace FailService
{
  public  class BooksIntegonLibrary
    {
        
        static BibliotecaEntities db = new BibliotecaEntities();

        public static void checkDate()
        {
        //    try
        //    {

        //        var loans = db.Loans.Where(d => d.DateCreate.Value.AddDays(5) >= DateTime.Now && d.StatusId == 3);
        //        foreach (var loan in loans)
        //        {

        //            db.Entry(loan).State = System.Data.Entity.EntityState.Modified;
        //        }
        //        db.SaveChanges();

        //        Console.WriteLine("se actualizaron Loans" + loans.Count());
        //    }
        //    catch (Exception ee) { Console.WriteLine(ee); }
        }
    }

}

