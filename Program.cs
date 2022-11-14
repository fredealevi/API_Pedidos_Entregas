using System.Text.Json.Serialization;
using Microsoft.EntityFrameworkCore;
using tech_test_payment_api.ContextoBanco;
using tech_test_payment_api.Models;


var builder = WebApplication.CreateBuilder(args);

builder.Services.AddDbContext<Context>(options =>
    options.UseLazyLoadingProxies().UseSqlServer(builder.Configuration.GetConnectionString("ConexaoPadrao")));

builder.Services.AddControllers().AddJsonOptions(x =>
{
    x.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});


// Add services to the container.


builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
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

app.UseAuthorization();

app.MapControllers();

app.Run();
