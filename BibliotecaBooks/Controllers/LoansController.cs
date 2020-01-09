using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Windows.Forms;
using Biblioteca;

namespace BibliotecaBooks.Controllers
{
    public class LoansController : Controller
    {
        private BibliotecaEntities db = new BibliotecaEntities();

        // GET: Loans
        public ActionResult Index(string searchString)
        {
            var loans = db.Loans.Include(l => l.Book).Include(l => l.Status).Include(l => l.User).Include(l => l.User1);


            return View(loans.ToList());
        }

        // GET: Loans/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // GET: Loans/Create
        public ActionResult Create()
        {
            ViewBag.IdBook = new SelectList(db.Books, "Id", "Autor");
            ViewBag.StatusId = new SelectList(db.Status, "Id", "status");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name");
            return View();
        }

        // POST: Loans/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,UserID,DateCreate,DateFinish,Quantity,StatusId,Price,IdBook,ISBN")] Loan loan)
        {


            ViewBag.IdBook = new SelectList(db.Books, "Id", "Autor", loan.IdBook);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "status", loan.StatusId);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", loan.UserID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", loan.UserID);
            try
            {
                using (var context = new BibliotecaEntities())
                {
                    var user = context.Users.Where(u => u.ID == loan.UserID).First();
                    var book = context.Books.Where(b => b.Id == loan.IdBook).First();
                    if (user.Quantity >= 3)
                    {
                        MessageBox.Show("Tiene 3 libros prestados ");
                        return View(loan);
                    }

                    if (book.Quantity <= 1)
                    {
                        MessageBox.Show("No Disponible para prestamo");
                        return View(loan);
                    }

                    if (user.IdStatus >= 4)
                    {
                        MessageBox.Show("No Disponible para prestamo");
                        return View(loan);
                    }
                    if (user.IdStatus >= 2)
                    {
                        MessageBox.Show("Tiene deudas por Pagar", "Error");
                        return View(loan);
                    }


                    loan.DateCreate = DateTime.Now;
                    loan.StatusId = 9;
                    db.Loans.Add(loan);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

            }
            catch (Exception)
            {
                return View(loan);
            }
        }

       // GET: Loans/BackBook/id
        public ActionResult BackBook(Loan loan)
        {
                        
                    loan.DateFinish = DateTime.Now;
                    loan.StatusId = 10 ;
                    db.Entry(loan).State = EntityState.Modified;
                    db.SaveChanges();
            return RedirectToAction("Index");

        }



        // GET: Loans/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            ViewBag.IdBook = new SelectList(db.Books, "Id", "Autor", loan.IdBook);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "status", loan.StatusId);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", loan.UserID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", loan.UserID);
            return View(loan);
        }

        // POST: Loans/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,UserID,DateCreate,DateFinish,Quantity,StatusId,Price,IdBook,ISBN")] Loan loan)
        {
            if (ModelState.IsValid)
            {
                db.Entry(loan).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.IdBook = new SelectList(db.Books, "Id", "Autor", loan.IdBook);
            ViewBag.StatusId = new SelectList(db.Status, "Id", "status", loan.StatusId);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", loan.UserID);
            ViewBag.UserID = new SelectList(db.Users, "ID", "Name", loan.UserID);
            return View(loan);
        }

        // GET: Loans/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Loan loan = db.Loans.Find(id);
            if (loan == null)
            {
                return HttpNotFound();
            }
            return View(loan);
        }

        // POST: Loans/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Loan loan = db.Loans.Find(id);
            db.Loans.Remove(loan);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
