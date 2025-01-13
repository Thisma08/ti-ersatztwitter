using Application;
using Application.UseCases.Like;
using Application.UseCases.Tweet;
using Application.UseCases.User;
using Domain.Services;
using Infrastructure;
using Infrastructure.Repositories;
/*using Infrastructure.Models;
using Infrastructure.Repositories;*/
using Microsoft.EntityFrameworkCore;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

var corsPolicyName = "AllowSpecificOrigins";
builder.Services.AddCors(options =>
{
    options.AddPolicy(name: corsPolicyName, policy =>
    {
        policy.WithOrigins("http://localhost:8080")
            .AllowAnyHeader()
            .AllowAnyMethod()
            .AllowCredentials();
    });
});

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddAutoMapper(typeof(ProfileMapper));

builder.Services.AddDbContext<AppDbContext>(cfg => 
    cfg.UseSqlServer(builder.Configuration.GetConnectionString("defaultConnection")
    ));

builder.Services.AddScoped<IUserRepository, UserRepository>();
builder.Services.AddScoped<ITweetRepository, TweetRepository>();
builder.Services.AddScoped<ILikeRepository, LikeRepository>();

builder.Services.AddScoped<UseCaseFetchAllUsers>();

builder.Services.AddScoped<UseCaseFetchAllTweets>();
builder.Services.AddScoped<UseCaseCreateTweet>();
builder.Services.AddScoped<UseCaseDeleteTweet>();

builder.Services.AddScoped<UseCaseFetchByUserAndTweet>();
builder.Services.AddScoped<UseCaseCreateLike>();
builder.Services.AddScoped<UseCaseDeleteLike>();
builder.Services.AddScoped<UseCaseCountLikes>();

builder.Services.AddScoped<TweetService>();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseRouting();

app.UseCors(corsPolicyName);

app.UseResponseCaching();

app.UseStaticFiles();

app.UseAuthorization();

app.MapControllers();

app.Run();