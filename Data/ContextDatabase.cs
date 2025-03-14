using Microsoft.EntityFrameworkCore;
using WebApi.Models;

namespace WebApi.Data;

public class ContextDatabase : DbContext
{
    public ContextDatabase(DbContextOptions<ContextDatabase> options)
        : base(options) { }

    public DbSet<PersonaModel> personas { get; set; }
}
