using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using AdminMNS.WebApp.Data;
using AdminMNS.WebApp.Data.Entities;
using Microsoft.AspNetCore.Authorization;
using AdminMNS.WebApp.Models.ViewModel.GraduatingClass;
using AdminMNS.WebApp.App_Code.Helpers;

namespace AdminMNS.WebApp.Controllers
{
	[Authorize(Roles = Roles.Admin)]
	public class GraduatingClassController : Controller
	{
		private readonly AppDbContext _context;

		public GraduatingClassController(AppDbContext context)
		{
			_context = context;
		}

		public async Task<IActionResult> Index()
		{
			return _context.GraduatingClass != null ?
						View(await _context.GraduatingClass.Select(gc => new IndexGraduatingClassViewModel(gc)).ToListAsync()) :
						Problem("Entity set 'AppDbContext.GraduatingClass'  is null.");
		}

		public async Task<IActionResult> Details(int? id)
		{
			if (id == null || _context.GraduatingClass == null)
			{
				return NotFound();
			}

			var graduatingClass = await _context.GraduatingClass
				.FirstOrDefaultAsync(m => m.Id == id);
			if (graduatingClass == null)
			{
				return NotFound();
			}

			DetailsGraduatingClassViewModel model = new DetailsGraduatingClassViewModel(graduatingClass);

			return View(model);
		}

		public IActionResult Create()
		{
			CreateGraduatingClassViewModel model = new CreateGraduatingClassViewModel();

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create([Bind("Id,Name,StartDate,EndDate")] GraduatingClass graduatingClass)
		{
			if (ModelState.IsValid)
			{
				_context.Add(graduatingClass);
				await _context.SaveChangesAsync();
				return RedirectToAction(nameof(Index));
			}
			return View(graduatingClass);
		}

		[HttpGet]
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null || _context.GraduatingClass == null)
			{
				return NotFound();
			}

			GraduatingClass? graduatingClass = await _context.GraduatingClass.FindAsync(id);
			if (graduatingClass == null)
			{
				return NotFound();
			}

			EditGraduatingClassViewModel model = new EditGraduatingClassViewModel(graduatingClass);

			return View(model);
		}

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, GraduatingClass graduatingClass)
		{
			if (id != graduatingClass.Id)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(graduatingClass);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					return NotFound();
				}
				return RedirectToAction(nameof(Index));
			}
			return View(graduatingClass);
		}

		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null || _context.GraduatingClass == null)
			{
				return NotFound();
			}

			var graduatingClass = await _context.GraduatingClass
				.FirstOrDefaultAsync(m => m.Id == id);
			if (graduatingClass == null)
			{
				return NotFound();
			}

			DeleteGraduatingClassViewModel model = new DeleteGraduatingClassViewModel(graduatingClass);

			return View(model);
		}

		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			if (_context.GraduatingClass == null)
			{
				return Problem("Entity set 'AppDbContext.GraduatingClass'  is null.");
			}
			var graduatingClass = await _context.GraduatingClass.FindAsync(id);
			if (graduatingClass != null)
			{
				_context.GraduatingClass.Remove(graduatingClass);
			}

			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool GraduatingClassExists(int id)
		{
			return (_context.GraduatingClass?.Any(e => e.Id == id)).GetValueOrDefault();
		}
	}
}
