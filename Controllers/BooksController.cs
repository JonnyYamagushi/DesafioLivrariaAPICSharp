using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DesafioLivrariaAPICSharp.Data;
using DesafioLivrariaAPICSharp.Models;
using DesafioLivrariaAPICSharp.Ultities;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace DesafioLivrariaAPICSharp.Controllers
{
    /// <summary>
    /// Controlador de API para gerenciamento de livros.
    /// </summary>
    [Route("api/[controller]")]
    [ApiController]
    [Produces("application/json")]
    public class BooksController : ControllerBase
    {
        private readonly BookstoreContext _context;

        public BooksController(BookstoreContext context)
        {
            try
            {
                _context = context;

                // Seed: livros de exemplo
                if (!_context.Books.Any())
                {
                    _context.Books.AddRange(
                        new Book { Title = "1984", Author = "George Orwell", Genre = "Ficção", Price = 29.90m, StockQuantity = 10 },
                        new Book { Title = "O Senhor dos Anéis", Author = "J.R.R. Tolkien", Genre = "Fantasia", Price = 79.50m, StockQuantity = 5 }
                    );
                    _context.SaveChanges();
                }
            }
            catch (Exception ex)
            {
                Functions.EscreveLog("BooksController", ex.Message);
            }
        }

        /// <summary>
        /// Retorna todos os livros (ou lista vazia).
        /// </summary>
        [HttpGet]
        [ProducesResponseType(typeof(IEnumerable<Book>), StatusCodes.Status200OK)]
        public async Task<ActionResult<IEnumerable<Book>>> GetBooks()
        {
            try
            {
                var books = await _context.Books.ToListAsync();
                return Ok(books);
            }
            catch (Exception ex)
            {
                Functions.EscreveLog("BooksController/GetBooks", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { StatusCode = 500, Message = "Erro interno no servidor." });
            }
        }

        /// <summary>
        /// Retorna um livro pelo ID.
        /// </summary>
        [HttpGet("{id:int}")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<ActionResult<Book>> GetBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                    return NotFound(new { Message = $"Livro com ID {id} não encontrado." });

                return Ok(book);
            }
            catch (Exception ex)
            {
                Functions.EscreveLog("BooksController/GetBook", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { StatusCode = 500, Message = "Erro interno no servidor." });
            }
        }

        /// <summary>
        /// Cria um novo livro.
        /// </summary>
        [HttpPost]
        [Consumes("application/json")]
        [ProducesResponseType(typeof(Book), StatusCodes.Status201Created)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<ActionResult<Book>> CreateBook([FromBody] Book book)
        {
            try
            {
                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool conflictExists = await _context.Books
                    .AnyAsync(b =>
                        b.Title.ToLower() == book.Title.ToLower() &&
                        b.Author.ToLower() == book.Author.ToLower());

                if (conflictExists)
                    return Conflict(new { Message = "Já existe um livro com o mesmo título e autor." });

                _context.Books.Add(book);

                await _context.SaveChangesAsync();

                return CreatedAtAction(nameof(GetBook), new { id = book.Id }, book);
            }
            catch (Exception ex)
            {
                Functions.EscreveLog("BooksController/CreateBook", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { StatusCode = 500, Message = "Erro interno no servidor." });
            }
        }

        /// <summary>
        /// Atualiza um livro existente.
        /// </summary>
        [HttpPut("{id:int}")]
        [Consumes("application/json")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status400BadRequest)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        [ProducesResponseType(StatusCodes.Status409Conflict)]
        public async Task<IActionResult> UpdateBook(int id, [FromBody] Book updatedBook)
        {
            try
            {
                if (id != updatedBook.Id)
                    return BadRequest(new { Message = "O ID da URL deve corresponder ao ID no corpo da requisição." });

                if (!ModelState.IsValid)
                    return BadRequest(ModelState);

                bool conflictExists = await _context.Books
                    .AnyAsync(b =>
                        b.Id != id &&
                        b.Title.ToLower() == updatedBook.Title.ToLower() &&
                        b.Author.ToLower() == updatedBook.Author.ToLower());

                if (conflictExists)
                    return Conflict(new { Message = "Já existe outro livro com o mesmo título e autor." });

                var existingBook = await _context.Books.FindAsync(id);
                if (existingBook == null)
                    return NotFound(new { Message = $"Livro com ID {id} não encontrado." });

                existingBook.Title = updatedBook.Title.Trim();
                existingBook.Author = updatedBook.Author.Trim();
                existingBook.Genre = updatedBook.Genre.Trim();
                existingBook.Price = updatedBook.Price;
                existingBook.StockQuantity = updatedBook.StockQuantity;

                _context.Entry(existingBook).State = EntityState.Modified;

                await _context.SaveChangesAsync();

                return NoContent();
            }
            catch (Exception ex)
            {
                Functions.EscreveLog("BooksController/UpdateBook", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { StatusCode = 500, Message = "Erro interno no servidor." });
            }
        }

        /// <summary>
        /// Exclui um livro.
        /// </summary>
        [HttpDelete("{id:int}")]
        [ProducesResponseType(StatusCodes.Status204NoContent)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        public async Task<IActionResult> DeleteBook(int id)
        {
            try
            {
                var book = await _context.Books.FindAsync(id);
                if (book == null)
                    return NotFound(new { Message = $"Livro com ID {id} não encontrado." });

                _context.Books.Remove(book);
                await _context.SaveChangesAsync();
                return NoContent();
            }
            catch (Exception ex)
            {
                Functions.EscreveLog("BooksController/DeleteBook", ex.Message);
                return StatusCode(StatusCodes.Status500InternalServerError,
                    new { StatusCode = 500, Message = "Erro interno no servidor." });
            }
        }
    }
}
