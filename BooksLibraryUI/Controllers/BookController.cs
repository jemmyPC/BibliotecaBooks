using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using RepositoryPattern.Interfaces;

namespace BooksLibraryUI.Controllers
{
    public class BookController : Controller
    {
        private readonly IRepository<Book> _repos;
        
        public BookController(IRepository<Book> repository)
        {
            _repos = repository;
        }
        // GET: Book
        public ActionResult Index()
        {

            return View();
        }
        // GET: Book/Create
        public IActionResult Agregar()
        {

            ViewData["Accion"] = "Agregar";
            return PartialView("Agregar", new Book());
        }
        public IActionResult Edith(int id)
        {

            ViewData["Accion"] = "Edith";
            return PartialView("Edith", new Book());
        }
        public IActionResult Details(int id)
        {
            ViewData["Accion"] = "Details";

            Book book = _repos.GetById(id);
            Book model = new List<Book>.Enumerator().Current;

            return PartialView("Details", book);

        }


        [HttpDelete]
        public IActionResult Delete(int id, Book book)
        {
            ViewData["Accion"] = "Delete";

            try
            {
                if (book == null)
                {
                    return BadRequest();
                }

                Book books = _repos.GetById(book.Id);

                book.Title = books.Title;
                book.ISBN = books.ISBN;
                book.Autor = books.Autor;
                book.NumPages = books.NumPages;
                book.Description = books.Description;
                book.Quantity = books.Quantity;
                book.IsActive = books.IsActive;

                books.IsActive = false;

                _repos.Update(books, book.Id);

                return View("Index", "Book");
            }

            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }




    }
}