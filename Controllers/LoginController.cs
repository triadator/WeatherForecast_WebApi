using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using WebApi.Db;

namespace WebApi.Controllers
{
    [Route("login")]
    public class LoginController : Controller

    {
        private readonly AppDbContext _context;

        public LoginController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
         public async Task LoginForm()
        {
            HttpContext.Response.ContentType = "text/html; charset=utf-8";

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
                        <input type='login' name='login' />
                    </p>
                    <p>
                        <label>Password</label><br />
                        <input type='password' name='password' />
                    </p>
                    <input type='submit' value='Login' />
                </form>
            </body>
            </html>";
            await HttpContext.Response.WriteAsync(loginForm);
        }


        [HttpPost]
        public  IActionResult Login()
        {
            
            var form = HttpContext.Request.Form;
            if (!form.ContainsKey("login") || !form.ContainsKey("password"))
                return BadRequest("Wrong data");
            

            string login = form["login"];
            string password = form["password"];

            Person person = _context.People.Where(p => p.login == login && p.password == password).FirstOrDefault();


            if (person is null) return BadRequest("Некорректные логин и пароль");

            var claims = new List<Claim> { new Claim(ClaimTypes.Name, person.login) };
            var claimsIdentity = new ClaimsIdentity(claims, "Cookies");

            HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));
            return Redirect("/api/stats/Moscow");
        }
    }
}
