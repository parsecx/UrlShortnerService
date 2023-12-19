using Kursovaya.Data;
using Kursovaya.Entities;
using Kursovaya.Models;
using Kursovaya.NewFolder;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace Kursovaya.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly ApplicationDbContext _context;
        private readonly UserManager<IdentityUser> _user;
        public HomeController(ILogger<HomeController> logger, UserManager<IdentityUser> user, ApplicationDbContext context)
        {
            _logger = logger;
            _user = user;
            _context = context;
        }

        public IActionResult Index(int page = 1)
        {
            List<UrlModel> urls = _context.UrlModels.ToList();
            int pageSize = 10;
            var count = urls.Count();
            var items = urls.Skip((page - 1) * pageSize).Take(pageSize).ToList();
            PageViewModel pageViewModel = new PageViewModel(count, page, pageSize);
            var model = new IndexViewModel(items,pageViewModel);
            return View(model);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }

        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Create(UrlModel model)
        {
            var identity = await _user.GetUserAsync(User);
            model.CreatedAt = DateTime.Now;
            model.Author = identity;
            model.IsEnalbed = true;
            model.ShortUrl = HashGenerator.GenerateHash(model.OriginalUrl);
            await _context.UrlModels.AddAsync(model);
            _context.SaveChanges();
            return Redirect("Index");
        }

        [HttpPost]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id != null)
            {
                UrlModel ? link = _context.UrlModels.First(p => p.Id == id);
                if (link != null)
                {
                    _context.UrlModels.Remove(link);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Index");
                }
            }
            return NotFound();
        }
    }
}
