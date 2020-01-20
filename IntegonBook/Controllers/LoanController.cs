using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Web;
using Microsoft.EntityFrameworkCore;
using Models;
using Models.Model;
using RepositoryPattern.Interfaces;


namespace IntegonBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoanController : ControllerBase
    {
     
        private readonly IRepository<Loan> _reposLoan;
        private readonly IRepository<User> _reposUser;
        private readonly IRepository<Book> _reposBook;
        private readonly IRepository<Status> _reposStatus;
        public LoanController(IRepository<Loan> reposLoan, IRepository<User> reposUser, IRepository<Book> reposBook, IRepository<Status> reposStatus)
        {
            _reposLoan = reposLoan;
            _reposUser = reposUser;
            _reposBook = reposBook;
            _reposStatus = reposStatus;
        }

        //   GET: Users this Method Get all Loans 
        [HttpGet]
        public IEnumerable<Object> GetAll()
        {

            var loans = _reposLoan.GetAll();
            var users = _reposUser.GetAll();
            var status = _reposStatus.GetAll();
            var books = _reposBook.GetAll();

            var query = from l in loans
                        join u in users on l.UserID equals u.ID
                        join s in status on l.StatusId equals s.Id
                        join b in books on l.IdBook equals b.Id
                        select new { l.Id, u.Name, u.LastName, l.DateCreate, l.DateFinish, b.Title, s.status, l.StatusId, l.Debt};


            return (query);
        }
        [HttpGet("{id}")]
        public Loan GetById(int id)
        {
            Loan loan = _reposLoan.GetById(id);
            return loan;
        }


        //This Method Create a new Loan
        [HttpPost]

        public ActionResult Post([FromBody] Loan loan)
        {
            User user = _reposUser.GetById(loan.UserID);
            Book book = _reposBook.GetById(loan.IdBook);

            if (user.Quantity > 2)
            {
                return BadRequest("{\"Loan\":\"You Have Theree Loans \"}");
            }
            if (book.Quantity - 1 < 1)
            {
                return BadRequest("{\"Loan\":\"There is only ONE Book on LIbrary\"}");
            }
                
            if (_reposLoan.GetAll().Where(l => l.UserID == user.ID && l.StatusId == 4).Count() > 0)
            {
                return BadRequest("{\"Loan\":\"Need to deliver and pay your Debts\"}");

            }
            if (_reposLoan.GetAll().Where(l => l.UserID == user.ID && l.StatusId == 5).Count() > 0)
            {
                return BadRequest("{\"Loan\":\"Have loans for debt\"}");

            }

            loan.DateCreate = DateTime.Now;
            loan.StatusId = 9;
            loan.IsActive = true;
            _reposLoan.Insert(loan);

            user.Quantity = user.Quantity + 1;
            book.Quantity = book.Quantity - 1;
            _reposUser.Update(user);
            _reposBook.Update(book);

            return Ok();
        }



        [HttpPut]
        public ActionResult BackBook(Loan loan)
        {
            if(loan.DateFinish != null)
            {
                return BadRequest("{\"Loan\":\"This loan have been finished\"}");
            }
            loan.DateFinish = DateTime.Now;
            loan.StatusId = 10;

             _reposLoan.Update(loan);

            User user = _reposUser.GetById(loan.UserID);
            Book book = _reposBook.GetById(loan.IdBook);
            user.Quantity = user.Quantity - 1;
            book.Quantity = book.Quantity + 1;
            _reposUser.Update(user);
            _reposBook.Update(book);
            return Ok();
        }
    }
}


