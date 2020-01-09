using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ContextDB;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Model;
using RepositoryPattern.Interfaces;

namespace IntegonBook.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly IRepository<User> _repos;
        public UserController(IRepository<User> repository)
        {
            _repos = repository;
        }

        //   GET: Users this Method Get all Users 
        [HttpGet]
        public IEnumerable<User> Index()
        {
            var user = _repos.GetAll();
            return (user);
        }


        [HttpGet("{id}")]
        public User GetById(int id)
        {
            User user = _repos.GetById(id);
            User model = user;
          return model;
        }




        //This Method Create a new User
        [HttpPost]
        public ActionResult PostUser([FromBody] User user)
        {
            try {
                
                    var repeatedCURP = _repos.GetAll().Where(u => u.CURP == user.CURP).Count();
                    var repeatedUserName = _repos.GetAll().Where(u => u.UserName == user.UserName).Count();

                if (repeatedCURP >= 1)
                {
                    return BadRequest("There is a Existing CURP");
                }
                if (repeatedUserName >= 1)
                {

                    return BadRequest("There is a UserName Existing");

                }
                    if (ModelState.IsValid)
                    {
                        User users = new User
                        {
                            Name = user.Name,
                            LastName = user.LastName,
                            CURP = user.CURP,
                            Email = user.Email,
                            UserName = user.UserName,
                            Password = user.Password


                        };
                        _repos.Insert(users);
                        return Ok();
                    }
              
             }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
            return Ok();
        }
       


        //this Method Edit User (Email and Password)
        [HttpPut]
        public ActionResult PutUser([FromBody] User user)
        {

            if (user == null)
            {
                return BadRequest();
            }

            User users = _repos.GetById(user.ID);
            users.Email = user.Email;
            users.Password = user.Password;
           // _repos.Update(users);
            return Ok();
        }


        //This Method Delete a User
        [HttpDelete]
        public ActionResult DeleteUser(User user)
        {
            try
            {
                _repos.Delete(user);
                return Ok();
            }
            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}