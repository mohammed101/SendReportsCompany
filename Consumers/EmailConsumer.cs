using MassTransit;

namespace SendReportsCompany.Consumers
{
    public class EmailConsumer : IConsumer<ISendReportRequest>
    {
        public Task Consume(ConsumeContext<ISendReportRequest> context)
        {
            // do work to send email here
            return Task.CompletedTask;
        }
    }
}
