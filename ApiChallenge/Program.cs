using ApiChallenge.Data;
using ApiChallenge.Data.Repositories;
using ApiChallenge.Services;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Registrar repositorios
builder.Services.AddScoped<IUserRepository, UserRepository>();

// Registrar servicios
builder.Services.AddScoped<IUserService, UserService>();

// Configure DbContext with connection string from appsettings
builder.Services.AddDbContext<ChallengeDbContext>(options =>
    options.UseMySQL(builder.Configuration.GetConnectionString("DefaultConnection") 
        ?? throw new InvalidOperationException("Connection string 'DefaultConnection' not found.")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    ChallengeDbContext context = scope.ServiceProvider.GetRequiredService<ChallengeDbContext>();
    context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapGet("/", () => "Hello World!");
app.UseHttpsRedirection();

app.MapControllers();

app.Run();
