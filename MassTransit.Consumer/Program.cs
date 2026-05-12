using Azure.Data.Tables;
using Azure.Identity;
using MassTransit;
using MassTransit.Consumer;
using Microsoft.Extensions.Hosting;


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);


builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<CreateArtWorkConsumer>();

    TableServiceClient serviceClient = new(
        endpoint: new Uri("https://stmasstransit.table.core.windows.net/"),
        new DefaultAzureCredential()
    );

    x.AddSagaStateMachine<ArtAcquisitionStateMachine, ArtAcquisition>()
        .AzureTableRepository(r =>
        {
            r.ConnectionFactory(() => serviceClient.GetTableClient("ArtAcquisition"));
        });

    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(new Uri("sb://sb-masstransit-neu-001.servicebus.windows.net"));

        cfg.ConfigureEndpoints(context);
    });
});

var host = builder.Build();

await host.RunAsync();