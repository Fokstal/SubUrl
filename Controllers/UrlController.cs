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
        private readonly AppDbContext _db;
        private readonly UrlService _urlService;


        public UrlController(ILogger<UrlController> logger, AppDbContext db)
        {
            _logger = logger;
            _db = db;
            _urlService = new(_db);
        }

        [HttpGet("{shortValue?}")]
        public async Task<IActionResult> Index(string? shortValue)
        {
            if (shortValue is null) return RedirectToAction(nameof(Create));

            Url? url = await _db.Url.FirstOrDefaultAsync(urlDb => urlDb.ShortValue == shortValue);

            if (url is null) return NotFound();

            url.FollowCount++;

            await _db.SaveChangesAsync();

            return Redirect(url.LongValue);
        }

        [HttpGet("list")]
        public async Task<IActionResult> GetList()
        {
            IEnumerable<Url> urlList = await _db.Url.ToArrayAsync();

            return View(urlList);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost("create")]
        public async Task<ActionResult<string>> Create([FromBody] UrlDTO urlDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            Url? url = await _db.Url.FirstOrDefaultAsync(urlDb => urlDb.LongValue.ToLower() == urlDTO.LongValue.ToLower());

            if (url is not null)
            {
                _db.Url.Remove(url);
            }

            string shortValue = await _urlService.GenerateUniqueShortValueByLongValue(urlDTO.LongValue);

            await _db.Url.AddAsync(new()
            {
                LongValue = urlDTO.LongValue,
                ShortValue = shortValue,
                DateCreated = DateTime.Now,
            });

            await _db.SaveChangesAsync();

            return Created("Url", new
            {
                LongValue = new string[] { "https://localhost:7020/url/" + shortValue },
            });
        }

        [HttpGet("update")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Url? urlToUpdate = await _db.Url.FirstOrDefaultAsync(urlDb => urlDb.Id == id);

            if (urlToUpdate is null) return NotFound();

            return View(urlToUpdate);
        }

        [Route("update")]
        public async Task<IActionResult> Update(Url url)
        {
            if (!ModelState.IsValid) return View(url);

            Url? urlFromDb = await _db.Url.FirstOrDefaultAsync(urlDb => urlDb.Id == url.Id);

            if (urlFromDb is null) return NotFound();

            urlFromDb.LongValue = url.LongValue;
            urlFromDb.ShortValue = url.ShortValue;
            urlFromDb.DateCreated = url.DateCreated;

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(GetList));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            Url? url = await _db.Url.FirstOrDefaultAsync(urlDb => urlDb.Id == id);

            if (url is null) return NotFound();

            _db.Url.Remove(url);

            await _db.SaveChangesAsync();

            return RedirectToAction(nameof(Index));
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        [Route("error")]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}