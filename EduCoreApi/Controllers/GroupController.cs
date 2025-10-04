using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Departments.Commands;
using EduCoreApi.Application.Feature.Departments.Models;
using EduCoreApi.Application.Feature.Departments.Queries;
using EduCoreApi.Application.Feature.Groups.Commands;
using EduCoreApi.Application.Feature.Groups.Models;
using EduCoreApi.Application.Feature.Groups.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GroupController : ControllerBase
    {
        private readonly IMediator _mediator;

        public GroupController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetGroups(), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetGroupById(id), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateGroupCommand createGroupCommand, CancellationToken cancellationToken = default)
        {
            var request = await _mediator.Send(createGroupCommand, cancellationToken);

            if (request.Code == 200)
            {
                return Ok(request);
            }
            else
            {
                return NotFound(request);
            }
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromBody]  UpdateGroupCommand updateDepartmentCommand, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(updateDepartmentCommand, cancellationToken);

            if (response.Code == 200)
            {
                return Ok(response);
            }
            else
            {
                return NotFound(response);
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new DeleteGroupCommand(id), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

    }
}