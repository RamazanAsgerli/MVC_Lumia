using Core.Models;
using Data.DTOs.AccountDTO;
using MessagePack.Formatters;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

namespace Exam_MVC.Controllers
{
    public class AccountController : Controller
    {
        private readonly UserManager<User> _userManager;
        private readonly SignInManager<User> _signInManager;
        private readonly RoleManager<IdentityRole> _roleManager;

        public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, RoleManager<IdentityRole> roleManager )
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _roleManager = roleManager;
        }

        public IActionResult Index()
        {

            return View();
        }

        public async Task<IActionResult> Register(RegisterDto registerDto)
        {
            if (!ModelState.IsValid) return View();

            User user = new User()
            {
                Name = registerDto.Name,
                Surname = registerDto.Surname,
                Email = registerDto.Email,
                UserName = registerDto.Username

            };
            var result = await _userManager.CreateAsync(user, registerDto.Password);
            if (!result.Succeeded)
            {
                foreach (var item in result.Errors)
                {
                    ModelState.AddModelError("", item.Description);
                }
                return View();
            }
            await _signInManager.SignInAsync(user, false);
            return RedirectToAction("Index", "Home");
        }

        public async Task<IActionResult> LogOut()
        {
            await _signInManager.SignOutAsync();
            return RedirectToAction("Index", "Home");
        }
        public async Task<IActionResult> Login(LoginDto loginDto)
        {
            if (!ModelState.IsValid) return View();
            var user = await _userManager.FindByNameAsync(loginDto.UserName);
            if (user == null)
            {
                ModelState.AddModelError("", "Username adi yanlisdir ve ya yoxdur");
                return View();
            }
            var result = await _signInManager.PasswordSignInAsync(user, loginDto.Password, loginDto.IsRemember, false);

            return RedirectToAction("index", "Home");
        }

        //public async Task<IActionResult> CreateRole()
        //{
        //    IdentityRole role = new IdentityRole("SuperAdmin");
        //    IdentityRole role2 = new IdentityRole("Admin");
        //    IdentityRole role3 = new IdentityRole("Member");

        //    await _roleManager.CreateAsync(role);
        //    await _roleManager.CreateAsync(role2);

        //    await _roleManager.CreateAsync(role3);

        //    return Ok("Rollarrr Yarandddiiiiiiiiii!!!!");


        //}

        //public async Task<IActionResult> AddRole()
        //{
        //    var role = await _userManager.FindByNameAsync("ramazaan");
        //    await _userManager.AddToRoleAsync(role, "SuperAdmin");
        //    return Ok("roll verdukkk!!!!!!!!");
        //}

        public async Task<IActionResult> AddRole()
        {
            var role = await _userManager.FindByNameAsync("ramazann");
            await _userManager.AddToRoleAsync(role, "SuperAdmin");
            return Ok("rolll verildiii!!!!!!!");
        }

    }
}
