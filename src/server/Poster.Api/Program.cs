using System.Text;
using Microsoft.IdentityModel.Tokens;
using Poster.Api.Extensions;
using Poster.Api.Middleware;
using Poster.Application;
using Poster.Infrastructure;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwagger();

builder.Services.AddApplication();
builder.Services.AddInfrastructure(builder.Configuration);

builder.Services.AddAuthentication("JWT")
    .AddJwtBearer("JWT", config =>
    {
        var secretBytes = Encoding.UTF8.GetBytes(builder.Configuration["Secrets:Secret"]);
        var key = new SymmetricSecurityKey(secretBytes);
        
        config.TokenValidationParameters = new TokenValidationParameters()
        {
            ValidIssuer = builder.Configuration["Secrets:Issuer"],
            ValidAudience = builder.Configuration["Secrets:Audience"],
            IssuerSigningKey = key,
        };
    });

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseCustomExceptionHandler();

app.UseHttpsRedirection();

app.UseAuthentication();

app.UseAuthorization();

app.MapControllers();

app.Run();