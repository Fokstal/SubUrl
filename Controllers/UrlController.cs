using System.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models;
using SubUrl.Models.DTO;
using SubUrl.Service;

namespace SubUrl.Controllers
{
    [Route("url")]
    public class UrlController : Controller
    {
        private readonly ILogger<UrlController> _logger;

        public UrlController(ILogger<UrlController> logger)
        {
            _logger = logger;
        }

        [HttpGet("{shortValue?}")]
        public async Task<IActionResult> Index(string? shortValue)
        {
            if (shortValue is null) return RedirectToAction(nameof(Create));

            using (AppDbContext db = new())
            {
                Url? url = await db.Url.FirstOrDefaultAsync(urlDb => urlDb.ShortValue == shortValue);

                if (url is null) return NotFound();

                url.FollowCount++;

                await db.SaveChangesAsync();

                return Redirect(url.LongValue);
            }
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            using (AppDbContext db = new())
            {
                IEnumerable<Url> urlList = await db.Url.ToArrayAsync();

                return View(urlList);
            }
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
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

                string shortValue = await UrlService.GenerateUniqueShortValueByLongValue(urlDTO.LongValue);

                await db.Url.AddAsync(new()
                {
                    LongValue = urlDTO.LongValue,
                    ShortValue = shortValue,
                    DateCreated = DateTime.Now,
                });

                await db.SaveChangesAsync();

                return Created("Url", "https://localhost:7020/url/" + shortValue);
            }
        }

        [HttpGet("update")]
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

        [Route("update")]
        public async Task<IActionResult> Update(Url url)
        {
            if (!ModelState.IsValid) return View(url);

            using (AppDbContext db = new())
            {
                Url? urlFromDb = await db.Url.FirstOrDefaultAsync(urlDb => urlDb.Id == url.Id);

                if (urlFromDb is null) return NotFound();

                urlFromDb.LongValue = url.LongValue;
                urlFromDb.ShortValue = url.ShortValue;
                urlFromDb.DateCreated = url.DateCreated;

                await db.SaveChangesAsync();

                return RedirectToAction(nameof(GetList));
            }
        }

        [HttpDelete("delete")]
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
        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}