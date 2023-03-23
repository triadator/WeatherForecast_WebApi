using Microsoft.AspNetCore.Authentication.Cookies;
using Npgsql.EntityFrameworkCore.PostgreSQL;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using System.Net;
using Web_Api.Interfaces;
using Web_Api.OpenMeteo;
using WebApi;
using Microsoft.AspNetCore.Authentication;
using System.Security.Claims;
using System;

List<Person> people = new List<Person> { new Person { Id = 1, login = "admin", password = "12341234" } };
var builder = WebApplication.CreateBuilder(args);

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
	.AddCookie(options =>
	{
		options.LoginPath = "/login";
		//options.Cookie.Name = "AuthCookie";
	});
builder.Services.AddAuthorization();

builder.Services.AddScoped<IForecastProvider, OpenMeteoClient>();
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();
//builder.Services.AddDbContext<AppDbContext>(options =>
//options.UseNpgsql(builder.Configuration.GetConnectionString("sfasf")));


var app = builder.Build();

app.UseAuthentication();
app.UseAuthorization();
app.MapGet("/login", async (HttpContext context) =>
{
	context.Response.ContentType = "text/html; charset=utf-8";
	
	string loginForm = @"<!DOCTYPE html>
    <html>
    <head>
        <meta charset='utf-8' />
        <title>Authentication</title>
    </head>
    <body>
        <h2>Login Form</h2>
        <form method='post'>
            <p>
                <label>Login</label><br />
                <input type='login name='login' />
            </p>
            <p>
                <label>Password</label><br />
                <input type='password' name='password' />
            </p>
            <input type='submit' value='Login' />
        </form>
    </body>
    </html>";
	await context.Response.WriteAsync(loginForm);
});

app.MapPost("/login", async (string? returnUrl, HttpContext context) =>
{
	// получаем из формы email и пароль
	var form = context.Request.Form;
	// если email и/или пароль не установлены, посылаем статусный код ошибки 400
	if (!form.ContainsKey("login") || !form.ContainsKey("password"))
		return Results.BadRequest("Login и/или пароль не установлены");

	string login = form["login"];
	string password = form["password"];

	// находим пользователя 
	Person? person = people.FirstOrDefault(p => p.login == login && p.password == password);
	// если пользователь не найден, отправляем статусный код 401
	if (person is null) return Results.Unauthorized();

	var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.login) };
	// создаем объект ClaimsIdentity
	ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, "Cookies");
	// установка аутентификационных куки
	await context.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
	return Results.Redirect(returnUrl ?? "/api/Moscow");
});

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}
app.UseCookiePolicy();
app.MapControllers();

app.Run();
