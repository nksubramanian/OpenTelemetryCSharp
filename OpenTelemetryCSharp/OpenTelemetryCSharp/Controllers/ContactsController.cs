using Microsoft.AspNetCore.Mvc;

namespace OpenTelemetryCSharp.Controllers
{

    [ApiController]
    [Route("[controller]")]
    public class ContactsController : ControllerBase
    {

        [HttpGet]
        public string Get()
        {
            return "subbu";
        }

    }
}
