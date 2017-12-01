using GigHub.Models;
using Microsoft.AspNet.Identity;
using System.Web.Http;

namespace GigHub.Controllers
{
    [Authorize]
    public class AttendancesController : ApiController
    {
        private readonly ApplicationDbContext _applicationDbContext;

        public AttendancesController()
        {
            _applicationDbContext = new ApplicationDbContext();
        }

        [HttpPost]
        public IHttpActionResult Attend([FromBody]int gigId)
        {
            var attendance = new Attendance
            {
                GigId = gigId,
                AttendeeId = User.Identity.GetUserId()
            };
            _applicationDbContext.DbSetAttendance.Add(attendance);
            _applicationDbContext.SaveChanges();

            return Ok();
        }
    }
}
