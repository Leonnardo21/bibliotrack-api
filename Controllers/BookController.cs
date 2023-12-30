using BiblioTrack.Data;
using BiblioTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioTrack.Controllers
{
    [ApiController]
    public class BookController : ControllerBase
    {
        [HttpGet("/")]
        public async Task<IActionResult> GetAsync([FromServices] BiblioTrackDbContext context)
        => Ok( await context.Books.ToListAsync());

        [HttpGet("/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] BiblioTrackDbContext context, [FromRoute] int id)
        {

            var model = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (model == null)
                return BadRequest("Livro não encontrado");
            

            return Ok(model);
        }

        [HttpPost("/")]
        public async Task<IActionResult> PostAsync([FromServices] BiblioTrackDbContext context, [FromBody] Book model) {

            var book = new Book { 
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Gender = model.Gender,
                ISBN = model.ISBN,
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return Created($"{book.Id}", book);
        }

        [HttpPut("/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromServices] BiblioTrackDbContext context, [FromBody] Book model)
        {
            var book = await context.Books.FirstOrDefaultAsync(x =>x.Id == id);
            if(book == null)
                return BadRequest("Livro não encontrado");

            book.Title = model.Title;
            book.Author = model.Author;
            book.Gender = model.Gender; 
            book.ISBN = model.ISBN;

            context.Books.Update(book);
            await context.SaveChangesAsync();

            return Ok("Informações do livro atualizado!");
        }

        [HttpDelete("/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BiblioTrackDbContext context) { 
            var bookId = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (bookId == null)
                return BadRequest("Livro não encontrado!");

            context.Books.Remove(bookId);
            await context.SaveChangesAsync();

            return Ok("Livro removido com sucesso!");
        }
    }
}
