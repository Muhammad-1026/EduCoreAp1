using EduCoreApi.Application.Feature.Students.Commands;
using EduCoreApi.Application.Feature.Students.Models;
using EduCoreApi.Application.Feature.Students.Queries;
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
    public async Task<List<GetStudentDto>> GetAll(CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetStudents(), cancellationToken);
    }

    [HttpGet("{id}")]
    public async Task<GetStudentDto> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetStudentByIdQuery(id), cancellationToken);
    }

    [HttpPost]
    public async Task<CreateStudentDto> Create([FromBody] CreateStudentCommand createStudentCommand, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(createStudentCommand, cancellationToken);
    }
}