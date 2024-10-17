using ToDoApi.Models;

namespace ToDoApi.Tests.Unit;

public class UserTest
{
    [Fact]
    public void TestHasThingsToDo_HasOneItem()
    {
        var user = new User() 
        {
            Email = "me@example.com",
            ToDos = [
                new ToDo() {
                    Title = "Mow the lawn",
                    IsDone = true
                },
                new ToDo() {
                    Title = "Do the dishes",
                    IsDone = false
                }
            ]
        };

        Assert.True( user.HasThingsToDo() );
    }

    [Fact]
    public void TestHasThingsToDo_AllDone()
    {
        var user = new User() 
        {
            Email = "me@example.com",
            ToDos = [
                new ToDo() {
                    Title = "Mow the lawn",
                    IsDone = true
                },
                new ToDo() {
                    Title = "Do the dishes",
                    IsDone = true
                }
            ]
        };

        Assert.False( user.HasThingsToDo() );
    }

    [Fact]
    public void TestHasThingsToDo_NoToDos()
    {
        var user = new User() 
        {
            Email = "me@example.com",
            ToDos = []
        };

        Assert.True( user.HasThingsToDo() );
    }
}