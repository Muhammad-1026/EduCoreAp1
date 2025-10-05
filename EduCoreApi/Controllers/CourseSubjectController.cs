using EduCoreApi.Application.Feature.Courses.Commands;
using EduCoreApi.Application.Feature.Courses.Queries;
using EduCoreApi.Application.Feature.CourseSubjects.Commands;
using EduCoreApi.Application.Feature.CourseSubjects.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreApi.API.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CourseSubjectController : ControllerBase
    {
        private readonly IMediator _mediator;

        public CourseSubjectController(IMediator mediator)
        {
            _mediator = mediator;
        }

        [HttpGet]
        public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetCourseSubjects(), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpGet("{id:Guid}")]
        public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(new GetCourseSubjectById(id), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }

        [HttpPost]
        public async Task<IActionResult> Create([FromBody] CreateCourseSubjectCommand createCourseSubjectCommand, CancellationToken cancellationToken = default)
        {
            var request = await _mediator.Send(createCourseSubjectCommand, cancellationToken);

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
        public async Task<IActionResult> Update([FromBody] UpdateCourseSubjectCommand updateCourseSubjectCommand, CancellationToken cancellationToken = default)
        {
            var response = await _mediator.Send(updateCourseSubjectCommand, cancellationToken);

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
            var response = await _mediator.Send(new DeleteCourseSubjectCommand(id), cancellationToken);

            if (response.Code == 200)
                return Ok(response);
            else
                return NotFound(response);
        }
    }
}
