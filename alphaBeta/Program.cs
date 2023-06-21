using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using alphaBeta.Helpers.Auth;
using Microsoft.Extensions.Options;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddDbContext<GamesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
builder.Services.AddScoped<IBasePlayerRepository<Player>, PlayerRepository>();
builder.Services.AddScoped<IRepositoryUnitOfWork, RepositoryUnitOfWork>();
builder.Services.AddScoped<IBaseBetRepository<Bet>, BetsRepository>();
builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<IBetsService, BetsService>();
builder.Services.AddScoped<ISlotsService, SlotsService>();
builder.Services.AddIdentity<Player, IdentityRole>()    
    .AddEntityFrameworkStores<GamesDbContext>()
    .AddDefaultTokenProviders();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddAuthentication(options =>
{
    options.DefaultAuthenticateScheme = IdentityConstants.ApplicationScheme;
    options.DefaultChallengeScheme = IdentityConstants.ApplicationScheme;
    options.DefaultSignInScheme = IdentityConstants.ExternalScheme;
}).AddIdentityCookies();
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminClaim", policy =>
        policy.Requirements.Add(new HasClaimRequirement("Role", "Admin")));
    options.AddPolicy("RequireUserClaim", policy =>
        policy.Requirements.Add(new HasClaimRequirement("Role", "User")));
});

builder.Services.AddSingleton<IAuthorizationHandler, HasClaimHandler>();

// Замените предыдущую строку регистрации IUserStore на следующую строку:
builder.Services.AddScoped<IUserStore<Player>>(provider => new UserStore<Player>(provider.GetRequiredService<GamesDbContext>()));
builder.Services.AddScoped<UserManager<Player>>();

var app = builder.Build();

app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();