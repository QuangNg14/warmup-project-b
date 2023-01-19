using Warmup_teamb_backend.Hubs;
using static System.Net.WebRequestMethods;
using Warmup_teamb_backend.Services;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorPages();
builder.Services.AddSignalR().AddAzureSignalR("Endpoint = https://warmup-b-signalr.service.signalr.net;AccessKey=ZyTefs2p2hCFkYgTdyj2gFcvojWX6A8isww52NZmqZU=;Version=1.0;");
//builder.Services.AddSignalR();
var app = builder.Build();

if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();
app.MapHub<ChatHub>("/chatHub");

app.Run();