using Microsoft.EntityFrameworkCore;
using ToDoApi.Models;

namespace ToDoApi.Data;

public class ApplicationDBContext : DbContext
{
    public ApplicationDBContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<ToDo> ToDos => Set<ToDo>();
}