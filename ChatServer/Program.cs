using Grpc.Net.Client;
using Microsoft.AspNetCore.Server.Kestrel.Core;

var builder = WebApplication.CreateBuilder(args);

builder.WebHost.ConfigureKestrel(options =>
{
    // Setup a HTTP/2 endpoint without TLS.
    options.ListenLocalhost(5000, o => o.Protocols =
        HttpProtocols.Http2);
    options.ListenLocalhost(5001, o => o.Protocols = HttpProtocols.Http1);
    options.ListenAnyIP(80, listenOptions =>
    {
        listenOptions.Protocols = HttpProtocols.Http1AndHttp2;
    });
});

// Add services to the container.
builder.Services.AddGrpc();
builder.Services.AddMagicOnion();
// swagger用のview
builder.Services.AddControllersWithViews();

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseRouting();
app.UseEndpoints(endpoints =>
{
    // swaggerで叩く通信のエンドポイント
    endpoints.MapMagicOnionHttpGateway("_", ((IApplicationBuilder)app).ApplicationServices.GetService<MagicOnion.Server.MagicOnionServiceDefinition>()!.MethodHandlers, GrpcChannel.ForAddress("http://localhost:80"));
    // swaggerを使う
    endpoints.MapMagicOnionSwagger("swagger", ((IApplicationBuilder)app).ApplicationServices.GetService<MagicOnion.Server.MagicOnionServiceDefinition>()!.MethodHandlers, "/_/");
    endpoints.MapMagicOnionService();
    endpoints.MapGet("/", async context =>
    {
        await context.Response.WriteAsync(
            "Communication with gRPC endpoints must be made through a gRPC client. To learn how to create a client, visit: https://go.microsoft.com/fwlink/?linkid=2086909");
    });
});

app.Run();