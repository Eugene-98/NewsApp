
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.ViewModels;

namespace NewsApp.Controllers
{
	public class AdminController : Controller
	{
		private readonly Context _context;
		IWebHostEnvironment _appEnvironment;

		public AdminController(Context context, IWebHostEnvironment appEnvironment)
		{
			_context = context;
			_appEnvironment = appEnvironment;
		}
		[Authorize(Roles = "admin")]
		public async Task<IActionResult> Index()
		{
			return View(await _context.News.ToListAsync());
		}

		// GET: News/Details/5
		public async Task<IActionResult> Details(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var news = await _context.News
				.FirstOrDefaultAsync(m => m.NewsId == id);
			if (news == null)
			{
				return NotFound();
			}

			return View(news);
		}

		// GET: News/Create
		public IActionResult Create()
		{
			return View();
		}

		// POST: News/Create

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Create(NewsViewModel model, IFormFile uploadedFile)
		{
			if (ModelState.IsValid)
			{
				News news = new News
				{
					NewsName = model.NewsName,
					NewsHeader = model.NewsHeader,
					NewsSubtitle = model.NewsSubtitle,
					NewsText = model.NewsText,
				};
				if (uploadedFile != null)
				{
					string path = "Files/" + uploadedFile.FileName;

					using (var fileStream = new FileStream(path, FileMode.Create))
					{
						await uploadedFile.CopyToAsync(fileStream);
					}

					news.NewsImageName = uploadedFile.FileName;
					news.NewsImagePath = path;
				}

				_context.News.Add(news);
				await _context.SaveChangesAsync();

				return RedirectToAction(nameof(Index));
			}
			else
			{
				ModelState.AddModelError("", "Invalid form");
			}

			return View(model);
		}

		// GET: News/Edit/5
		public async Task<IActionResult> Edit(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var news = await _context.News.FindAsync(id);
			if (news == null)
			{
				return NotFound();
			}
			return View(news);
		}

		// POST: News/Edit/5

		[HttpPost]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> Edit(int id, News news)
		{
			if (id != news.NewsId)
			{
				return NotFound();
			}

			if (ModelState.IsValid)
			{
				try
				{
					_context.Update(news);
					await _context.SaveChangesAsync();
				}
				catch (DbUpdateConcurrencyException)
				{
					if (!NewsExists(news.NewsId))
					{
						return NotFound();
					}
					else
					{
						throw;
					}
				}
				return RedirectToAction(nameof(Index));
			}
			return View(news);
		}

		// GET: News/Delete/5
		public async Task<IActionResult> Delete(int? id)
		{
			if (id == null)
			{
				return NotFound();
			}

			var news = await _context.News
				.FirstOrDefaultAsync(m => m.NewsId == id);
			if (news == null)
			{
				return NotFound();
			}

			return View(news);
		}

		// POST: News/Delete/5
		[HttpPost, ActionName("Delete")]
		[ValidateAntiForgeryToken]
		public async Task<IActionResult> DeleteConfirmed(int id)
		{
			var news = await _context.News.FindAsync(id);
			_context.News.Remove(news);
			await _context.SaveChangesAsync();
			return RedirectToAction(nameof(Index));
		}

		private bool NewsExists(int id)
		{
			return _context.News.Any(e => e.NewsId == id);
		}
	}
}