using EduCoreApi.Application.Feature.Departments.Commands;
using EduCoreApi.Application.Feature.Departments.Models;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DepartmentController : ControllerBase
    {
        private readonly IMediator _mediator;

        public DepartmentController(IMediator mediator)
        {
            _mediator = mediator;
        }
        [HttpGet]
        public async Task<IActionResult> GetDepartments(CancellationToken cancellationToken)
        {
            return Ok();
        }

        [HttpPost]
        public async Task<CreateDepartmentDto> Create([FromBody] CreateDepartmentCommand createDepartmentCommand, CancellationToken cancellationToken)
        {
            return await _mediator.Send(createDepartmentCommand, cancellationToken);
        }
    }
}
