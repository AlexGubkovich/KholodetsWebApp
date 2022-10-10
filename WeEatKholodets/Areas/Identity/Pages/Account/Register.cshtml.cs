using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.UI.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;
using System.Text.Encodings.Web;
using WeEatKholodets.Models;

namespace WeEatKholodets.Areas.Identity.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserManager<User> userManager;
        private readonly SignInManager<User> signInManager;
        private readonly IEmailSender emailSender;

        public RegisterModel(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender)
        {
            this.userManager = userManager;
            this.signInManager = signInManager;
            this.emailSender = emailSender;
        }


        public async Task OnGetAsync(string? returnUrl = null)
        {
            ReturnUrl ??= Url.Content("~/");

            await HttpContext.SignOutAsync(IdentityConstants.ExternalScheme);

            ExternalLogins = (await signInManager.GetExternalAuthenticationSchemesAsync()).ToList();

            ReturnUrl = returnUrl;
        }

        #region snippet
        public async Task<IActionResult> OnPostAsync(string? returnUrl = null)
        {
            returnUrl = returnUrl ?? Url.Content("~/");
            if (ModelState.IsValid)
            {
                var user = new User { UserName = Input?.Email, Email = Input?.Email };
                var result = await userManager.CreateAsync(user, Input?.Password);
                if (result.Succeeded)
                {

                    var code = await userManager.GenerateEmailConfirmationTokenAsync(user);
                    var callbackUrl = Url.Page(
                        "/Account/ConfirmEmail",
                        pageHandler: null,
                        values: new { userId = user.Id, code = code },
                    protocol: Request.Scheme);

                    await emailSender.SendEmailAsync(Input?.Email, "Confirm your email",
                        $"Please confirm your account by <a href='{HtmlEncoder.Default.Encode(callbackUrl!)}'>clicking here</a>.");

                    await signInManager.SignInAsync(user, isPersistent: false);
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
        #endregion

        [BindProperty]
        public InputForRegister Input { get; set; } = null!;
        public List<AuthenticationScheme>? ExternalLogins { get; private set; }
        public string? ReturnUrl { get; set; }

        public class InputForRegister
        {
            [Required]
            [Display(Name = "Email")]
            public string Email { get; set; } = null!;

            [Required]
            [DataType(DataType.Password)]
            [Display(Name = "Пароль")]
            public string Password { get; set; } = null!;

            [Required]
            [Compare("Password", ErrorMessage = "Пароли не совпадают")]
            [DataType(DataType.Password)]
            [Display(Name = "Подтвердить пароль")]
            public string ConfirmPassword { get; set; } = null!;
        }
    }


}
