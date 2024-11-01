using TodoWebAPI.Services;
using TodoWebAPI;
using Microsoft.EntityFrameworkCore;
using TodoWebAPI.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
var mssql = builder.Configuration.GetConnectionString("DefaultConnection");
var mySql = builder.Configuration.GetConnectionString("MySQLConnection");


builder.Services.AddDbContext<SQLTodoDbContext>(options => options.UseSqlServer(mssql));
builder.Services.AddDbContext<MySQLTodoDbContext>(options => options.UseMySql(mySql, ServerVersion.AutoDetect(mySql)));

builder.Services.AddScoped<SqlTodoService>();
builder.Services.AddScoped<MySqlTodoService>();

builder.Services.AddScoped<TodoProviderFactory>();
builder.Services.AddControllers();

builder.Services.AddCors(options => { options.AddDefaultPolicy(builder => { builder.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader(); }); });


// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddHealthChecks();
builder.Services.AddAWSLambdaHosting(LambdaEventSource.HttpApi);

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors();
app.MapHealthChecks("/healthz");
app.UseAuthorization();

app.MapControllers();

app.Run();
