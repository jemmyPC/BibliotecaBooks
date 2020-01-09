using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using RepositoryPattern.Interfaces;

namespace IntegonBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _repos;
        public BookController(IRepository<Book> repository)
        {
            _repos = repository;
        }

        //   GET: Book this Method Get all Books 
        [HttpGet]
        public IEnumerable<Book> Index()
        {
            var books = _repos.GetAll();
            return (books);
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            Book books = _repos.GetById(id);
            return books;
        }



        //This Method Create a new Book
        [HttpPost]
        public ActionResult PostBook([FromBody] Book book)
        {
            try
            {
                var repeatedISBN = _repos.GetAll().Where(u => u.ISBN == book.ISBN).Count();
                var CountISBN = _repos.GetAll().Where(u => u.Quantity == book.Quantity &&(u.ISBN == book.ISBN)).Count();

                if (repeatedISBN >= 1)
                {

                    return BadRequest("ISBN existing ");
                }
                if ( CountISBN >= 10)
                {
                    return BadRequest("There is to much Books whit the same ISBN");
                }
                if ( book.Quantity >= 10)
                {
                    return BadRequest("There is to much Books Only can be 10");
                }

                if (ModelState.IsValid)
                {
                    Book books = new Book
                    {
                        Id = book.Id,
                        ISBN = book.ISBN,
                        Autor = book.Autor,
                        Title = book.Title,
                        NumPages = book.NumPages,
                        Edition = book.Edition,
                        Description = book.Description,
                        Quantity = book.Quantity
                    };

                    _repos.Insert(books);
                    return Ok();
                }
                return Ok();
            }
            catch (Exception err)
            {
              return BadRequest(err.Message);
            }
        }




        //this Method Edit Book (Description)
        [HttpPut]
        public ActionResult PutBook([FromBody]  Book book)
        {
            if (book == null)
            {
                return BadRequest();
            }
            Book books = _repos.GetById(book.Id);
            books.Description = book.Description;

            _repos.Update(books,book.Id);
            return Ok();
        }

        //This Method Delete a Book
        [HttpDelete]
        public ActionResult DeleteBook(Book book)
        {
            _repos.Delete(book);
            return Ok();
        }

    }
}