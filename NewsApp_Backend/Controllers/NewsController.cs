using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NewsApp.Data;
using NewsApp.Models;
using NewsApp.ViewModels;

namespace NewsApp.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class NewsController : ControllerBase
    {
        private readonly Context _context;
        IWebHostEnvironment _appEnvironment;

        public NewsController(Context context, IWebHostEnvironment appEnvironment)
        {
            _context = context;
            _appEnvironment = appEnvironment;
        }

        // GET: News
       [HttpGet] 
        public JsonResult Get()
        {
            return new JsonResult(_context.News);
        }

        // POST: News/Create
        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Post(NewsViewModel model, IFormFile uploadedFile)
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
		            string path = "/Files/" + uploadedFile.FileName;

		            using (var fileStream = new FileStream(_appEnvironment.WebRootPath + path, FileMode.Create))
		            {
			            uploadedFile.CopyTo(fileStream);
                    }
 
		            news.NewsImageName = uploadedFile.FileName;
                    news.NewsImagePath = "https://localhost:7245" + path;
	            }

                 _context.News.Add(news);
                 _context.SaveChanges();

                return new JsonResult(news);
            }
            else
            {
	            ModelState.AddModelError("", "Invalid form");
            }

            return new JsonResult(model);
        }
        // POST: News/Edit/5

        [HttpPut("{id}")]
        public void Put(int id,  News news)
        {
            if (id != news.NewsId)
            {
                
            }

            if (ModelState.IsValid)
            {
                try
                {
                     _context.Update(news);
                     _context.SaveChanges();
                }
                catch (DbUpdateConcurrencyException)
                {
                   
                }
            }
        }

        // GET: News/Delete/5
        [HttpDelete("{id}")]
        public void Delete(int? id)
        {
            if (id == null)
            { 
            }

            var news =  _context.News.FirstOrDefault(m => m.NewsId == id);
            if (news == null)
            {
            }

           
        }
    }
}
