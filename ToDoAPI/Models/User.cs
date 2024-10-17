using Microsoft.AspNetCore.Identity;

namespace ToDoApi.Models;

public class User : IdentityUser
{

    public ICollection<ToDo> ToDos { get; set; } = [];

    public bool HasThingsToDo()
    {
        foreach (var item in ToDos)
        {
            if ( item.IsDone == false){
                return true;
            }
        }

        return ToDos.Count > 0 ? false : true;
    }
}