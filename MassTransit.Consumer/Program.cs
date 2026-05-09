using MassTransit;
using MassTransit.Consumer;
using Microsoft.Extensions.Hosting;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<CreateArtWorkConsumer>();

    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(new Uri("sb://sb-masstransit-neu-001.servicebus.windows.net"));

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();

await host.RunAsync();