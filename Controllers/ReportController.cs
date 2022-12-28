using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SendReportsCompany.Models;

namespace SendReportsCompany.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ReportController : ControllerBase
    {
        private readonly IMediator _meditor;
        public ReportController(IMediator mediator)
        {
            _meditor = mediator;
        }

        public async Task<ActionResult> SubmitOrder([FromBody] ReportRequestModel reportRequestModel)
        {
            await _meditor.Publish(new SubmitReportRequestCommand { ReportRequestModel = reportRequestModel });
            return Ok();
        }
    }
}
