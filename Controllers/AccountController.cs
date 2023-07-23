using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Models.ViewModel.Account;
using AdminMNS.WebApp.Services.Abstractions;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Web;

namespace AdminMNS.WebApp.Controllers
{
	public class AccountController : Controller
	{
		private readonly UserManager<User> _userManager;
		private readonly SignInManager<User> _signInManager;
		private readonly IEmailSender _emailSender;
		private readonly IRoleService _roleHelper;

		public AccountController(UserManager<User> userManager, SignInManager<User> signInManager, IEmailSender emailSender, IRoleService roleHelper)
		{
			_userManager = userManager;
			_signInManager = signInManager;
			_emailSender = emailSender;
			_roleHelper = roleHelper;
		}

		[HttpGet]
		public IActionResult Register()
		{
			RegisterViewModel model = new RegisterViewModel();
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Register(RegisterViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Password == model.ConfirmPassword)
				{
					User user = new User(model);

					IdentityResult result = await _userManager.CreateAsync(user, model.Password);
					await _userManager.AddToRoleAsync(user, "User");
					if (result.Succeeded)
					{
						await _emailSender.SendRegisterUserConfirmationEmail(user, model, this);

						return RedirectToAction("Index", "Home", new { message = "Un email de validation vous a été envoyé" });
					}
					else
					{
						foreach (IdentityError error in result.Errors)
						{
							ModelState.AddModelError(error.Code, error.Description);
						}
					}
				}
				else
				{
					ModelState.AddModelError("Password", "Le mot de passe et la confirmation ne correspondent pas");
				}
			}
			model.Password = "";
			model.ConfirmPassword = "";
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> ConfirmEmail(string email, string code)
		{
			User? user = await _userManager.FindByEmailAsync(email);
			if (user?.EmailConfirmed == false)
			{
				code = HttpUtility.UrlDecode(code);

				IdentityResult confirmationResult = await _userManager.ConfirmEmailAsync(user, code);
				if (confirmationResult.Succeeded)
				{
					await _signInManager.SignInAsync(user, isPersistent: false);
				}
			}
			return RedirectToAction("Index", "Home");
		}

		[HttpGet]
		public IActionResult Login()
		{
			LoginViewModel model = new LoginViewModel();
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Login(LoginViewModel model)
		{
			if (ModelState.IsValid)
			{
				User? user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					if (user.EmailConfirmed)
					{
						Microsoft.AspNetCore.Identity.SignInResult loginResult = await _signInManager.PasswordSignInAsync
							(model.Email, model.Password, model.RememberMe, true);
						if (loginResult.Succeeded)
						{
							if (await _roleHelper.IsAuthorizedToAdministration(user))
							{
								return RedirectToAction("Index", "Administration");
							}
							else
							{
								return RedirectToAction("Index", "Home");
							}
						}
						else
						{
							ModelState.AddModelError("incorrect_login", "Email ou mot de passe incorrect");
						}
						if (loginResult.IsLockedOut)
						{
							return View("Lockout");
						}
					}
					else
					{
						ModelState.AddModelError("email_confirmation", "Vous devez d'abord confirmer votre email");
					}
				}
				else
				{
					ModelState.AddModelError("incorrect_login", "Email ou mot de passe incorrect");
				}
			}
			model.Password = "";
			return View(model);
		}

		[HttpGet]
		public IActionResult ForgetPassword()
		{
			return View();
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> ForgetPassword(ForgetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				User? user = await _userManager.FindByEmailAsync(model.Email);
				if (user != null)
				{
					string code = await _userManager.GeneratePasswordResetTokenAsync(user);
					var routeValues = new { email = model.Email, code = HttpUtility.UrlEncode(code) };
					string? resetUrl = Url.Action("ResetPassword", "Account", routeValues, Request.Scheme);

					await _emailSender.SendMail(model.Email, "Réinitialisation de votre mot de passe", "Cliquez sur le lien pour réinitialiser votre mot de passe : " + resetUrl);

				}
				return View("ForgetPasswordConfirmation");
			}
			return View(model);
		}

		[HttpGet]
		public IActionResult ResetPassword(string email, string code)
		{
			ResetPasswordViewModel viewModel = new ResetPasswordViewModel { Email = email, Code = code };
			return View(viewModel);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> ResetPassword(ResetPasswordViewModel model)
		{
			if (ModelState.IsValid)
			{
				User? user = await _userManager.FindByEmailAsync(model.Email);
				if (user is null)
				{
					return View("ResetPasswordConfirmation");
				}
				if (model.Password == model.ConfirmPassword)
				{
					string code = HttpUtility.UrlDecode(model.Code);

					IdentityResult resetResult = await _userManager.ResetPasswordAsync(user, code, model.Password);
					if (resetResult.Succeeded)
					{
						return View("ResetPasswordConfirmation");
					}
				}
				else
				{
					ModelState.AddModelError("Password", "Le mot de passe et la confirmation ne correspondent pas");
				}
			}
			return View(model);
		}

		[HttpPost, ValidateAntiForgeryToken]
		public async Task<IActionResult> Logout()
		{
			await _signInManager.SignOutAsync();
			return RedirectToAction("Index", "Home");
		}
	}
}
