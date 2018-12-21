# OpenFaaS C# HTTP Template

This repository contains the template for OpenFaaS using the upgraded `of-watchdog` which allows for higher throughput.

```
$ faas template pull https://github.com/distantcam/csharp-webrequest-template
$ faas new --list
Languages available as templates:
- csharp-webrequest

```

This template uses a middleware handler in an ASPNET Core Web API. This allows additional context available in the request (by providing the full body to the handler) and more control over the response by passing it back to the HTTP reponse context.

## Using the template
First, pull the template with the faas CLI and create a new function:

```
$ faas-cli template pull https://github.com/distantcam/csharp-webrequest-template
$ faas-cli new --lang csharp-webrequest <function name>
```

In the directory that was created, using the name of you function, you'll find `FunctionHandler.cs`. It will look like this:

``` csharp
using Microsoft.AspNetCore.Http;
using System.IO;
using System.Threading.Tasks;

namespace Function
{
    public class FunctionHandler
    {
        public async Task<(int, string)> Handle(HttpRequest request)
        {
            var reader = new StreamReader(request.Body);
            var input = await reader.ReadToEndAsync();

            return (200, $"Hello! Your input was {input}");
        }
    }
}
```

This is a simple implementation of a hello-world function. 

You are able to add packages to your function using the `dotnet add package` syntax. The packages will be added to your final function's container automatically.

For example, you could add the popular `Newtonsoft.JSON` package for formatting JSON objects.

```csharp
using Microsoft.AspNetCore.Http;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Function
{
    public class SampleResponse
    {
        public string FunctionStatus { get; set; }
    }
    
    public class FunctionHandler
    {
        public Task<(int, string)> Handle(HttpRequest request)
        {
            var res = new SampleResponse();
            res.FunctionStatus = "Success";

            var output = JsonConvert.SerializeObject(res);

            return Task.FromResult((200, output));
        }
    }
}
```