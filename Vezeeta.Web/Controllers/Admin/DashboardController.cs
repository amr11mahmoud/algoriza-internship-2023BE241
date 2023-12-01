using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Service.Dtos.Response.Admin;

namespace Vezeeta.Web.Controllers.Admin
{
    public class DashboardController : AdminBaseController
    {
        public DashboardController() { }

        [HttpGet]
        public ActionResult<int> GetNumberOfDoctors()
        {
            return Ok(10);
        }

        [HttpGet]
        public ActionResult<TopRequestsResponseDto> GetNumberOfPatients()
        {
            return Ok(new TopRequestsResponseDto());
        }

        

        [HttpGet]
        public ActionResult<int> GetNumberOfRequests()
        {
            return Ok(5);
        }

        [HttpGet]
        public ActionResult TopFiveSpecializations()
        {
            return Ok("hello");
        }

        [HttpGet]
        public ActionResult TopTenDoctors()
        {
            return Ok("hello");
        }
    }
}
