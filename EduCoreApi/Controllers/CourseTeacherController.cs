using EduCoreApi.Application.Feature.CourseSubjects.Commands;
using EduCoreApi.Application.Feature.CourseSubjects.Queries;
using EduCoreApi.Application.Feature.CourseTeachers.Commands;
using EduCoreApi.Application.Feature.CourseTeachers.Models;
using EduCoreApi.Application.Feature.CourseTeachers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class CourseTeacherController : ControllerBase
{
    private readonly IMediator _mediator;

    public CourseTeacherController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetCourseTeachers(), cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetCourseTeacherById(id), cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromBody] CreateCourseTeacherCommand createCourseTeacherCommand, CancellationToken cancellationToken = default)
    {
        var request = await _mediator.Send(createCourseTeacherCommand, cancellationToken);

        return Ok(request);

        //if (request.Code == 200)
        //{
        //    return Ok(request);
        //}
        //else
        //{
        //    return NotFound(request);
        //}
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromBody] UpdateCourseTeacherCommand updateCourseTeacherCommand, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(updateCourseTeacherCommand, cancellationToken);

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
        var response = await _mediator.Send(new DeleteCourseTeacherCommand(id), cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }
}
