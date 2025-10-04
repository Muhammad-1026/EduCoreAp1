using EduCoreApi.Application.Feature.Subjects.Commands;
using EduCoreApi.Application.Feature.Subjects.Queries;
using EduCoreApi.Application.Feature.Teachers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public SubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetSubjects(), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetSubjectById(id), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromForm] CreateSubjectCommand createSubjectCommand, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(createSubjectCommand, cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpPut]
        public async Task<IActionResult> Update([FromForm] UpdateSubjectCommand updateSubjectCommand, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(updateSubjectCommand, cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpDelete]
        public async Task<IActionResult> Delete([FromBody] DeleteTeacherCommand deleteTeacherCommand, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(deleteTeacherCommand, cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }
    }
}