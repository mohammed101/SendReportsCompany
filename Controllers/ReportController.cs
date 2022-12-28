using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

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
    }
}
