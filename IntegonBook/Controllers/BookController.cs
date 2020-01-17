using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models;
using Models.Model;
using RepositoryPattern.Interfaces;

namespace IntegonBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class BookController : ControllerBase
    {
        private readonly IRepository<Book> _repos;
        private readonly IRepository<User> _reposU;
        private readonly IRepository<Loan> _reposL;

        public BookController(IRepository<Book> repository, IRepository<User> reposU, IRepository<Loan> reposL)
        {
            _repos = repository;
            _reposU = reposU;
            _reposL = reposL; 
        }

        //   GET: Book this Method Get all Books 
        [HttpGet]
        public IEnumerable<Book> Index()
        {
            var books = _repos.GetAll().Where(u => u.IsActive == true && u.Quantity >= 2);
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
                
                foreach (Book b in repeatedISBN){
                    return "{\"ISBN\":\"El ISBN ya existe\"}";
                }

                if ((int)book.Quantity > 10)
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
                        Quantity = book.Quantity,
                        IsActive = true,

                    };

                   
                    _repos.Insert(books);
                   

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
        public String PutBook([FromBody]  Book b)
        {

            if (b == null)
            {
                return "{\"OK\":\"Error\"}";
            }

            Book book = _repos.GetById(b.Id);

            if (b.Description == null)
            {

                var loan = _reposL.GetAll().Where(u => u.IdBook == b.Id && u.DateFinish == null).Count();


                if (loan + (int)book.Quantity + b.Quantity > 10)
                {
                    if (loan == 0)
                    {
                        return "{\"Quantity\":\"You can not add more than 10 copies\"}";
                    }
                    else if (loan == 1)
                    {
                        return "{\"Quantity\":\"You can not add more than 10 copies, we have " + loan + " copy in loan\"}";
                    }
                    else {
                        return "{\"Quantity\":\"You can not add more than 10 copies, we have " + loan + " copies in loan\"}";
                    }
                    
                }
                book.Quantity = book.Quantity + b.Quantity;
            }
            else
            {
                book.Description = b.Description;
            }

            _repos.Update(book, b.Id);
            return "{\"OK\":\"OK\"}";
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