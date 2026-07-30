using GamersDock.Data;
using Microsoft.EntityFrameworkCore;
using GamersDock.Entities;

var builder = WebApplication.CreateBuilder(args);

// Add standard services to the container
builder.Services.AddOpenApi();
builder.Services.AddDefaultIdentity<Users>(options => options.SignIn.RequireConfirmedAccount = true)
    .AddEntityFrameworkStores<GamersDockContext>();

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection") ??
    "Data Source=gamersdock.db";

builder.Services.AddDbContext<GamersDockContext>(options =>
    options.UseSqlite(connectionString));

// Build the application (No builder.Services calls allowed after this point)
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
}

app.UseHttpsRedirection();
app.UseAuthorization();

app.Run();