using Microsoft.EntityFrameworkCore;
using WebApi.Data;
using WebApi.Filters;
using WebApi.Services;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddDbContext<ContextDatabase>(opt =>
    opt.UseSqlServer(builder.Configuration.GetConnectionString("Connection"))
);
builder.Services.AddScoped<Persona_Services>();
builder.Services.AddScoped<CustomNotFound>();

builder.Services.AddControllers(opt =>
{
    opt.Filters.Add<CustomNotFound>();
});

builder.Services.AddControllers();
builder.Services.AddOpenApi();

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
