using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace Vezeeta.Web.Controllers
{
    [ApiController]
    public class ApplicationController : ControllerBase
    {
        public ApplicationController()
        {
        }

        protected int GetUserId()
        {
            var subjectIdClaim = HttpContext.User.Claims.FirstOrDefault(c => c.Type == ClaimTypes.NameIdentifier);

            if (subjectIdClaim != null)
            {
                int userId;
                bool userIdParsedSuccessfully = int.TryParse(subjectIdClaim.Value, out userId);

                if (userIdParsedSuccessfully)
                    return userId;
            }

            return 0;
        }

    }
}
