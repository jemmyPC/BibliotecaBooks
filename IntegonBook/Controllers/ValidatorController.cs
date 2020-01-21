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
    public class ValidatorController : ControllerBase
    {

        private readonly IRepository<Loan> _reposLoan;
        private readonly IRepository<User> _reposUser;
        private readonly IRepository<Book> _reposBook;
        private readonly IRepository<Status> _reposStatus;
        public ValidatorController(IRepository<Loan> reposLoan, IRepository<User> reposUser, IRepository<Book> reposBook, IRepository<Status> reposStatus)
        {
            _reposLoan = reposLoan;
            _reposUser = reposUser;
            _reposBook = reposBook;
            _reposStatus = reposStatus;
        }

        [HttpGet("{id}")]
        public IEnumerable<Object> Debts(int id)
        {
            var loans = _reposLoan.GetAll();
            var users = _reposUser.GetAll().Where(u => u.ID == id);
            var status = _reposStatus.GetAll();
            var books = _reposBook.GetAll();
            var query = from l in loans
                        join u in users on l.UserID equals u.ID
                        join s in status on l.StatusId equals s.Id
                        join b in books on l.IdBook equals b.Id
                        select new { l.Id, u.Name, u.LastName, l.DateCreate, l.DateFinish, b.Title, s.status, l.StatusId, l.Debt };
            query = query.Where(l => l.StatusId == 4 || l.StatusId == 5).ToList();
            return query;
        }

        [HttpPost("{id}")]
        public IEnumerable<Object> ActiveLoans(int id)
        {
            var loans = _reposLoan.GetAll();
            var users = _reposUser.GetAll().Where(u => u.ID == id);
            var status = _reposStatus.GetAll();
            var books = _reposBook.GetAll();
            var query = from l in loans
                        join u in users on l.UserID equals u.ID
                        join s in status on l.StatusId equals s.Id
                        join b in books on l.IdBook equals b.Id
                        select new { l.Id, u.Name, u.LastName, l.DateCreate, l.DateFinish, b.Title, s.status, l.StatusId, l.Debt };
            return query.ToList();
        }

    }


}