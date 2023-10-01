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
            return Ok($"Job Id:{jobId}, Welcome email sent to user");
        }
        [HttpPost]
        [Route("[action]")]
        public IActionResult Discount()
        {
            //Delayed Job
            int timeInSeconds = 30;
            var jobId = BackgroundJob.Schedule(() => SendWelcomeEmail("Welcome to our app."),TimeSpan.FromSeconds(timeInSeconds));
            return Ok($"Job Id:{jobId}, Discount email sent to user in {timeInSeconds}");
        }
        [HttpPost]
        [Route("[action]")]
        [Obsolete]
        public IActionResult DatabaseUpdate()
        {
            //Recurring job

            RecurringJob.AddOrUpdate(() => Console.WriteLine("database updated"), Cron.Minutely);
            return Ok("Database check job initiated");
        }
        public void SendWelcomeEmail(string text)
        {
            Console.WriteLine(text);
        }
    }
}