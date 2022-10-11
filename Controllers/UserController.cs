using Test7.DTO;
using Test7.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Net;

namespace Test7.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class UserController : ControllerBase
    {
        private readonly Db1Context Db1Context;

        public UserController(Db1Context Db1Context)
        {
            this.Db1Context = Db1Context;
        }
        [HttpGet("AllUsers")]
        public async Task<ActionResult<List<UserDTO>>> Get()
        {
            var List = await Db1Context.Users.Select(
                s => new UserDTO
                {
                    Id = s.Id,
                    FirstName = s.FirstName,
                    LastName = s.LastName
                }
            ).ToListAsync();

            if (List.Count < 0)
            {
                return NotFound();
            }
            else
            {
                return List;
            }
        }

        [HttpGet("GetUserById")]
        public async Task<ActionResult<UserDTO>> GetUserById(int Id)
        {
            UserDTO User = await Db1Context.Users.Select(s => new UserDTO
            {
                Id = s.Id,
                FirstName = s.FirstName,
                LastName = s.LastName,
            }).FirstOrDefaultAsync(s => s.Id == Id);
            if (User == null)
            {
                return NotFound();
            }
            else
            {
                return User;
            }
        }

    }
}