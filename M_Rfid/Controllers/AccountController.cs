using System;
using System.Collections.Generic;
using System.Linq;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using System.Web;
using M_Rfid.Models;
using M_Rfid.ModelViews;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace M_Rfid.Controllers
{
    [Route("[controller]")]
    [ApiController]
    public class AccountController : ControllerBase
    {
        private readonly ApplicationDb _db;
        private readonly UserManager<ApplicationUser> _manager;
        private readonly SignInManager<ApplicationUser> _signInManager;

        public AccountController(ApplicationDb db, UserManager<ApplicationUser> manager, SignInManager<ApplicationUser> signInManager)
        {
            _db = db;
            _manager = manager;
            _signInManager = signInManager;
        }
        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register(RegisterModel model)
        {
            if (model == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                if (EmailExistes(model.Email))
                {
                    return BadRequest("Email is not available");
                }
                if (!IsEmailValid(model.Email))
                {
                    return BadRequest("Email is not valid");
                }
                if (UserNameExistes(model.UserName))
                {
                    return BadRequest("Username is used");
                }
                var user = new ApplicationUser()
                {
                    Email = model.Email,
                    UserName = model.UserName

                };
                var result = await _manager.CreateAsync(user, model.Password);
                if (result.Succeeded)
                {
                    ////http://localhost:50255/Account/RegistrationConfirm?ID=554545&Token=7767678djshdks
                    //var token = await _manager.GenerateEmailConfirmationTokenAsync(user);
                    //var confirmLink = Url.Action("RegistrationConfirm", "Account", new
                    //{ ID = user.Id, Token = HttpUtility.UrlEncode(token) }, Request.Scheme);
                    //var txt = "Please confirm your registration to our site";
                    //var link = "<a href=\"" + confirmLink + "\">Confirm regitration</a>";
                    //var title = "Registration Confirm";
                    //if (await SendGridAPI.Execute(user.Email, user.UserName, txt, link, title))
                    //{
                    return StatusCode(StatusCodes.Status200OK);
                    //}

                }
                else
                {
                    return BadRequest(result.Errors);
                }

            }
            return StatusCode(StatusCodes.Status400BadRequest);

        }

        private bool UserNameExistes(string userName)
        {
            return _db.Users.Any(x => x.UserName == userName);
        }

        private bool EmailExistes(string email)
        {
            return _db.Users.Any(x => x.Email == email);
        }
        private bool IsEmailValid(string email)
        {
            Regex em = new Regex(@"\w+@\w+.com|\w+@\w+.net");
            if (em.IsMatch(email))
            {
                return true;
            }
            return false;
        }
        [HttpGet]
        [Route("RegistrationConfirm")]
        public async Task<IActionResult> RegistrationConfirm(string ID, string Token)
        {
            if (string.IsNullOrEmpty(ID) || string.IsNullOrEmpty(Token))

                return NotFound();

            var user = await _manager.FindByIdAsync(ID);
            if (user == null)

                return NotFound();

            var result = await _manager.ConfirmEmailAsync(user, HttpUtility.UrlDecode(Token));
            if (result.Succeeded)
            {
                return Ok("Registration success");
            }
            else
            {
                return BadRequest(result.Errors);
            }
        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login(LoginModel model)
        {
            if (model == null)

                return NotFound();

            var user = await _manager.FindByEmailAsync(model.Email);
            if (user == null)
                return NotFound();

            //if (!user.EmailConfirmed)
            //{
            //    return Unauthorized("Email is not confirmed yet!!");
            //}

            var result = await _signInManager.PasswordSignInAsync(user, model.Password, model.RememberMe, false);
            if (result.Succeeded)
            {
                return Ok();
            }
            else
            {
                return BadRequest(result.IsNotAllowed);
            }
        }
        [Authorize]
        [HttpGet]
        [Route("GetAllUsers")]
        public async Task<ActionResult<IEnumerable<ApplicationUser>>> GetAllUsers()
        {
            return await _db.Users.ToListAsync();
        }
    }
}