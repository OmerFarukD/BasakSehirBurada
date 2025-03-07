using BasakSehirBurada.Application;
using BasakSehirBurada.Application.Services.JwtServices;
using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence;
using BasakSehirBurada.Persistence.Contexts;
using BasakSehirBurada.Presentation.Middlewares;
using Microsoft.AspNetCore.Identity;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));


// 
builder.Services.AddExceptionHandler<HttpExceptionHandler>();
builder.Services.AddStackExchangeRedisCache(opt =>
{
    opt.Configuration = "localhost:6379";
    opt.InstanceName = "BASAKSEHIR_CACHE";
});



builder.Services.AddIdentity<User, IdentityRole>(opt =>
{
    opt.User.RequireUniqueEmail = true;
    opt.Password.RequireNonAlphanumeric = false;
    opt.Password.RequiredLength = 6;
}).AddEntityFrameworkStores<BaseDbContext>();


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();


app.UseExceptionHandler(_ => { });

app.MapControllers();

app.Run();
