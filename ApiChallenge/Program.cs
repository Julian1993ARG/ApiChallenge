using ApiChallenge.Data;
using ApiChallenge.Data.Entities.Dtos;
using ApiChallenge.Data.Repositories;
using ApiChallenge.Data.Validations;
using ApiChallenge.Services;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Repository registrations
builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<IDomicilioRepository, DomicilioRepository>();

// Service registrations
builder.Services.AddScoped<IUserService, UserService>();
builder.Services.AddScoped<IDomicilioService, DomicilioService>();

builder.Services.AddAutoMapper(typeof(Program));

// Validator registrations
builder.Services.AddScoped<IValidator<CreateUserDto>, CreateUserValidation>();
builder.Services.AddScoped<IValidator<CreateUserWithAddressDto>, CreateUserWithAddressValidation>();
builder.Services.AddScoped<IValidator<CreateAddressDto>, CreateDomicilioForUserValidation>();

// Configure DbContext with connection string from appsettings
builder.Services.AddDbContext<ApplicationDbContext>(
    options => options.UseNpgsql(builder.Configuration.GetConnectionString("Database")));

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    ApplicationDbContext context = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();
    //context.Database.Migrate();
}

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.MapControllers();

app.Run();
