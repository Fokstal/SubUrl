using System.Diagnostics;
using MediatR;
using Microsoft.AspNetCore.Mvc;
using SubUrl.Data;
using SubUrl.Models;
using SubUrl.Models.DTO;
using SubUrl.Models.Entities;
using SubUrl.MediatR.Commands;
using SubUrl.MediatR.Queries;
using SubUrl.Service;

namespace SubUrl.Controllers
{
    public class UrlController : Controller
    {
        private readonly ILogger<UrlController> _logger;
        private readonly UrlService _urlService;
        private readonly ISender _sender;

        public UrlController(ILogger<UrlController> logger, AppDbContext db, ISender sender)
        {
            _logger = logger;
            _urlService = new(db);
            _sender = sender;
        }

        [HttpGet("{shortValue?}")]
        public async Task<IActionResult> Index(string? shortValue)
        {
            if (shortValue is null) return RedirectToAction(nameof(Create));

            UrlEntity? url = await _sender.Send(new GetUrlByShortValueQuery(shortValue));

            if (url is null) return NotFound();

            await _sender.Send(new IncUrlFollowCountCommand(url));

            return Redirect(url.LongValue);
        }

        [HttpGet("list")]
        public async Task<ActionResult<IEnumerable<UrlEntity>>> GetList()
        {
            IEnumerable<UrlEntity> urlList = await _sender.Send(new GetUrlListQuery());

            return View(urlList);
        }

        [HttpGet("{id:int}")]
        public async Task<ActionResult<UrlEntity>> Get(int id)
        {
            if (id < 1) return BadRequest();

            UrlEntity? url = await _sender.Send(new GetUrlByIdQuery(id));

            if (url is null) return NotFound();

            return Ok(url);
        }

        [HttpGet("create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost(Name = "Create")]
        public async Task<ActionResult<string>> Create([FromBody] UrlCreateDTO urlDTO)
        {
            if (!ModelState.IsValid) return BadRequest(ModelState);

            string shortValue = await _urlService.GenerateUniqueShortValueByLongValue(urlDTO.LongValue);

            UrlEntity urlToAdd = await _sender.Send(new CreateUrlCommand(urlDTO.LongValue, shortValue));

            await _sender.Send(new AddUrlCommand(urlToAdd));

            return Created("Url", new
            {
                ShortValueLink = "https://localhost:7020/" + shortValue,
            });
        }

        [HttpGet("update")]
        public async Task<IActionResult> Update(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            UrlEntity? urlToUpdate = await _sender.Send(new GetUrlByIdQuery(Convert.ToInt32(id)));

            if (urlToUpdate is null) return NotFound();

            UrlDTO urlDTO = new()
            {
                Id = urlToUpdate.Id,
                ShortValue = urlToUpdate.ShortValue,
                LongValue = urlToUpdate.LongValue,
                DateCreated = urlToUpdate.DateCreated,
            };

            return View(urlDTO);
        }

        [Route("update")]
        public async Task<IActionResult> Update(UrlDTO urlDTO)
        {
            if (!ModelState.IsValid) return View(urlDTO);

            UrlEntity? urlToUpdate = await _sender.Send(new GetUrlByIdQuery(Convert.ToInt32(urlDTO.Id)));

            if (urlToUpdate is null) return NotFound();

            await _sender.Send(new UpdateUrlCommand(urlToUpdate, urlDTO));

            return RedirectToAction(nameof(GetList));
        }

        [HttpDelete("delete")]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id is null || id < 1) return BadRequest();

            UrlEntity? urlToRemove = await _sender.Send(new GetUrlByIdQuery(Convert.ToInt32(id)));

            if (urlToRemove is null) return NotFound();

            await _sender.Send(new RemoveUrlCommand(urlToRemove));

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