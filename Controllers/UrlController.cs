using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using SubUrl.Data;
using SubUrl.Models;

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
    }
}