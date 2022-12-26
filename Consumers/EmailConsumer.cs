using MassTransit;

namespace SendReportsCompany.Consumers
{
    public class EmailConsumer : IConsumer<ISendReportRequest>
    {
        public Task Consume(ConsumeContext<ISendReportRequest> context)
        {
            throw new NotImplementedException();
        }
    }
}
