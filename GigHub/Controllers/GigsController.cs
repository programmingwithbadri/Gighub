using GigHub.Models;
using GigHub.ViewModels;
using Microsoft.AspNet.Identity;
using System.Linq;
using System.Web.Mvc;

namespace GigHub.Controllers
{
    public class GigsController : Controller
    {
        /// <summary>
        ///  Object to access the dbContext to connect the entity framework.
        /// </summary>
        private ApplicationDbContext applicationDbContext;

        /// <summary>
        /// Constructor to create object for DBContext.
        /// </summary>
        public GigsController()
        {
            applicationDbContext = new ApplicationDbContext();
        }

        /// <summary>
        /// Controller to Create or Add the Gigs
        /// Authorize is used to validate only the authorised artist to access this page
        /// </summary>
        /// <returns>ViewModel that contains the list of Gigs</returns>
        [Authorize]
        public ActionResult Create()
        {
            var viewModel = new GigFormViewModel { Genres = applicationDbContext.DbSetGenre.ToList() }; // gigFormViewModel should return Genres
            return View(viewModel);
        }

        /// <summary>
        /// COntroller to Post the created Gigs to Database
        /// </summary>
        /// <param name="gigFormViewModel">ViewModel that contains the Gig Details</param>
        /// <returns>Redirects to the next page</returns>
        [Authorize]
        [HttpPost]
        [ValidateAntiForgeryToken] // Prevents CSRF Forgery attack
        public ActionResult Create(GigFormViewModel gigFormViewModel)
        {
            if (!ModelState.IsValid) // Checks whether the state changes else will go into this method
            {
                gigFormViewModel.Genres = applicationDbContext.DbSetGenre.ToList(); // gigFormViewModel should return Genres
                return View("Create", gigFormViewModel);
            }

            var gig = new Gig
            {
                ArtistID = User.Identity.GetUserId(),
                GenreID = gigFormViewModel.Genre,
                DateTime = gigFormViewModel.GetDateTime(),
                Venue = gigFormViewModel.Venue

            };

            applicationDbContext.DbSetGig.Add(gig); // Add the Gig to the Gig table(dbSetGig) using DB Context(applicationDbContext)
            applicationDbContext.SaveChanges(); // EF will generate the SQL statement and will update the database

            return RedirectToAction("Index", "Home"); // Returns to the Redirect Page. Index - Action, Home - Controller

        }
    }
}