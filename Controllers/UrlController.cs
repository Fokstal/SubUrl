using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models;
using SubUrl.Models.DTO;

namespace SubUrl.Controllers
{
    public class UrlController : Controller
    {
        private readonly ILogger<UrlController> _logger;

        public UrlController(ILogger<UrlController> logger)
        {
            _logger = logger;
        }

        public async Task<IActionResult> Index()
        {
            using (AppDbContext db = new())
            {
                IEnumerable<Url> urlList = await db.Url.ToArrayAsync();

                return View(urlList);
            }
        }

        [HttpGet]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<ActionResult<string>> Create([FromBody] UrlDTO urlDTO)
        {
            if (!ModelState.IsValid) return BadRequest("Link has been required!");

            using (AppDbContext db = new())
            {
                Url? url = await db.Url.FirstOrDefaultAsync(urlDb => urlDb.LongValue.ToLower() == urlDTO.LongValue.ToLower());

                if (url is not null)
                {
                    db.Url.Remove(url);
                }

                string shortValue = urlDTO.LongValue.Length + new Random().Next(-10, 10) + "";

                await db.Url.AddAsync(new()
                {
                    LongValue = urlDTO.LongValue,
                    ShortValue = shortValue,
                    DateCreated = DateTime.Now,
                });

                await db.SaveChangesAsync();

                return Created("Url", shortValue);
            }
        }

        [HttpGet]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            using (AppDbContext db = new())
            {
                Url? urlToUpdate = await db.Url.FirstOrDefaultAsync(urlDb => urlDb.Id == id);

                if (urlToUpdate is null) return NotFound();

                return View(urlToUpdate);
            }
        }

        public async Task<IActionResult> Update(Url url)
        {

            if (!ModelState.IsValid) return View(url);

            using (AppDbContext db = new())
            {
                db.Url.Update(url);

                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }

        [HttpDelete]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            using (AppDbContext db = new())
            {
                Url? url = await db.Url.FirstOrDefaultAsync(urlDb => urlDb.Id == id);

                if (url is null) return NotFound();

                db.Url.Remove(url);

                await db.SaveChangesAsync();

                return RedirectToAction(nameof(Index));
            }
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}