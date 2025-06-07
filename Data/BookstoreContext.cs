using DesafioLivrariaAPICSharp.Models;
using Microsoft.EntityFrameworkCore;

namespace DesafioLivrariaAPICSharp.Data;

public class BookstoreContext : DbContext
{
    public BookstoreContext(DbContextOptions<BookstoreContext> options)
            : base(options)
    {
    }

    // DbSet representando a tabela de livros
    public DbSet<Book> Books { get; set; }
}
