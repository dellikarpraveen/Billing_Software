using Microsoft.AspNetCore.Builder;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Configure EF Core
builder.Services.AddDbContext<Billing_Software.ApiScaffold.Data.AppDbContext>(options =>
    options.UseSqlServer(builder.Configuration.GetConnectionString("ApiDatabase") ?? "Data Source=(localdb)\\MSSQLLocalDB;Initial Catalog=BillingApiDb;Integrated Security=True;"));

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Allow minimal CORS for development frontends
builder.Services.AddCors(options => options.AddDefaultPolicy(policy =>
    policy.AllowAnyHeader().AllowAnyMethod().AllowAnyOrigin()));

// Add minimal services
builder.Services.AddHttpContextAccessor();
builder.Services.AddSwaggerGen();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseDeveloperExceptionPage();
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.MapControllers();

app.UseCors();

// Redirect root to Swagger UI for local development convenience
app.MapGet("/", () => Results.Redirect("/swagger"));

// Ensure DB created for development (no migrations applied here)
using (var scope = app.Services.CreateScope())
{
    var db = scope.ServiceProvider.GetRequiredService<Billing_Software.ApiScaffold.Data.AppDbContext>();
    db.Database.EnsureCreated();
}

app.Run();
