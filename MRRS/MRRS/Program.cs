using Microsoft.EntityFrameworkCore;
using MRRS;
using MRRS.Service;

var builder = WebApplication.CreateBuilder(args);
var configuration = builder.Configuration;

builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddDbContext<ReservationDbContext>(
    options =>
    {
        options.UseSqlite(configuration.GetConnectionString("ConnString"));
    });
builder.Services.AddScoped<RoomResService>();
builder.Services.AddScoped<RoomService>();

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
 