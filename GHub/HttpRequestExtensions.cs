using Microsoft.AspNetCore.Http;

namespace GHub
{
    public static class HttpRequestExtensions
    {
        public static string GetAbsoluteFullHost(this HttpRequest request)
        {
            return $"{request.Scheme}://{request.Host}";
        }
    }
}