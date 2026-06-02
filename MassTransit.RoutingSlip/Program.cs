using MassTransit;
using MassTransit.RoutingSlip;

HostApplicationBuilder builder = Host.CreateApplicationBuilder(args);

builder.Services.AddMassTransit(x =>
{
    x.AddConsumer<BidSubmittedConsumer>();

    x.AddActivity<BidAcceptedActivity, BidAcceptedArguments, BidAcceptedLog>();
    x.AddActivity<BidRejectedActivity, BidRejectedArguments, BidRejectedLog>();
    x.AddActivity<AquiredActivity, AquiredArguments, AquiredLog>();

    x.UsingAzureServiceBus((context, cfg) =>
    {
        cfg.Host(new Uri("sb://sb-masstransit-neu-002.servicebus.windows.net"));

        cfg.ConfigureEndpoints(context);
    });
});

builder.Services.AddHostedService<Worker>();


var host = builder.Build();
host.Run();
