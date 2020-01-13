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
        public LoanController(IRepository<Loan> repository, IRepository<User> reposU, IRepository<Book> reposB, AppDbContext appDb)
        {
            _repos = repository;
            _reposU = reposU;
            _reposB = reposB;
            _appDb = appDb;
        }

        //   GET: Users this Method Get all Loans 
        [HttpGet]
        public IEnumerable<Loan> Index()
        {

            var loans = _repos.GetAll();
            return (loans);
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
                    return "{\"Loan\":\"Tiene 3 libros prestados\"}";
                }
                if (book.Quantity - 1 < 1)
                {
                    return "{\"Loan\":\"No Disponible para prestamo\"}";
                }

                if (user.IdStatus == 4)
                {
                    return "{\"Loan\":\"DSGDFYH\"}";

                }
                if (user.IdStatus == 2)
                {
                    return "{\"Loan\":\"Tiene deudas por Pagar\"}";

                }

                Loan loans = new Loan
                {
                      Id = loan.Id,
                     UserID = loan.UserID,
                    IdBook = loan.IdBook,
                    Quantity = 1,
                    Title = book.Title




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
            loans.DateFinish = DateTime.Now;
            loans.StatusId = 10;
             _repos.Update(loans, loan.Id);
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


