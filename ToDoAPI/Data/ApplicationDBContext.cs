using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using ToDoApi.Models;

namespace ToDoApi.Data;

public class ApplicationDBContext : IdentityDbContext<IdentityUser>
{
    public ApplicationDBContext(DbContextOptions options)
        : base(options)
    {
    }

    public DbSet<ToDo> ToDos => Set<ToDo>();
}