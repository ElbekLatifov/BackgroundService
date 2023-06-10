
namespace BackgroundServices.Services;
public class ExampleBackgroundService : BackgroundService
{
    protected override async Task ExecuteAsync(CancellationToken stoppingToken) 
    {
        var timer = new Timer(Tick, null, 0, 20000); 
    }

    public void Tick(object? sender)
    {
        var jobs = JobService.Jobs.Where(p=>p.IsReady ==  false).ToList();  

        foreach (var job in jobs)
        {
            job.Result = job.A + job.B;
            job.IsReady = true;
        }
    }
}

public class JobService
{
    public static List<Job> Jobs { get; set; } = new List<Job>();
}

public class Job
{
    public Guid Id { get; set; }
    public int A { get; set; }
    public int B { get; set; }
    public int Result { get; set; }
    public bool IsReady { get; set; }
}
