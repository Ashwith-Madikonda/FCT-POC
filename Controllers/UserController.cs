using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace FCT_POC_API.Controllers
{
    public class UserController : Controller
    {
        [HttpGet("GetUserDetails")]
        [Authorize]
        public async Task<ActionResult<dynamic>> GetUserDetails()
        {
            var user = new  { 
                FirstName = "Ashwith",
                LastName = "Madikonda"
            };
            return Ok(user);
        }
    }
}
