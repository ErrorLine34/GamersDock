using GamersDock.Data;
using GamersDock.Endpoints;
using GamersDock.Entities;
using Microsoft.EntityFrameworkCore;

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

app.MapGameEndpoints();
app.MapProfileEndpoints();
app.MapUserEndpoints();
app.MapInstanceSettingsEndpoints();

app.UseHttpsRedirection();
app.UseAuthorization();

app.Run();