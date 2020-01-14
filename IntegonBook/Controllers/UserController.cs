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

            var user = _repos.GetAll().Where(u=> u.IsActive== true);
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
        public String PostUser([FromBody] User user)
        {
            try {
                
                    var repeatedCURP = _repos.GetAll().Where(u => u.CURP == user.CURP).Count();
                    var repeatedUserName = _repos.GetAll().Where(u => u.UserName == user.UserName).Count();

                if (repeatedCURP >= 1)
                {
                    return "{\"CURP\":\"There is a Existing CURP\"}";
                }
                if (repeatedUserName >= 1)
                {

                    return "{\"UserName\":\"There is a UserName Existing\"}";

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
                        return "{\"OK\":\"OK\"}";
                    }
              
             }
            catch (Exception err)
            {
                return err.Message;
            }
            return "{\"OK\":\"OK\"}";
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
                          
            user.Name = users.Name;
            user.LastName = users.LastName;
            user.CURP = users.CURP;
            user.UserName = user.UserName;
            users.Email = user.Email;
            users.Password = user.Password;
           _repos.Update(users, user.ID);
            return Ok();
        }


        //This Method Delete a User
        [HttpDelete("{id}")]
        public ActionResult DeleteUser([FromBody]  User user)
        {
            try
            {
                if (user == null)
                {
                    return BadRequest();
                }

                User users = _repos.GetById(user.ID);

                user.Name = users.Name;
                user.LastName = users.LastName;
                user.CURP = users.CURP;
                user.UserName = users.UserName;
                user.Email = users.Email;
                user.Password = users.Password;
                user.IsActive = users.IsActive;

                users.IsActive = false;

                _repos.Update(users, user.ID);

                return Ok();
            }

            catch (Exception err)
            {
                return BadRequest(err.Message);
            }
        }
    }
}