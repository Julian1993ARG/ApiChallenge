using ApiChallenge.Data;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<ChallengeDbContext>();

var app = builder.Build();

using (var scope = app.Services.CreateScope())
{
    ChallengeDbContext context = scope.ServiceProvider.GetRequiredService<ChallengeDbContext>();
    context.Database.EnsureCreated();
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
