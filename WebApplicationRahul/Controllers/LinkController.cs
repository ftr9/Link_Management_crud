using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;
using WebApplicationRahul.Data;
using WebApplicationRahul.Models;

namespace WebApplicationRahul.Controllers
{
    [Authorize]
    public class LinkController : Controller
    {

        public readonly WebApplicationRahulContext dbContext;
        public LinkController(WebApplicationRahulContext ctx) {
            this.dbContext = ctx;
        }

        [HttpGet]
        public IActionResult Index()
        {
            var userPrincipals = User as ClaimsPrincipal;
            var userEmail = userPrincipals?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrEmpty(userEmail))
            {
                var userWithLinks = this.dbContext.User.Where(u => u.Email == userEmail).Include(l => l.Links).FirstOrDefault();
                
                if(userWithLinks == null)
                {
                    return NotFound();
                }

                return View(userWithLinks);
            }
            return RedirectToRoute("/");
        }

        [HttpGet]
        public IActionResult Add()
        {
            return View();      
        }

        [HttpPost]
        public IActionResult Add(AddLinkModel linkModel)
        {
            var userPrincipals = User as ClaimsPrincipal;
            var userEmail = userPrincipals?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if (!String.IsNullOrEmpty(userEmail))
            {

                var user = this.dbContext.User.First(U => U.Email == userEmail);
                this.dbContext.Links.Add(new Link
                {
                    Name = linkModel.Name,
                    url = linkModel.url,
                    purpose = linkModel.purpose,
                    User = user,
                });
                this.dbContext.SaveChanges();
                return RedirectToAction("Index");

            }

            return RedirectToRoute("/");
        }


        [HttpGet]
        public IActionResult Details(int? linkId)
        {
            if(linkId == null)
            {
                return NotFound();
            }

            var userPrincipals = User as ClaimsPrincipal;
            var userEmail = userPrincipals?.Claims?.FirstOrDefault(c => c.Type == ClaimTypes.Email)?.Value;
            if(String.IsNullOrEmpty(userEmail))
            {
                return RedirectToRoute("/Account/Login");
            }
            
            
            var user = this.dbContext.User.First(U => U.Email == userEmail);
            var ValidLink = this.dbContext.Links.FirstOrDefault(l => l.Id == linkId && l.UserId == user.Id);
            if(ValidLink == null)
            {
                    return NotFound();
            }

            return View(ValidLink);

             
        }

        [HttpPost]
        public IActionResult Update(Link link)
        {
            this.dbContext.Links.Update(link);
            this.dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

        [HttpPost]
        public IActionResult Delete(Link link)
        {

            var validLink = this.dbContext.Links.FirstOrDefault(l => l.Id == link.Id);
            this.dbContext.Links.Remove(validLink);
            this.dbContext.SaveChanges();
            return RedirectToAction("Index");
        }

    }
}
