using System.Net;
namespace NetKubernetes.Middleware;

public class MiddlewareException : Exception 
{
    public HttpStatusCode Code { get; set; }
    public object? Errors { get; set; }
    public MiddlewareException(HttpStatusCode code, object? errors = null)
    {
        Code = code;
        Errors = errors;
    }
}