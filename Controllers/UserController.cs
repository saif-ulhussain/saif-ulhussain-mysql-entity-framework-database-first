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

        [HttpPost("CreateUser")]
        public async Task<HttpStatusCode> CreateUser(UserDTO User)
        {
            var entity = new User()
            {
                FirstName = User.FirstName,
                LastName = User.LastName
            };
            Db1Context.Users.Add(entity);
            await Db1Context.SaveChangesAsync();
            return HttpStatusCode.Created;
        }

        [HttpPut("UpdateUser")]
        public async Task<HttpStatusCode> UpdateUser(UserDTO User)
        {
            var entity = await Db1Context.Users.FirstOrDefaultAsync(s => s.Id == User.Id);
            entity.FirstName = User.FirstName;
            entity.LastName = User.LastName;
            await Db1Context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }

        [HttpDelete("DeleteUser/{Id}")]
        public async Task<HttpStatusCode> DeleteUser(int Id)
        {
            var entity = new User()
            {
                Id = Id
            };
            Db1Context.Users.Attach(entity);
            Db1Context.Users.Remove(entity);
            await Db1Context.SaveChangesAsync();
            return HttpStatusCode.OK;
        }


    }
}