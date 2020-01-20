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
        private readonly IRepository<Book> _reposBook;
        private readonly IRepository<Loan> _reposLoan;

        public BookController(IRepository<Book> reposBook, IRepository<Loan> reposLoan)
        {
            _reposBook = reposBook;
            _reposLoan = reposLoan; 
        }

        //   GET: Book this Method Get all Books 
        [HttpGet]
        public IEnumerable<Book> GetAll()
        {
            var books = _reposBook.GetAll().Where(u => u.IsActive == true && u.Quantity > 1);
            return (books);
        }

        [HttpGet("{id}")]
        public Book GetById(int id)
        {
            return _reposBook.GetById(id);
        }



        //This Method Create a new Book
        [HttpPost]
        public ActionResult PostBook([FromBody] Book book)
        {

            var repeatedISBN = _reposBook.GetAll().Where(u => u.ISBN == book.ISBN);
                
            foreach (Book b in repeatedISBN){
                return BadRequest("{\"ISBN\":\"This ISBN already exist\"}");
            }

            if ((int)book.Quantity > 10)
            {
                return BadRequest("{\"Quantity\":\"Maximum books allowed are 10\"}");
            }
            book.IsActive = true;

            _reposBook.Insert(book);
            return Ok();

        }

        //this Method Edit Book (Description)
        [HttpPut]
        public ActionResult PutBook([FromBody]  Book book)
        {

            int quantity = book.Description.EndsWith("+1") ? 1 : book.Description.EndsWith("-1") ? -1 : 0;
            if (quantity != 0)
            {

                var loan = _reposLoan.GetAll().Where(u => u.IdBook == book.Id && u.DateFinish == null).Count();


                if (loan + (int)book.Quantity + quantity > 10)
                {
                    if (loan == 0)
                    {
                        return BadRequest("{\"Quantity\":\"Maximum books allowed are 10\"}");
                    }
                    else if (loan == 1)
                    {
                        return BadRequest("{\"Quantity\":\"You can not add more than 10 copies, we have " + loan + " copy in loan\"}");
                    }
                    else {
                        return BadRequest("{\"Quantity\":\"You can not add more than 10 copies, we have " + loan + " copies in loan\"}");
                    }
                    
                }
                book.Description = book.Description.Remove(book.Description.Length-2);
                book.Quantity = book.Quantity + quantity;
            }

            _reposBook.Update(book);
            return Ok();
        }

    }
}