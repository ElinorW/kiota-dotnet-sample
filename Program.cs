using System;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.IO;
using Azure.Identity;
using Microsoft.Kiota.Http.HttpClientLibrary;
using Microsoft.Kiota.Authentication.Azure;
using Microsoft.Kiota.Abstractions;

namespace Graphdotnetv4
{
    class Program
    {
        static async Task Main(string[] args)
        {
            var requestAdapter = GetRequestAdapter();
            var democlient = new DemoSDK.DemoClient(requestAdapter);

            var messages = await democlient.Users["elinor@binarydomain.onmicrosoft.com"].Messages.GetAsync();
            foreach (var message in messages!.Value!)
            {
                Console.WriteLine(message.Subject);
            }
        }

        static IRequestAdapter GetRequestAdapter()
        {
            var options = new InteractiveBrowserCredentialOptions
            {
                ClientId = "fec9af70-c91d-4a56-8a81-04bc510325c1",
                TenantId = "common",
                RedirectUri = new Uri("http://localhost:1234")
            };
            return new HttpClientRequestAdapter(new AzureIdentityAuthenticationProvider(new InteractiveBrowserCredential(options), scopes: new string[] { "Mail.Read" }));

        }
    }
}
