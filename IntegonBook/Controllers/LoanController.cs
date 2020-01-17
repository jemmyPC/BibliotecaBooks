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
        private readonly AppDbContext _appDb;
        private readonly IRepository<Loan> _repos;
        private readonly IRepository<User> _reposU;
        private readonly IRepository<Book> _reposB;
        private readonly IRepository<Status> _reposS;
        public LoanController(IRepository<Loan> repository, IRepository<User> reposU, IRepository<Book> reposB, IRepository<Status> reposS, AppDbContext appDb)
        {
            _repos = repository;
            _reposU = reposU;
            _reposB = reposB;
            _appDb = appDb;
            _reposS = reposS;
        }

        //   GET: Users this Method Get all Loans 
        [HttpGet]
        public IEnumerable<Object> Index()
        {

            var loans = _repos.GetAll();
            var users = _reposU.GetAll();
            var status = _reposS.GetAll();
            var books = _reposB.GetAll();

            var query = from l in loans
                        join u in users on l.UserID equals u.ID
                        join s in status on l.StatusId equals s.Id
                        join b in books on l.IdBook equals b.Id
                        select new { l.Id, u.Name, u.LastName, l.DateCreate, l.DateFinish, b.Title, s.status, l.StatusId, l.Price };


            return (query);
        }
        [HttpGet("{id}")]
        public Loan GetById(int id)
        {
            Loan loan = _repos.GetById(id);
            return loan;
        }


        //This Method Create a new Loan
        [HttpPost]

        public String PostLoan([FromBody] Loan loan)
        {

            try
            {
                var user = _appDb.User.Where(u => u.ID == loan.UserID).First();
                var book = _appDb.Book.Where(b => b.Id == loan.IdBook).First();

                if (user.Quantity > 2)
                {
                    return "{\"Loan\":\"You Have Theree Loans \"}";
                }
                if (book.Quantity - 1 < 1)
                {
                    return "{\"Loan\":\"There is only ONE Book on LIbrary\"}";
                }

                if (user.IdStatus == 4)
                {
                    return "{\"Loan\":\"Need to deliver an pay your Debs\"}";

                }
                if (user.IdStatus == 5)
                {
                    return "{\"Loan\":\"Have loans for deb\"}";

                }

                Loan loans = new Loan
                {
                      Id = loan.Id,
                     UserID = loan.UserID,
                    IdBook = loan.IdBook,
                    Quantity = 1,
                    IsActive = true




    };

                loans.DateCreate = DateTime.Now;
                loans.StatusId = 9;
                _repos.Insert(loans);

                user.Quantity = user.Quantity + loans.Quantity;
                book.Quantity = book.Quantity - loans.Quantity;
                _reposU.Update(user, user.ID);
                _reposB.Update(book, book.Id);

                return "{\"OK\":\"OK\"}";
            }

            catch (Exception err)
            {
                return "{\"OK\":\"" + err.Message + "\"}";
            }
        }



        [HttpPut]
        public ActionResult BackBook(Loan loan)
        {
            Loan loans = _repos.GetById(loan.Id);
            if(loans.DateFinish != null)
            {
                return BadRequest("This loan have been finished");
            }
            loans.DateFinish = DateTime.Now;
            loans.StatusId = 10;

             _repos.Update(loans, loan.Id);

            User user = _reposU.GetById(loans.UserID);
            Book book = _reposB.GetById(loans.IdBook);
            user.Quantity = user.Quantity - 1;
            book.Quantity = book.Quantity + 1;
            _reposU.Update(user, user.ID);
            _reposB.Update(book, book.Id);
            return Ok();

        }


        //This Method Delete a User
        [HttpDelete]
        public ActionResult DeleteLoan(Loan loan)
        {

            _repos.Delete(loan);
            return Ok();
        }

    }
}


