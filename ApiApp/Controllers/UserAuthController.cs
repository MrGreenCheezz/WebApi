using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace ApiApp.Controllers
{
    public class UserAuthController : ControllerBase
    {
        [Route("[controller]/Account/Test")]
        [HttpGet]
        [Authorize]
        public string TestUser()
        {
            return User.Identity.Name;
        }
        [Route("[controller]/Account/Login")]
       [HttpPost]
       public async Task LoginUserAsync(string UserName, string UserPassword)
        {
            using(InfoContext db = new InfoContext())
            {
                var blogs = from b in db.UsersDataBase
                            where b.UserName == UserName
                            select b;
                
                if (blogs.Count() != 0)
                {
                    var userForLogin = blogs.First();
                    if (userForLogin.UserPassword != UserPassword)
                    {
                        System.Diagnostics.Debug.WriteLine("Wrong Password!");
                    }
                    else
                    {
                        //Возврат куки и тд.....
                        await Authenticate(UserName);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("No such username!");
                }
            }

        }
        [Route("[controller]/Account/Register")]
        [HttpPost]
        public void RegisterUser(string UserName, string UserPassword)
        {
            using(InfoContext db = new InfoContext())
            {
                
                var blogs = from b in db.UsersDataBase
                            where b.UserName == UserName
                            select b;
                
                if (blogs.Count() == 0)
                {
                    if (db.UsersDataBase.Count() != 0) {
                        var NewUser = new UserBaseClass();
                        NewUser.Id = db.UsersDataBase.Count() + 1;
                        NewUser.UserName = UserName;
                        NewUser.UserPassword = UserPassword;
                        db.UsersDataBase.Add(NewUser);
                    }
                    else
                    {
                        var NewUser = new UserBaseClass();
                        NewUser.Id =  1;
                        NewUser.UserName = UserName;
                        NewUser.UserPassword = UserPassword;
                        db.UsersDataBase.Add(NewUser);
                    }
                }
                else
                {
                    System.Diagnostics.Debug.WriteLine("Already registered!");
                }
                db.SaveChanges();
            }
        }
        private async Task Authenticate(string userName)
        {
            // создаем один claim
            var claims = new List<Claim>
            {
                new Claim(ClaimsIdentity.DefaultNameClaimType, userName)
            };
            // создаем объект ClaimsIdentity
            ClaimsIdentity id = new ClaimsIdentity(claims, "ApplicationCookie", ClaimsIdentity.DefaultNameClaimType, ClaimsIdentity.DefaultRoleClaimType);
            // установка аутентификационных куки
            await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(id));
        }
    }
}
