using Exo.WebApi.Contexts;
using Exo.WebApi.Repositories;
using Microsoft.IdentityModel.Tokens;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using System;
using System.Text;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddScoped<ExoContext, ExoContext>();
builder.Services.AddTransient<ProjetoRepository, ProjetoRepository>();
builder.Services.AddTransient<UsuarioRepository, UsuarioRepository>();

builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = true,
            ValidateAudience = true,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("U2FsdGVkX1+S8W5n8VmZKoyHq4+d9l9vqOb8abmHk7M=")),
            ClockSkew = TimeSpan.FromMinutes(5), 
            ValidIssuer = "exo.webApi",
            ValidAudience = "exo.webApi",
        };
    });

builder.Services.AddControllers();

var app = builder.Build();

app.UseRouting();

app.UseAuthentication(); 

app.UseAuthorization();  

app.MapControllers();

app.Run();
