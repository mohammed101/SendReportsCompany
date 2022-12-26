using MassTransit;
using MassTransit.Transports.Fabric;
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

        config.Message<ISendReportRequest>(e => e.SetEntityName("report-requests")); // name of the primary exchange
        config.Publish<ISendReportRequest>(e => e.ExchangeType = ExchangeType.Direct); // primary exchange type
        config.Send<ISendReportRequest>(e =>
        {
            e.UseRoutingKeyFormatter(context => context.Message.Provider.ToString()); // route by provider (email or fax)
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
