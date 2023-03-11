using System.Net;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace ExtDependency
{
    public class Function1
    {
        private readonly ILogger _logger;

        public Function1(ILoggerFactory loggerFactory)
        {
            _logger = loggerFactory.CreateLogger<Function1>();
        }

       
        [Function("Function1")]
        public HttpResponseData Run([HttpTrigger(AuthorizationLevel.Anonymous, "get", "post")] HttpRequestData req, FunctionContext context)
        {

            req.Headers.TryGetValues("traceparent", out var headerValue);
            _logger.LogInformation("C# HTTP trigger function processed a request.");
            var (operationId, parentId) = GetOperationIdAndParentId(context);
            var response = req.CreateResponse(HttpStatusCode.OK);
            response.Headers.Add("Content-Type", "text/plain; charset=utf-8");

            response.WriteString("Welcome to Azure Functions!");

            return response;
        }


        private (string, string) GetOperationIdAndParentId(FunctionContext executionContext)
        {
            var traceParent = executionContext.TraceContext.TraceParent;
            if (string.IsNullOrEmpty(traceParent))
            {
                return ("", "");

            }
            var partofTraceParent = traceParent.Split('-');
            if (partofTraceParent.Length != 4)
            {
                return ("", "");

            }


            return (partofTraceParent[0], partofTraceParent[1]);

        }
    }
}
