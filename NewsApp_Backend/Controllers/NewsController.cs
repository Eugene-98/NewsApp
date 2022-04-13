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

        [HttpGet("{id}")]
        public JsonResult Details(int id)
		{
            News news = _context.News.FirstOrDefault(n => n.NewsId == id);
            return new JsonResult(news);
		}

        // POST: News/Create

        [HttpPost]
        [ValidateAntiForgeryToken]
        public JsonResult Post(NewsViewModel model, IFormFile uploadedFile)
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
                news.NewsImagePath = path;
            }
            else
            {
                return new JsonResult("Error");
            }
            _context.News.Add(news);
            _context.SaveChanges();

            return new JsonResult("Added Successfully");
        }

        [HttpPut("{id}")]
        public JsonResult Put(int id,  News news)
        {
            if (id != news.NewsId)
            {
                return new JsonResult("Error");
            }
            try
            {
                _context.Update(news);
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {

            }
            return new JsonResult("Updated Successfully");
        }

        // GET: News/Delete/5
        [HttpDelete("{id}")]
        public JsonResult Delete(int? id)
        {

            var news =  _context.News.FirstOrDefault(m => m.NewsId == id);
            _context.News.Remove(news);
            return new JsonResult("Deleted Successfully");

        }
    }
}
