using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;
using Microsoft.AspNetCore.Identity;
using System.Security.Claims;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=ToDo.db";
builder.Services.AddSqlite<ApplicationDBContext>(connectionString);

builder.Services.AddAuthorization();
builder.Services.AddIdentityApiEndpoints<User>()
    .AddEntityFrameworkStores<ApplicationDBContext>();

var app = builder.Build();

app.MapGroup("/api").MapIdentityApi<User>();

app.MapGet("/", () => "Hello World!");


//GET a list of todos for the user
app.MapGet( "/api/todo", (ApplicationDBContext db, ClaimsPrincipal user) =>
{
    return db.Users
            .Include(p => p.ToDos)
            .AsNoTracking()
            .SingleOrDefault(p => p.UserName == user.Identity.Name)
            .ToDos;
}).RequireAuthorization();

//Get a single Todo
app.MapGet("/api/todo/{id}", Results<Ok<ToDo>, NotFound> (ApplicationDBContext db, ClaimsPrincipal user, int id) =>
{

    var todo = db.ToDos.FirstOrDefault(td => td.UserId == user.FindFirstValue(ClaimTypes.NameIdentifier) && td.Id == id);

    if (todo is null)
        return TypedResults.NotFound();

    return TypedResults.Ok(todo);
}).RequireAuthorization();

//Create a new todo for the user
app.MapPost( "/api/todo", (ClaimsPrincipal user, ApplicationDBContext db, ToDo todo) => 
{
    
    todo.UserId = user.FindFirstValue(ClaimTypes.NameIdentifier);
    db.ToDos.Add(todo);
    db.SaveChanges();
    return Results.Created();
}).RequireAuthorization();

//Update an existing Todo
app.MapPut( "/api/todo/{id}", (ApplicationDBContext db, ClaimsPrincipal user, ToDo updateTodo, int id) => {
    var todo = db.ToDos.SingleOrDefault(p => p.UserId == user.FindFirstValue(ClaimTypes.NameIdentifier) && p.Id == id );
    if (todo is null)
        return Results.NotFound();
    todo.Title = updateTodo.Title;
    todo.IsDone = updateTodo.IsDone;
    db.SaveChanges();

    return Results.NoContent();
}).RequireAuthorization();

//Delete a todo
app.MapDelete( "/api/todo/{id}", (ApplicationDBContext db, int id) => 
{
    var todo = db.ToDos.SingleOrDefault(p => p.Id == id);
    if (todo is null)
        return Results.NotFound();
    db.ToDos.Remove(todo);
    db.SaveChanges();

    return Results.NoContent();
}).RequireAuthorization();

app.Run();
