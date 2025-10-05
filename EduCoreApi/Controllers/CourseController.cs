using EduCoreApi.Application.Feature.Courses.Commands;
using EduCoreApi.Application.Feature.Courses.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourseController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetCourses(), cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetCourseById(id), cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCuorseCommand createCuorseCommand, CancellationToken cancellationToken = default)
    {
        var request = await _mediator.Send(createCuorseCommand, cancellationToken);

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
    public async Task<IActionResult> Update([FromBody] UpdateCourseCommand updateCourseCommand, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(updateCourseCommand, cancellationToken);

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
        var response = await _mediator.Send(new DeleteCourseCommand(id), cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }
}