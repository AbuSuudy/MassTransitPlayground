using Azure.Data.Tables;
using Azure.Identity;
using MassTransit;
using MassTransit.Consumer;
using Microsoft.Extensions.Hosting;


HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

var serviceClient = new TableServiceClient(
    new Uri("https://stmasstransit001.table.core.windows.net"),
    new DefaultAzureCredential()
);

builder.Services.AddMassTransit(x =>
{
    x.SetKebabCaseEndpointNameFormatter();

    x.AddConsumer<BidSubmittedConsumer>();

    x.AddConsumer<BidAcceptedConsumer>();

    x.AddConsumer<BidRejectedConsumer>();

    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(new Uri("sb://sb-masstransit-neu-002.servicebus.windows.net"));

        cfg.ConfigureEndpoints(context);
    });


    x.AddSagaStateMachine<ArtAcquisitionStateMachine, ArtAcquisition>()
    .AzureTableRepository(r =>
    {
        serviceClient.CreateTableIfNotExists("ArtAcquisition");

        r.ConnectionFactory(() => serviceClient.GetTableClient("ArtAcquisition"));
    });
});


var host = builder.Build();

await host.RunAsync();