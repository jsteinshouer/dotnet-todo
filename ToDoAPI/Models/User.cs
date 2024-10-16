using Microsoft.AspNetCore.Identity;

namespace ToDoApi.Models;

public class User : IdentityUser
{

    public ICollection<ToDo> ToDos { get; set; } = [];
}