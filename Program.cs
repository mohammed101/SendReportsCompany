using MassTransit;
using RabbitMQ.Client;
using SendReportsCompany;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.

builder.Services.AddControllers();
// Learn more about configuring Swagger/OpenAPI at https://aka.ms/aspnetcore/swashbuckle
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

builder.Services.AddMassTransit(mt =>
{
    mt.UsingRabbitMq((context, config) =>
    {
        config.Host("localhost", "/", h =>
        {
            h.Username("guest");
            h.Password("guest");
        });

        config.ReceiveEndpoint("email-reports", re =>
        {
            // turns off default fanout settings
            re.ConfigureConsumeTopology = false;
            // a replicated queue to provide high availability and data safety. available in RMQ 3.8+
            re.SetQuorumQueue();

            // enables a lazy queue for more stable cluster with better predictive performance.
            // Please note that you should disable lazy queues if you require really high performance, if the queues are always short, or if you have set a max-length policy.
            re.SetQueueArgument("declare", "lazy");

            re.Consumer<EmailConsumer>();
            re.Bind("report-requests", e =>
            {
                e.RoutingKey = "email";
                e.ExchangeType = ExchangeType.Direct;
            });
        });
        config.ReceiveEndpoint("fax-reports", re =>
        {
            re.ConfigureConsumeTopology = false;
            re.Consumer<FaxConsumer>();
            re.Bind("report-requests", e =>
            {
                e.RoutingKey = "fax";
                e.ExchangeType = ExchangeType.Direct;
            });
        });
    });
    builder.Services.AddMassTransitHostedService();
});

var app = builder.Build();

// Configure the HTTP request pipeline.
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

app.UseHttpsRedirection();

app.UseAuthorization();

app.MapControllers();

app.Run();
