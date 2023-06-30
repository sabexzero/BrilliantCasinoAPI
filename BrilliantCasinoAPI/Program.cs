using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using BrilliantCasinoAPI.Helpers.Auth;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.IdentityModel.Tokens;
using System.Text;
using BrilliantCasinoAPI.Middleware;
using Microsoft.OpenApi.Models;

using BrilliantCasinoAPI.Services.Concrete;
using BrilliantCasinoAPI.Services.Abstract;
using BrilliantCasinoAPI.Data;
using BrilliantCasinoAPI.Data.Repositories.Abstract;
using BrilliantCasinoAPI.Data.Repositories.Concrete;
using BrilliantCasinoAPI.Models.Concrete;

var builder = WebApplication.CreateBuilder(args);


builder.Services.AddControllers();
builder.Services.AddDbContext<GamesDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetSection("ConnectionStrings")["DefaultConnection"]));
builder.Services.AddScoped<IBaseBetRepository, BetsRepository>();
builder.Services.AddScoped<IBaseLobbyRepository, LobbyRepository>();
builder.Services.AddScoped<IPlayersService, PlayersService>();
builder.Services.AddScoped<IBetsService, BetsService>();
builder.Services.AddScoped<ISlotsService, SlotsService>();
builder.Services.AddScoped<ILobbyService, LobbyService>();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "JWT Authorization header using the Bearer scheme",
        Type = SecuritySchemeType.Http,
        Scheme = "bearer",
        BearerFormat = "JWT"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            new string[] {}
        }
    });
});
builder.Services.AddAuthorization(options =>
{
    options.AddPolicy("RequireAdminClaim", policy =>
        policy.Requirements.Add(new HasClaimRequirement("Role", "Admin")));
    options.AddPolicy("RequireUserClaim", policy =>
        policy.Requirements.Add(new HasClaimRequirement("Role", "User")));
});

builder.Services.AddAuthentication(cfg =>
{
    cfg.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    cfg.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
})
       .AddJwtBearer(options =>
                {
                    options.TokenValidationParameters = new TokenValidationParameters
                    {
                        ValidateIssuer = false,
                        ValidateAudience = false,
                        ValidateLifetime = true,
                        ValidateIssuerSigningKey = true,
                        ValidIssuer = "yourdomain.com",
                        ValidAudience = "yourdomain.com",
                        IssuerSigningKey = new SymmetricSecurityKey(
                            Encoding.UTF8.GetBytes("My secret goes here really"))
                    };

                    options.RequireHttpsMetadata = false;
                });

builder.Services.AddIdentity<Player, IdentityRole>()
    .AddEntityFrameworkStores<GamesDbContext>();

builder.Services.AddSingleton<IAuthorizationHandler, HasClaimHandler>();
builder.Services.AddScoped<IUserStore<Player>>(provider => new UserStore<Player>(provider.GetRequiredService<GamesDbContext>()));
builder.Services.AddScoped<UserManager<Player>>();

var app = builder.Build();

app.UseSwagger();

app.UseSwaggerUI();

//app.UseMiddleware<CustomExceptionHandlerMiddleware>();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();