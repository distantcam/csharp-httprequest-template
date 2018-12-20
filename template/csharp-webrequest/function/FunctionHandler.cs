using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Function
{
    public class FunctionHandler
    {
        public async Task Handle(HttpContext context)
        {
            var input = GetRequestBody(context);

            await context.Response.WriteAsync($"Hello! Your input was {input}");
        }

        private string GetRequestBody(HttpContext context)
        {
            var reader = new StreamReader(context.Request.Body);
            string text = reader.ReadToEnd();
            return text;
        }
    }
}