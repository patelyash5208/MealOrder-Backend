using Microsoft.EntityFrameworkCore;
using foodorder.Models;


var builder = WebApplication.CreateBuilder(args);

var MyAllowSpecificOrigins = "MyAllowedOrigins";

builder.Services.AddCors(options =>
{
    options.AddPolicy(name: MyAllowSpecificOrigins,
                      policy =>
                      {
                          policy.AllowAnyOrigin().AllowAnyMethod().AllowAnyHeader();
                      });
});
// Add services to the container.

builder.Services.AddControllers();
//builder.Services.AddDbContext<foodorderContext>(opt => opt.UseInMemoryDatabase("FOOD_ORDER"));
builder.Services.AddDbContext<foodorderContext>(opt => opt.UseSqlServer("Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=FOOD_ORDER;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False"));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseCors("MyAllowedOrigins");
app.UseAuthorization();

app.MapControllers();

app.Run();
