using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using RepositoryPattern.Interfaces;

namespace BooksLibraryUI.Controllers
{
    public class LoanController : Controller
    {
        private readonly IRepository<Loan> _repos;
        public LoanController(IRepository<Loan> repository)
        {
            _repos = repository;
        }
        // GET: Loan
        public ActionResult Index()
        {
            return View();
        }


        // GET: Loan/Create
        public IActionResult Agregar()
        {

            ViewData["Accion"] = "Agregar";
            return PartialView("Agregar", new Loan());
        }
        public IActionResult Details(int id)
        {
            ViewData["Accion"] = "Details";

            Loan loan = _repos.GetById(id);
            Loan model = new List<Loan>.Enumerator().Current;

            return PartialView("Details", loan);

        }
    }
}