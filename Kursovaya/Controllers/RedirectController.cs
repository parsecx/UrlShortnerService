using Kursovaya.Data;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Kursovaya.Controllers
{
    public class RedirectController : Controller
    {
        private readonly ApplicationDbContext _context;
        public RedirectController(ApplicationDbContext context)
        {
            _context = context;
        }
        [AllowAnonymous]
        [Route("/{id}")]
        [HttpGet]
        public IActionResult RedirectionResult(string id)
        {
            try
            {
                var redirectUrl = _context.UrlModels.Where(x => x.ShortUrl == id).First();
                if(!redirectUrl.IsEnalbed) 
                {
                    throw new ArgumentNullException();
                }
                redirectUrl.AmountOfRedirections++;
                _context.UrlModels.Update(redirectUrl);
                _context.SaveChanges();
                return RedirectPermanent(redirectUrl.OriginalUrl);
            }
            catch (InvalidOperationException ex)
            {
                ViewBag.Message = "Required URL not found!";
                return View();
            }
            catch (ArgumentNullException ex)
            {
                ViewBag.Message = "This URL is not availible at the moment.";
                return View();
            }
            catch(Exception ex) 
            {
                ViewBag.Message="Something went wrong right now...";
                return View();
            }
        }
    }
}
