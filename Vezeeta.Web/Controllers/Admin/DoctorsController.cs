using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Vezeeta.Service.Dtos.Request.Doctors;

namespace Vezeeta.Web.Controllers.Admin
{
    public class DoctorsController : AdminBaseController
    {
        public DoctorsController()
        {
            
        }

        [HttpGet]
        public ActionResult<string> GetAll()
        {
            return Ok(string.Empty);
        }

        [HttpGet]
        public ActionResult<string> GetById(string id)
        {
            return Ok(string.Empty);
        }

        [HttpPost]
        public ActionResult<bool> Add(AddDoctorDto request)
        {
            return Ok(true);
        }
    }
}
