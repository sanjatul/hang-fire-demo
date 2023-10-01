using Hangfire;
using Microsoft.AspNetCore.Mvc;

namespace hang_fire.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class HangfireController : ControllerBase
    {
        [HttpPost]
        [Route("[action]")]
        public IActionResult Welcome()
        {
            //Fire and Forget Job - For one time
            var jobId=BackgroundJob.Enqueue(()=>SendWelcomeEmail("Welcome to our app."));
            return Ok($"Job Id:{jobId}, Welcome email ent to user");
        }
        public void SendWelcomeEmail(string text)
        {
            Console.WriteLine(text);
        }
    }
}