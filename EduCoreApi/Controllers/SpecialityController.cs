using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Specialities.Commands;
using EduCoreApi.Application.Feature.Specialities.Models;
using EduCoreApi.Application.Feature.Specialitys.Commands;
using EduCoreApi.Application.Feature.Specialitys.Queries;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SpecialityController : ControllerBase
    {
        private readonly IMediator _mediator;
        public SpecialityController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<ApiResponse<List<GetSpecialityDto>>> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetSpecialities(), cancellationToken);

            if (response.Code == 200)
                return response;
            else
                return response;
        }

        [HttpGet("{id:Guid}")]
        public async Task<ApiResponse<GetSpecialityDto>> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetSpecialityById(id), cancellationToken);

            if (response.Code == 200)
                return response;
            else
                return response;
        }

        [HttpPost]
        public async Task<ApiResponse<CreateSpecialityDto>> Create([FromBody] CreateSpecialityCommand createSpecialityCommand, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(createSpecialityCommand, cancellationToken);

            if (response.Code == 200)
                return response;
            else
                return response;
        }

        [HttpPut]
        public async Task<ApiResponse<Guid>> Update([FromBody] UpdateSpecialityCommand updateSpecialityCommand, CancellationToken cancellationToken = default)
        {
            if (updateSpecialityCommand.SpecialityId == Guid.Empty)
            {
                return new ApiResponse<Guid>
                {
                    Code = 400,
                    Message = "ID in the URL does not match ID in the body",
                    Data = Guid.Empty
                };
            }

            var response = await _mediator.Send(updateSpecialityCommand, cancellationToken);

            if (response.Code == 200)
                return response;
            else
                return response;
        }

        [HttpDelete("{id:Guid}")]
        public async Task<IActionResult> Delete(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new DeleteSpecialityCommand(id), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }
    }
}