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
builder.Services.AddControllers();

var app = builder.Build();

app.MapGet("/", () => "Hello World!");
app.MapGroup("/api").MapIdentityApi<User>();
app.MapControllers();

app.Run();
