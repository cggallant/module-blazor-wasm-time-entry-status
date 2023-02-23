using blazor_time_entry_status_module;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http.Headers;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");


// Create an HttpClient object with a default Accept header (all calls to the Http object in the razor pages will have this
// header. saves a step later.)
HttpClient Http = new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) };
Http.DefaultRequestHeaders.Accept.Clear();
Http.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));

#if DEBUG
    // If you need to run this locally without using the Timesheet Simulator, you can use the following - F5/Ctrl+F5 don't currently link to the
    // simulator container. You need to Publish to see changes in Simulator (publishing uses Release mode so this block isn't included. index.razor
    // also has a debug statement that will send calls to the Timesheet API)
    //
    // You'll need to adjust the following credentials as this is from a demo database I used to have.
    string AccessToken = "access_token=\"client=0b216a3614354c5c8b84b98ea779fe36.352977&user_token=64fc431e5fbd46b8916477dd71841216.352977\"";
    Http.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("WRAP", AccessToken);
#endif

// Add the HttpClient to the services collection
builder.Services.AddScoped(sp => Http);


await builder.Build().RunAsync();
