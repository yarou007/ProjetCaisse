using System.Security.Claims;
using CustomAth.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using Microsoft.AspNetCore.Identity; // Make sure to import this

namespace CustomAth.Controllers;

public class AccountController : Controller
{
    private readonly AppDbContext _context;

    public AccountController(AppDbContext appDbContext)
    {
        _context = appDbContext;
    }
    
    
    [Authorize(Roles = "Admin")]
    public IActionResult Index()
    {
        return View(_context.UserAccounts.Include(U =>U.Role).ToList());
    }

    public IActionResult Registration()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Registration(RegistrationViewModel registrationViewModel)
    {
        if (ModelState.IsValid)
        {
            UserAccount userAccount = new UserAccount();
            userAccount.FirstName = registrationViewModel.FirstName;
            userAccount.LastName = registrationViewModel.LastName;
            userAccount.Email = registrationViewModel.Email;
            userAccount.UserName = registrationViewModel.UserName;
            var hasher = new PasswordHasher<UserAccount>();
            userAccount.Password = hasher.HashPassword(userAccount,  registrationViewModel.Password);
            userAccount.RoleId = _context.Roles.FirstOrDefault(r => r.Name == "User").Id;
            try
            {
                _context.UserAccounts.Add(userAccount);
                _context.SaveChanges();
                ModelState.Clear(); 
                ViewBag.Message = $"{userAccount.FirstName} {userAccount.LastName} Registred Successfuly, Please Log In."; 
            }catch(DbUpdateException ex )
            {
                ModelState.AddModelError("","Please enter unique email");
                return View(registrationViewModel);
            }

        return View();
        }
        return View(registrationViewModel);
    }


    public IActionResult Login()
    {
        return View();
    }
    
    [HttpPost]
    public IActionResult Login(LoginViewModel loginViewModel)
    {
        if (ModelState.IsValid)
        {
            var userAccount = _context.UserAccounts
                .Include(x => x.Role)
                .Where(x => x.UserName.Equals( loginViewModel.UserNameOrEmail) || 
                            x.Email.Equals(loginViewModel.UserNameOrEmail) ).FirstOrDefault();
            if (userAccount != null)
            {

                var hasher = new PasswordHasher<UserAccount>();
                var result = hasher.VerifyHashedPassword(userAccount, userAccount.Password, loginViewModel.Password);
                if (result == PasswordVerificationResult.Success)
                {

                    // mrigla behs ncreatiw cookie
                    var claims = new List<Claim>
                    {
                        new Claim(ClaimTypes.Name, userAccount.FirstName),
                        new Claim("Name", userAccount.FirstName),
                        new Claim(ClaimTypes.Role, userAccount.Role.Name) // Ajouter le rôle
                    };
                    var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
                    userAccount.timeStamp = DateTime.Now;
                    _context.SaveChanges();
                    HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme,
                        new ClaimsPrincipal(claimsIdentity));
                    return RedirectToAction("SecurePage");
                }
                else {       
                    ModelState.AddModelError("", "Username/Email or password not correct");
                }
            }
            else
            {
                ModelState.AddModelError("","Username/Email or password not correct");
            }
        }
        
        return View(loginViewModel);
    }

    public IActionResult logout()
    {
        HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        return RedirectToAction("Login");
    }

    [Authorize]
    public IActionResult SecurePage()
    {
        ViewBag.Name = HttpContext.User.Identity.Name;
        return View();
    }
    
    public IActionResult AccessDenied()
    {
        return View();
    }
}