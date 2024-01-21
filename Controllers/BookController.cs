using BiblioTrack.Data;
using BiblioTrack.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace BiblioTrack.Controllers
{
    [ApiController]
    [Route("/api/book/")]
    public class BookController : ControllerBase
    {
        private static List<Book> livros = new List<Book>();

        [HttpGet("/api/books")]
        public async Task<IActionResult> GetAllAsync([FromServices] BiblioTrackDbContext context)
        => Ok(await context.Books.ToListAsync());

        [HttpGet("/api/books/{id:int}")]
        public async Task<IActionResult> GetByIdAsync([FromServices] BiblioTrackDbContext context, [FromRoute] int id)
        {

             var model = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
             if (model == null)
                 return NotFound("Livro não encontrado");


             return Ok(model);
        }

        [HttpPost("/api/books/")]
        public async Task<IActionResult> PostAsync([FromServices] BiblioTrackDbContext context, [FromBody] Book model)
        {

            var book = new Book
            {
                Id = model.Id,
                Title = model.Title,
                Author = model.Author,
                Gender = model.Gender,
                ISBN = model.ISBN,
                IsDisponibility = model.IsDisponibility,
            };

            context.Books.Add(book);
            await context.SaveChangesAsync();

            return Created($"{book.Id}", book);
        }

        [HttpPut("/api/books/{id:int}")]
        public async Task<IActionResult> PutAsync([FromRoute] int id, [FromServices] BiblioTrackDbContext context, [FromBody] Book model)
        {
            var book = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (book == null)
                return NotFound("Livro não encontrado");

            book.Title = model.Title;
            book.Author = model.Author;
            book.Gender = model.Gender;
            book.ISBN = model.ISBN;
            book.IsDisponibility = model.IsDisponibility;

            context.Books.Update(book);
            await context.SaveChangesAsync();

            return Ok("Informações do livro atualizado!");
        }

        [HttpDelete("/api/books/{id:int}")]
        public async Task<IActionResult> DeleteAsync([FromRoute] int id, [FromServices] BiblioTrackDbContext context)
        {
            var bookId = await context.Books.FirstOrDefaultAsync(x => x.Id == id);
            if (bookId == null)
                return NotFound("Livro não encontrado!");

            context.Books.Remove(bookId);
            await context.SaveChangesAsync();

            return Ok("Livro removido com sucesso!");
        }
    }
}
