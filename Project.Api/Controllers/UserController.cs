using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components.Forms;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Project.Api.Context;
using Project.Api.Models;

namespace Project.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {
        private readonly ApiDbContext _context;

        public UserController(ApiDbContext context)
        {
            _context = context;
        }

        [HttpGet("GetAll")]        
        public IActionResult Get()
        {
            return Ok(_context.Users.ToList());
        }

        [HttpGet("GetById")]
        public IActionResult GetById(int id)
        {
            return Ok(_context.Users.FirstOrDefault(t => t.Id == id));
        }

        [HttpPost]
        public IActionResult Create([FromBody] User user)
        {
            _context.Add(user);
            _context.SaveChanges();

            return Ok(user.Id);
        }

        [HttpDelete]
        public IActionResult Delete(int id)
        {
            _context.Remove(_context.Users.Find(id));
            _context.SaveChanges();

            return Ok("Delete Succesed ...");
        }

        [HttpPut]
        public IActionResult Update(int id, User user)
        {
            if (user.Id != id)
            {
                return BadRequest();
            }

            _context.Entry(user).State = EntityState.Modified;
            _context.SaveChanges();

            return Ok("Update succesed ...");
        }
    }
}
