using System.Collections.Generic;
using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using NewsApp.Data;
using NewsApp.Models;

namespace NewsApp.Controllers
{

	[ApiController]
	[Route("api/[controller]")]
	public class HomeController : ControllerBase
	{
		private readonly Context _context;
		public HomeController(Context context)
		{
			_context = context;
		}
		[HttpGet]
		public JsonResult Get()
		{
			var latest = _context.News
				.Where(m => m.NewsId != 0)
				.OrderByDescending(m => m.NewsId)
				.Take(3);

			
			return new JsonResult(latest);
		}
	}
}
