using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models;
using RepositoryPattern.Interfaces;


namespace BooksLibraryUI.Controllers
{
    public class UserController : Controller
    {

        private readonly IRepository<User> _reposUser;
        private readonly IRepository<Loan> _reposLoan;
        private readonly IRepository<Book> _reposBook;
        private readonly IRepository<Status> _reposStatus;
        public UserController(IRepository<User> reposUser, IRepository<Loan> reposLoan, IRepository<Book> reposBook, IRepository<Status> reposStatus)
        {
            _reposUser = reposUser;
            _reposLoan = reposLoan;
            _reposBook = reposBook;
            _reposStatus = reposStatus;
        }

        // GET: User
        public ActionResult Index()
        {
            return View();
        }

        // GET: User/Create
        public IActionResult Agregar()
        {
            ViewData["Accion"] = "Agregar";
            return PartialView("Agregar", new User());
        }

        // GET: User/Edit/5
        public IActionResult Edith(int id)
        {
            ViewData["Accion"] = "Edith";
            User user = _reposUser.GetById(id);
            User model = new List<User>.Enumerator().Current;
            return PartialView("Edith", user);
        }

        public IActionResult Details(int id)
        {
            ViewData["Accion"] = "Details";
            User user = _reposUser.GetById(id);
            User model = new List<User>.Enumerator().Current;
            return PartialView("Details", user);
        }

        public IActionResult Debts(int id)
        {
            return View();
        }


        public IActionResult ActiveLoans(int id)
        {
            return View();

        }
    }
}
