using Microsoft.AspNetCore.Mvc;

namespace ChatBotStocksQuotes.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class HealthCheckController : ControllerBase
    {
        public string Get()
        {
            return "I'm gooood";
        }
    }
}
