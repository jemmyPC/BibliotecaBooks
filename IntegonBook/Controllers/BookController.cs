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
        public String PostBook([FromBody] Book book)
        {
            try
            {
                var repeatedISBN = _repos.GetAll().Where(u => u.ISBN == book.ISBN);
                int quantityBook = 0;

                foreach (Book b in repeatedISBN){
                    quantityBook = (int)b.Quantity;
                }
                int total = quantityBook + (int)book.Quantity;
                if (total > 10)
                {
                    return "{\"Quantity\":\"There is to much Books Only can be 10\"}";
                    
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

                    if(repeatedISBN.Count() == 0){
                        _repos.Insert(books);
                    }
                    else {
                        Book b = repeatedISBN.FirstOrDefault();
                        b.Quantity = total;
                        _repos.Update(b, b.Id);
                    }

                    return "{\"OK\":\"OK\"}";
                }
                return "{\"OK\":\"OK\"}";
            }
            catch (Exception err)
            {
                return "{\"OK\":\""+err.Message+"\"}";
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