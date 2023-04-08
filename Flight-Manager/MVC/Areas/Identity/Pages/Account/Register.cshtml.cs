using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Text.Encodings.Web;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Data;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Logging;
using BusinessController;
using FluentEmail.Smtp;
using System.Net.Mail;
using FluentEmail.Core;
using Microsoft.VisualStudio.Web.CodeGeneration.Contracts.Messaging;
using System.Web.Helpers;

namespace MVC.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly SignInManager<dbUser> _signInManager;
        private readonly UserManager<dbUser> _userManager;
        private readonly ILogger<RegisterModel> _logger;
        private readonly FmDbContext _fmDbContext;

        private readonly IEmailSender emailSender;

        public RegisterModel(
            UserManager<dbUser> userManager,
            SignInManager<dbUser> signInManager,
            ILogger<RegisterModel> logger,

            FmDbContext fmdbContext)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _logger = logger;

            _fmDbContext = fmdbContext;
        }

        [BindProperty]
        public InputModel Input { get; set; }

        public string ReturnUrl { get; set; }

        public IList<AuthenticationScheme> ExternalLogins { get; set; }

        public class InputModel
        {

            [Required]
            public string Email { get; set; }
            [Required]
            public string UserName { get; set; }

            [Required]
            public string FirstName { get; set; }

            [Required]
            public string LastName { get; set; }
            [Required]
            public string Address { get; set; }

            [Required]
            [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 6)]
            [DataType(DataType.Password)]
            [Display(Name = "Password")]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Display(Name = "Confirm password")]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
            
            
            [Required]

            [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]

            public string EGN { get; set; }

            [Required]

            [DataType(DataType.PhoneNumber)]

            [StringLength(10, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 10)]

            public string PhoneNumber { get; set; }



        }

        public async Task OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList();
            if (ModelState.IsValid)
            {
                var user = new dbUser { Id = Guid.NewGuid().ToString(), UserName = Input.UserName, Email = Input.Email, FirstName = Input.FirstName, LastName = Input.LastName, PhoneNumber = Input.PhoneNumber, EGN = Input.EGN, Address = Input.Address };
                var result = await _userManager.CreateAsync(user, Input.Password);
                if (result.Succeeded)
                {
                    _logger.LogInformation("User created a new account with password.");

                    await _userManager.AddToRoleAsync(user, "User");


                    LoginAutherification.userName = Input.UserName;
                    await _signInManager.SignInAsync(user, isPersistent: false);


                    //await emailSender.SendEmailAsync(user.Email, "Welcome to KC-Airline!!!", "You are registered!!!");

                    return LocalRedirect(returnUrl);
                }
                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            // If we got this far, something failed, redisplay form
            return Page();
        }
    }
}
