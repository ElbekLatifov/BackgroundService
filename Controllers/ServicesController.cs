using BackgroundServices.Services;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace BackgroundServices.Controllers;

[Route("api/[controller]")]
[ApiController]
public class ServicesController : ControllerBase
{
    [HttpPost]
    public IActionResult Add(int s, int t)
    {
        if(!ModelState.IsValid)
        {
            return BadRequest("Invalide sonlar");
        }
        var job = new Job()
        {
            Id = Guid.NewGuid(),
            A = s,
            B = t
        };
        JobService.Jobs.Add(job);

        return Ok(job.Id);
    }
    [HttpPost("result")]
    public async Task<IActionResult> GetResult(Guid guid)
    {
        var job = JobService.Jobs.FirstOrDefault(x => x.Id == guid);
        if (job == null)
        {
            return NotFound();
        }

        if (!job.IsReady)
        {
            return Ok("Bajarilmoqda...");
        }

        return Ok(job.Result);
    }
}
