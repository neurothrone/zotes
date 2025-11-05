using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Zotes.Persistence.Data;
using Zotes.Persistence.Entities;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<ZotesAppDbContext>(options =>
    options.UseInMemoryDatabase("AppDb"));
builder.Services.AddDbContext<ZotesIdentityDbContext>(options =>
    options.UseInMemoryDatabase("AppDb"));

builder.Services
    .AddIdentityCore<User>(options =>
    {
        // Align configuration with the StrongPassword attribute
        options.User.RequireUniqueEmail = true;
        options.Password.RequiredLength = 8;
        options.Password.RequireDigit = true;
        options.Password.RequireLowercase = true;
        options.Password.RequireUppercase = true;
        options.Password.RequireNonAlphanumeric = false;
    })
    .AddRoles<IdentityRole<Guid>>()
    .AddEntityFrameworkStores<ZotesIdentityDbContext>();


builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.Run();