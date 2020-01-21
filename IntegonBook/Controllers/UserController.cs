using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using Models;
using RepositoryPattern.Interfaces;

namespace IntegonBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _reposUser;
        private readonly IRepository<Loan> _reposLoan;

        public UserController(IRepository<User> reposUser, IRepository<Loan> reposLoan)
        {
            _reposUser = reposUser;
            _reposLoan = reposLoan;
        }

        //   GET: Users this Method Get all Users 
        [HttpGet]
        public IEnumerable<User> GetAll()
        {

            var user = _reposUser.GetAll().Where(u=> u.IsActive== true);
            return (user);
        }

        [HttpGet("{id}")]
        public User GetById(int id)
        {
            return _reposUser.GetById(id);
        }

        
        //This Method Create a new User
        [HttpPost]
        public ActionResult Post([FromBody] User user)
        {
        
            var repeatedCURP = _reposUser.GetAll().Where(u => u.CURP == user.CURP).Count();
            var repeatedUserName = _reposUser.GetAll().Where(u => u.UserName == user.UserName).Count();

            if (repeatedCURP >= 1 && repeatedUserName >= 1)
            {
                return BadRequest("{\"CURP\":\"There is a Existing CURP\",\"UserName\":\"There is a UserName Existing\"}");
            }
            if (repeatedCURP >= 1)
            {
                return BadRequest("{\"CURP\":\"There is a Existing CURP\"}");
            }
            if (repeatedUserName >= 1)
            {
                return BadRequest("{\"UserName\":\"There is a UserName Existing\"}");
            }

            user.Quantity = 0;
            user.IsActive = true;
            _reposUser.Insert(user);
                        
            return Ok();
        }
       

        //this Method Edit User (Email and Password)
        [HttpPut]
        public ActionResult Put([FromBody] User user)
        {
            if (user.IsActive == false)
            {
                var loans = _reposLoan.GetAll().Where(l => l.UserID == user.ID && l.StatusId != 10).Count();
                if (loans > 0)
                {
                    return BadRequest("{\"Debt\":\"The user need to get back the book\"}");
                }
            }

            _reposUser.Update(user);
            return Ok();
        }



        

    }
}