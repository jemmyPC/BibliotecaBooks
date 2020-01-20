using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Model;
using RepositoryPattern.Interfaces;

namespace BooksLibraryUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly IRepository<User> _reposU;
        private readonly IRepository<Book> _reposB;
        private readonly IRepository<Loan> _reposL;
        public LoanController(IRepository<Loan> repository, IRepository<User> reposU, IRepository<Book> reposB)
        {
            _reposL = repository;
            _reposU = reposU;
            _reposB = reposB;
        }


        // GET: Loan
        public ActionResult Index()
        {
            return View();
        }


        // GET: Loan/Create
        // GET: Loan/Create
        public IActionResult Agregar()
        {
            ViewData["Accion"] = "Agregar";
            Loan new_loan = new Loan();
            new_loan.users = _reposU.GetAll();
            new_loan.books = _reposB.GetAll();
            return PartialView("Agregar", new_loan);
        }
    }
}