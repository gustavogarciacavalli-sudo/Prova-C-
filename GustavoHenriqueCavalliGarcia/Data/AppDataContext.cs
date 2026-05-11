using Microsoft.EntityFrameworkCore;
using GustavoHenriqueCavalliGarcia.Models;

namespace GustavoHenriqueCavalliGarcia.Data;

public class AppDataContext : DbContext
{
    public DbSet<Livro> Livros { get; set; }

    protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
    {
        optionsBuilder.UseSqlite("Data Source=Pedro_Gustavo.db");
    }
}
