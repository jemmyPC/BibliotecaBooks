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
    public class BooksController : Controller
    {
        private BibliotecaEntities db = new BibliotecaEntities();

        // GET: Books
        public ActionResult Index(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                List<Book> books = db.Books.Where(u => u.ISBN.ToString().Contains(searchString) || u.Autor.Contains(searchString)
              || u.Title.Contains(searchString) || u.Edition.Contains(searchString) || u.Description.Contains(searchString)).ToList();

                return View(books);
            }
        return View(db.Books.ToList());
        }   
 
        // GET: Books/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // GET: Books/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Books/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ISBN,Autor,Title,NumPages,Edition,Description,Quantity")] Book book)
        {

            try
            {
                using (var context = new BibliotecaEntities()) { 
                    var repeatedISBN = context.Books.Where(u => u.ISBN == book.ISBN).Count();
                   
             if (repeatedISBN >= 1 )
                    {
                        MessageBox.Show("ISBN YA existe", "Error");
                    }

                    if (ModelState.IsValid)
                    {
                        db.Books.Add(book);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                }

            }
            catch (Exception) {

                MessageBox.Show("Ya tiene sus limites", "Error");
            }
            return View(book);
        }

        // GET: Books/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ISBN,Autor,Title,NumPages,Edition,Description,Quantity")] Book book)
        {
            if (ModelState.IsValid)
            {
                db.Entry(book).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(book);
        }

        // GET: Books/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Book book = db.Books.Find(id);
            if (book == null)
            {
                return HttpNotFound();
            }
            return View(book);
        }

        // POST: Books/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Book book = db.Books.Find(id);
            db.Books.Remove(book);
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
