using abc.core.Common;
using abc.Infrastructure;
using MassTransit;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.Identity.Web;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddMicrosoftIdentityWebApi(builder.Configuration.GetSection("AzureAd"));

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
builder.Services.AddMediator(cfg =>
{
    cfg.AddConsumers(AppDomain.CurrentDomain.Load("abc.core"));
});
builder.Services
   .AddDbContext<AppDbContext>(opts =>
   {
       opts
           .UseNpgsql(
               builder.Configuration.GetConnectionString("DefaultConnection"),
               optionsBuilder =>
               {
                   optionsBuilder.UseNodaTime();
               })
           .EnableSensitiveDataLogging();
   })
   .AddScoped<IUnitOfWork, UnitOfWork<AppDbContext>>();
var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
