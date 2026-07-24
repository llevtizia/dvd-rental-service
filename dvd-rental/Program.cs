using Scalar.AspNetCore;
using Microsoft.EntityFrameworkCore;

using dvd_rental.Data;



var builder = WebApplication.CreateBuilder(args);

// database: SQLite + seeding del catalogo DVD
builder.Services.AddDbContext<AppDbContext>( options => 
    options.UseSqlite( builder.Configuration.GetConnectionString("DefaultConnection"))
    .UseSeeding( (context, _) =>
    {
        var db = (AppDbContext)context;
        if ( !db.Dvds.Any() )
        {
            db.Dvds.AddRange( DvdSeedData.GetDvdSeedData() );
            db.SaveChanges();
        }
    })
    .UseAsyncSeeding( async (context, _, cancellationToken) =>
    {
        var db = (AppDbContext)context;
        if ( !await db.Dvds.AnyAsync(cancellationToken))
        {
            db.Dvds.AddRange( DvdSeedData.GetDvdSeedData() );
            await db.SaveChangesAsync(cancellationToken);
        }
    
    }));


// CORS -> autorizza il frontend 
builder.Services.AddCors( options =>
{
    options.AddPolicy( "AllowFrontend", policy =>
        policy.WithOrigins( "http://localhost:5173") // da controllare
        .AllowAnyHeader()
        .AllowAnyMethod());
});

// Add services to the container.
builder.Services.AddControllers();

// Learn more about configuring OpenAPI at https://aka.ms/aspnet/openapi
builder.Services.AddOpenApi();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.MapOpenApi();
    app.MapScalarApiReference();
}

app.UseHttpsRedirection();

app.UseCors("AllowFrontend"); // attiva la policy nella pipeline

app.UseAuthorization();

app.MapControllers();

app.Run();
