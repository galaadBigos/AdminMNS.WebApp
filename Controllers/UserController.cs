using AdminMNS.WebApp.App_Code.Helpers;
using AdminMNS.WebApp.Data;
using AdminMNS.WebApp.Data.Entities;
using AdminMNS.WebApp.Models.ViewModel.User;
using AdminMNS.WebApp.Services.Abstractions;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace AdminMNS.WebApp.Controllers
{
	[Authorize(Roles = $"{Roles.Admin}, {Roles.Personnel}")]
	public class UserController : Controller
	{
		private readonly AppDbContext _context;
		private readonly UserManager<User> _userManager;
		private readonly RoleManager<IdentityRole> _roleManager;
		private readonly IEmailSender _emailSender;
		private readonly IUserService _userService;

		public UserController(AppDbContext context, UserManager<User> userManager, RoleManager<IdentityRole> roleManager, IEmailSender emailSender, IUserService userService)
		{
			_context = context;
			_userManager = userManager;
			_roleManager = roleManager;
			_emailSender = emailSender;
			_userService = userService;
		}

		[HttpGet]
		public async Task<IActionResult> Index(string message)
		{
			List<IndexUserViewModel> models = await _userService.CreateIndexUserViewModels();
			ViewBag.Message = message;

			return View(models);
		}

		[HttpGet]
		public IActionResult Create()
		{
			CreateUserViewModel model = new CreateUserViewModel();

			return View(model);
		}

		[HttpPost]
		[AutoValidateAntiforgeryToken]
		public async Task<IActionResult> Create(CreateUserViewModel model)
		{
			if (ModelState.IsValid)
			{
				if (model.Password == model.ConfirmPassword && model.Password != null)
				{
					User user = new User(model);

					IdentityResult isUserInDatabase = await _userService.AddUserInDatabase(model, user);

					if (isUserInDatabase.Succeeded && model.Email != null)
					{
						await _emailSender.SendCreateUserConfirmationEmail(user, model, this);

						return RedirectToAction("Index", "User", new { message = "Un email de validation a été envoyé au nouvel utilisateur" });
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
		public async Task<IActionResult> Edit(string? id)
		{
			if (id == null || _context.Users == null)
			{
				return NotFound();
			}

			User? user = await _context.Users.FindAsync(id);
			if (user == null)
			{
				return NotFound();
			}

			EditUserViewModel model = new EditUserViewModel(user);
			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(string id, EditUserViewModel model)
		{
			if (id != model.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					IdentityResult updateResult = await _userService.UpdateUser(id, model);
				}
				catch (DbUpdateConcurrencyException)
				{
					return NotFound();
				}

				return RedirectToAction("Index");
			}
			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Details(string? id)
		{
			if (id == null || _userManager.Users == null)
			{
				return NotFound();
			}

			User? user = await _userManager.Users.Where(u => u.Id == id).Include(u => u.GraduatingClass).FirstAsync();

			if (user == null)
			{
				return NotFound();
			}

			IList<string> roleNames = await _userManager.GetRolesAsync(user);
			DetailsUserViewModel model = new DetailsUserViewModel(user, roleNames);

			return View(model);
		}

		[HttpGet]
		public async Task<IActionResult> Delete(string? id)
		{
			if (id == null || _userManager.Users == null)
			{
				return NotFound();
			}

			User? user = await _userManager.FindByIdAsync(id);

			if (user == null)
			{
				return NotFound();
			}
			DeleteUserViewModel model = new DeleteUserViewModel(user);

			return View(model);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(string id)
		{
			if (_userManager.Users == null)
			{
				return Problem("Entity set 'AppDbContext.AspNetUsers'  is null.");
			}

			User? user = await _userManager.FindByIdAsync(id);
			if (user != null)
			{
				await _userManager.DeleteAsync(user);
			}

			return RedirectToAction("Index");
		}
	}
}
