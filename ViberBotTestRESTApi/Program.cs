using Microsoft.EntityFrameworkCore;
using Viber.Bot.NetCore.Middleware;
using ViberWalkBot;
using ViberWalkBot.Context;
using ViberWalkBot.Repositories;
using ViberWalkBot.Repositories.Interface;
using ViberWalkBot.Services;
using ViberWalkBot.Services.Interface;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddViberBotApi(opt =>
{
    opt.Token = SettingsBot.Token;
    opt.Webhook = SettingsBot.Webhook;
});

builder.Services.AddDbContext<AppDbContext>(options => options.UseSqlServer("Server=Artiline\\MSSQL;Database=Track;Trusted_Connection=True;TrustServerCertificate=True;").LogTo(Console.WriteLine));

//Repositorie
builder.Services.AddScoped<ITrackLocationRepository, TrackLocationRepository>();
builder.Services.AddScoped<IWalkRepository, WalkRepository>();

//Services
builder.Services.AddScoped<ITrackLocationService, TrackLocationService>();
builder.Services.AddScoped<IWalkService, WalkService>();
builder.Services.AddScoped<IViberService, ViberService>();



var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}



app.UseHttpsRedirection();


app.MapControllers();

app.Run();
