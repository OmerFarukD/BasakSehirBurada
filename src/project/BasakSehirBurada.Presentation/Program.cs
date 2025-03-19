using BasakSehirBurada.Application;
using BasakSehirBurada.Application.Services.JwtServices;
using BasakSehirBurada.Domain.Entities;
using BasakSehirBurada.Persistence;
using BasakSehirBurada.Persistence.Contexts;
using BasakSehirBurada.Presentation.Middlewares;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.Text;

var builder = WebApplication.CreateBuilder(args);




// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddApplicationServices();
builder.Services.AddPersistenceServices(builder.Configuration);
builder.Services.Configure<CustomTokenOptions>(builder.Configuration.GetSection("TokenOptions"));

builder.Services.AddCors(options =>
{
    options.AddPolicy("AllowAll",
        policy => policy.AllowAnyOrigin()
                        .AllowAnyMethod()
                        .AllowAnyHeader());
});


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


var tokenOption = builder.Configuration.GetSection("TokenOptions").Get<CustomTokenOptions>();
builder.Services.AddAuthentication(opt =>
{
    opt.DefaultAuthenticateScheme = JwtBearerDefaults.AuthenticationScheme;
    opt.DefaultChallengeScheme = JwtBearerDefaults.AuthenticationScheme;
}).AddJwtBearer(JwtBearerDefaults.AuthenticationScheme, opt =>
{
    opt.TokenValidationParameters = new Microsoft.IdentityModel.Tokens.TokenValidationParameters()
    {
        ValidIssuer = tokenOption.Issuer,
        ValidAudience = tokenOption.Audience[0],
        ValidateIssuerSigningKey = true,
        ValidateIssuer = true,
        ValidateLifetime = true,
        ClockSkew = TimeSpan.Zero,
        IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(tokenOption.SecurityKey))
    };
});


var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseAuthentication(); // kÝMLÝK DOÐRULA

app.UseAuthorization(); // YETKÝLENDÝR


app.UseExceptionHandler(_ => { });

app.MapControllers();


app.Use(async (context, next) =>
{
    if (context.Request.Headers.ContainsKey("Authorization"))
    {
        Console.WriteLine("Authorization Header: " + context.Request.Headers["Authorization"]);
    }
    else
    {
        Console.WriteLine("Authorization Header Missing!");
    }
    await next();
});

app.Run();
