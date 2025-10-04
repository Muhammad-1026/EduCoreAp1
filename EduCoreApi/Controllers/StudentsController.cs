using EduCoreApi.Application.Feature.Students.Commands;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Students.Queries;
using EduCoreApi.Application.Feature.Subjects.Commands;
using EduCoreApi.Application.Feature.Teachers.Commands;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace EduCoreApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class StudentsController : ControllerBase
{
    private readonly IMediator _mediator;

    public StudentsController(IMediator mediator)
    {
        _mediator = mediator;
    }

    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetStudents(), cancellationToken);

        if (response.Code == 1)
            return Ok(response);
        else
            return NotFound(response);
    }


    [HttpGet("{id:Guid}")]
    public async Task<IActionResult> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(new GetStudentById(id), cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpPost]
    public async Task<IActionResult> Create([FromForm] CreateStudentCommand createStudentCommand, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(createStudentCommand, cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpPut]
    public async Task<IActionResult> Update([FromForm] UpdateStudentCommand updateStudentCommand, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(updateStudentCommand, cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpDelete]
    public async Task<IActionResult> Delete([FromBody]  DeleteStudentCommand deleteStudentCommand, CancellationToken cancellationToken = default)
    {
        var response = await _mediator.Send(deleteStudentCommand, cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }
}