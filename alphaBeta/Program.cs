using Microsoft.AspNetCore.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
builder.Services.AddDbContext<GamesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddScoped<IBaseRepository<Player>, PlayerRepository>();
builder.Services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();
builder.Services.AddScoped<IBaseRepository<Bet>, BetsRepository>();
builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<IBetsService, BetsService>();
builder.Services.AddScoped<ISlotsService, SlotsService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();


var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
