using EduCoreApi.Application.Feature.Departments.Commands;
using EduCoreApi.Application.Feature.Departments.Models;
using EduCoreApi.Application.Feature.Departments.Queries;
using EduCoreApi.Application.Common.Results;
using Microsoft.AspNetCore.Mvc;
using MediatR;

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
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetDepartments(), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<ApiResponse<GetDepartmentDto>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetDepartmentById(id), cancellationToken);

            if (response.Code == 200)
                return response;
            else
                return response;

        }

        //[HttpPost]
        //public async Task<CreateDepartmentDto> Create([FromBody] CreateDepartmentCommand createDepartmentCommand, CancellationToken cancellationToken = default)
        //{
        //    return await _mediator.Send(createDepartmentCommand, cancellationToken);
        //}

        [HttpPut]
        public async Task<ApiResponse<Guid>> Update([FromBody] UpdateDepartmentCommand updateDepartmentCommand, CancellationToken cancellationToken = default)
        {
            if (updateDepartmentCommand.DepartmentId != updateDepartmentCommand.DepartmentId)
            {
                return new ApiResponse<Guid>
                {
                    Code = 400,
                    Message = "ID in the URL does not match ID in the body",
                    Data = Guid.Empty
                };
            }

            var response = await _mediator.Send(updateDepartmentCommand, cancellationToken);

            if (response.Code == 200)
            {
                return response;
            }
            else
            {
                return response;
            }
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new DeleteDepartmentCommand(id), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }
    }
}
