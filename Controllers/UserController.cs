using BiblioTrack.Data;
using BiblioTrack.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioTrack.Controllers
{
    [ApiController]
    [Route("/api/user/")]
    public class UserController : ControllerBase
    {

        [HttpGet]
        public async Task<IActionResult> GetAllUsersAsync([FromServices] BiblioTrackDbContext context) => Ok(await context.Users.ToListAsync());
        

        [HttpGet("{id:int}")]
        public async Task<IActionResult> GetUserById([FromServices] BiblioTrackDbContext context, [FromRoute] int id)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
                if(user == null)
                {
                    return NotFound();
                }

                return Ok(user);
            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPost]
        public async Task<IActionResult> PostAsync([FromServices] BiblioTrackDbContext context, [FromBody] User model) {
            try
            {
                var user = new User
                {
                    Id = model.Id,
                    Name = model.Name,
                    Email = model.Email,
                    Phone = model.Phone,
                    Username = model.Username,
                    PasswordHash = model.PasswordHash,
                    CreatedAt = DateTime.Now,
                    IsActive = true,
                };

                await context.Users.AddAsync(user);
                context.SaveChanges();
                return Created($"Usuário criado com sucesso", user.Id);
            }catch(Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpPut("{id:int}")]
        public async Task<IActionResult> PutAsync([FromServices] BiblioTrackDbContext context, [FromRoute] int id, [FromBody] User model)
        {
            try
            {
                var user = await context.Users.FirstOrDefaultAsync(x => x.Id == id);

                if(user == null)
                    return NotFound();

                user.Name = model.Name;
                user.Email = model.Email;
                user.Phone = model.Phone;
                user.Username = model.Username;
                user.PasswordHash = model.PasswordHash;

                context.Users.Update(user);
                await context.SaveChangesAsync();

                return Ok("Dados atualizados!");

            }catch (Exception ex)
            {
                return BadRequest(ex.Message);
            }
        }

        [HttpDelete("{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BiblioTrackDbContext context) {
            var userId = await context.Users.FirstOrDefaultAsync(x => x.Id == id);
            if( userId == null)
                return NotFound();

            context.Users.Remove(userId) ;
            context.SaveChanges();

            return Ok("Usuário removido!");
        }
    }
}
