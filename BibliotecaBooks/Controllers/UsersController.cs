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
    public class UsersController : Controller
    {
        private BibliotecaEntities db = new BibliotecaEntities();



        //   GET: Users
        public ActionResult Index(string searchString)
        {

            if (!String.IsNullOrEmpty(searchString))
            {
                List<User> users = db.Users.Where(u => u.CURP.Contains(searchString) || u.Email.Contains(searchString)
              || u.LastName.Contains(searchString) || u.UserName.Contains(searchString) || u.Name.Contains(searchString)).ToList();

                return View(users);
            }

            return View(db.Users.ToList());
        }


     
        // GET: Users/Details/5
        public ActionResult Details(int? id, int? deb)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            user.Loans1 = user.Loans.Where(s => s.UserID == id && (s.StatusId == 4 || s.StatusId == 5 || s.StatusId == 8)).ToList();
            return View(user);
        }

        // GET: Users/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,Name,LastName,CURP,Email,UserName,Password,Quantity,IsActive,status")] User user)
        {
            try
            {
                using (var context = new BibliotecaEntities())
                {
                    var repeatedCURP = context.Users.Where(u => u.CURP == user.CURP).Count();
                    var repeatedUserName = context.Users.Where(u => u.UserName == user.UserName).Count();
                    if (repeatedCURP >= 1)
                    {
                        if (!ModelState.IsValid)
                        {
                            MessageBox.Show("Invalido", "Error");
                        }

                        MessageBox.Show("Curp Repetido", "Error");

                    }
                    if (repeatedUserName >= 1)
                    {
                        if (!ModelState.IsValid)
                        {
                            MessageBox.Show("INVALIDO", "Error");
                        }

                        MessageBox.Show("Username ya existe", "Error");

                    }

                    db.Users.Add(user);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            catch (System.Exception)
            {
                return View(user);
            }

        }
        // GET: Users/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(User user)
        {
            if (ModelState.IsValid)
            {
                db.Entry(user).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(user);
        }


        // GET: Users/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            User user = db.Users.Find(id);
            if (user == null)
            {
                return HttpNotFound();
            }
            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            User user = db.Users.Find(id);
            db.Users.Remove(user);
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
    public class UserDetail
        {
            
        }

}
