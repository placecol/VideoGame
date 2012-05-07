using System.Linq;
using System.Web.Mvc;
using Riddley.VideoGame.Web.Models;

namespace Riddley.VideoGame.Web.Controllers
{
    [Authorize]
    public class HomeController : RavenController
    {
        public ActionResult Index()
        {
            var user = GetUser();
            return View(user);
        }

        private User GetUser()
        {
            var userName = User.Identity.Name;
            return Docs.Query<User>().Single(p => p.AspNetUserGuid == userName);
        }
    }
}