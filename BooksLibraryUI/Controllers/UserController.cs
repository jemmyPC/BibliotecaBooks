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

        private readonly IRepository<User> _repos;
        private readonly IRepository<Loan> _reposL;

        public UserController(IRepository<User> repository, IRepository<Loan> reposL)
        {
            _repos = repository;
            _reposL = reposL;
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
            User user = _repos.GetById(id);
            User model = new List<User>.Enumerator().Current;
            return PartialView("Edith", user);

        }
        public IActionResult Details(int id)
        {
            ViewData["Accion"] = "Details";
            User user = _repos.GetById(id);
            User model = new List<User>.Enumerator().Current;
            return PartialView("Details", user);
        }
    }
}
