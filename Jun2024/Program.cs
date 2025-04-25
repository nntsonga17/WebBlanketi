using Microsoft.EntityFrameworkCore;
using Models;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddDbContext<Context>(options =>
{
    options.UseSqlServer(builder.Configuration.GetConnectionString("RentCS"));
});
builder.Services.AddCors(options => {
    options.AddPolicy("CORS", builder =>{
        builder.WithOrigins(new string[]{
            "http://localhost:5237",
            "https://localhost:5237",
            "http://127.0.0.1:5237",
            "https://127.0.0.1:5237",
            "https://127.0.0.1:5500",
            "http://127.0.0.1:5500",
            "https://localhost:5500",
            "http://localhost:5500"
        })
        .AllowAnyHeader()
        .AllowAnyMethod();
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


