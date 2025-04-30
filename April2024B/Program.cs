using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddControllers();
builder.Services.AddDbContext<Context>(options => {
    options.UseSqlServer(builder.Configuration.GetConnectionString("MaratonCS"));
});
builder.Services.AddCors(options => {
    options.AddPolicy("CORS", builder=> {
        builder.WithOrigins(new string[]{
            "http://localhost:5141",
            "https://localhost:5141",
            "http://127.0.0.1:5141",
            "https://127.0.0.1:5141",
            "https://127.0.0.1:5500",
            "http://127.0.0.1:5500",
            "https://localhost:5500",
            "http://localhost:5500"
        });
    });
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseStaticFiles();

app.UseRouting();

app.UseCors("CORS");

app.UseAuthorization();

app.MapControllers();

app.Run();

