using apiFIDO.Data;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.DependencyInjection;
using Pomelo.EntityFrameworkCore.MySql;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddCors(o => o.AddPolicy("MYCORS", builder =>
{
    builder.AllowAnyOrigin()
           .AllowAnyMethod()
           .AllowAnyHeader();
}));

var stringConexion = builder.Configuration.GetConnectionString("MyMysqlConnection");

builder.Services.AddEntityFrameworkMySql()
    .AddDbContext<ApplicationDbContext>(options =>
    {
        options.UseMySql(stringConexion, ServerVersion.AutoDetect(stringConexion));
    });

builder.Services.AddControllers();

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();
app.UseCors("MYCORS");
app.UseAuthorization();
app.MapControllers();
app.Run();
