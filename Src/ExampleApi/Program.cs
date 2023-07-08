var builder = WebApplication.CreateBuilder(args);


// Add services to the container.
var tmp = builder.Configuration.GetValue<string>("ConnectionString");
builder.Services.AddControllers();
builder.Services.AddScoped<Burza>(x => new Burza(tmp));
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();


app.UseMiddleware<ExeptionMiddleWare>();
app.MapControllers();
app.UseMiddleware<TimeMiddleWare>();

app.Map("tmp", () => new { x = "pyszny" });

new tmp(app.Services);

app.Run();


class ExeptionMiddleWare
{

    private readonly RequestDelegate _next;
    private readonly ILogger<ExeptionMiddleWare> logger;

    public ExeptionMiddleWare(RequestDelegate next, ILogger<ExeptionMiddleWare> logger)
    {
        _next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (ValidationException)
        {
            context.Response.StatusCode = 400;
        }
        catch (Exception ex)
        {
            logger.LogError(ex, "B³ad w requescie");
            context.Response.StatusCode = 500;
            context.Response.WriteAsJsonAsync(ex);
        }
    }
}

class TimeMiddleWare
{

    private readonly RequestDelegate _next;
    private readonly ILogger<ExeptionMiddleWare> logger;

    public TimeMiddleWare(RequestDelegate next, ILogger<ExeptionMiddleWare> logger)
    {
        _next = next;
        this.logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        var time = System.Diagnostics.Stopwatch.GetTimestamp();
        try
        {
            await _next(context);
        }
        finally
        {
            var elapsed = System.Diagnostics.Stopwatch.GetElapsedTime(time);
            logger.LogInformation("Method executed in {Time}", elapsed);
        }
    }
}
public class ValidationException : Exception
{

}

public class Burza
{
    private static int number;
    private readonly string connectionString;

    public Burza(string connectionString)
    {
        number = number + 1;
        this.connectionString = connectionString;
    }
    public void CzyPada()
    {

    }
}

public class tmp
{
    public tmp(IServiceProvider serviceDescriptors)
    {
        using (var scope = serviceDescriptors.CreateScope())
        {
            var burza = scope.ServiceProvider.GetRequiredService<Burza>();
            burza = scope.ServiceProvider.GetRequiredService<Burza>();
            burza = scope.ServiceProvider.GetRequiredService<Burza>();
            burza = scope.ServiceProvider.GetRequiredService<Burza>();
        }

    }
}