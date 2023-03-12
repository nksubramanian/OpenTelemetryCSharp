using System.Diagnostics;

public class ActivityMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ActivityMiddleware> _logger;

    public ActivityMiddleware(RequestDelegate next, ILogger<ActivityMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task Invoke(HttpContext context)
    {
        // Create a new activity
        var activity = new Activity("WebApi.Request");
        //activity.SetParentId("00-295dd8a547b4d06405c1ea99c0bc512f-663b9cc4055b4400-01 ");

        // Set the activity trace ID and parent ID from the incoming request headers
        if (context.Request.Headers.TryGetValue("traceparent", out var traceparentHeader))
        {
            //var traceparent = ActivityContext.Parse(traceparentHeader.ToString());
            //activity.SetParentId(traceparent.SpanId, traceparent.TraceId, traceparent.TraceFlags);
            activity.SetParentId(traceparentHeader);


        }

            activity.Start();
       

        try
        {
            var t = activity.Id; 
            // Call the next middleware in the pipeline
            await _next(context);
        }
        finally
        {
            // Stop the activity
            activity.Stop();

            // Log the activity
            _logger.LogInformation(activity.OperationName + " completed in " + activity.Duration);
        }
    }

}