using BLL;
using BLL.Services;
using DAL.EF;
using DAL.Repo;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();


builder.Services.AddDbContext<ZeroHungerDbContext>(opt =>
{
    opt.UseSqlServer(builder.Configuration.GetConnectionString("DbConn"));
});
builder.Services.AddScoped<ResturantRepo>();
builder.Services.AddScoped<ResturantService>();
builder.Services.AddScoped<EmployeeRepo>();
builder.Services.AddScoped<EmployeeService>();
builder.Services.AddScoped<CollectRequestRepo>();
builder.Services.AddScoped<CollectRequestService>();
builder.Services.AddAutoMapper(typeof(MappingProfile));



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
