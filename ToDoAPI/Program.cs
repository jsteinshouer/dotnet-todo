using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.EntityFrameworkCore;
using ToDoApi.Data;
using ToDoApi.Models;

var builder = WebApplication.CreateBuilder(args);

var connectionString = "Data Source=ToDo.db";
builder.Services.AddSqlite<ApplicationDBContext>(connectionString);

var app = builder.Build();

app.MapGet("/", () => "Hello World!");

app.MapGet( "/api/todo", (ApplicationDBContext db) => db.ToDos.AsNoTracking().ToList() );
app.MapPost( "/api/todo", (ApplicationDBContext db, ToDo todo) => {
    db.ToDos.Add(todo);
    db.SaveChanges();
    return Results.Created();
});
app.MapPut( "/api/todo/{id}", (ApplicationDBContext db, ToDo updateTodo, int id) => {
    var todo = db.ToDos.SingleOrDefault(p => p.Id == id);
    if (todo is null)
        return Results.NotFound();
    todo.Title = updateTodo.Title;
    todo.IsDone = updateTodo.IsDone;
    db.SaveChanges();

    return Results.NoContent();
});
app.MapDelete( "/api/todo/{id}", (ApplicationDBContext db, int id) => {
    var todo = db.ToDos.SingleOrDefault(p => p.Id == id);
    if (todo is null)
        return Results.NotFound();
    db.ToDos.Remove(todo);
    db.SaveChanges();

    return Results.NoContent();
});

app.Run();
