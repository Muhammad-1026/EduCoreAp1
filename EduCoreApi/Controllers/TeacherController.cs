using EduCoreApi.Application.Common.Results;
using EduCoreApi.Application.Feature.Teachers.Commands;
using EduCoreApi.Application.Feature.Teachers.Models;
using EduCoreApi.Application.Feature.Teachers.Queries;
using MediatR;
using Microsoft.AspNetCore.Mvc;


namespace EduCoreApi.API.Controllers;

[Route("api/[controller]")]
[ApiController]
public class TeacherController(IMediator mediator) : ControllerBase
{
    [HttpGet]
    public async Task<IActionResult> GetAll(CancellationToken cancellationToken = default)
    {
        var response = await mediator.Send(new GetTeachers(), cancellationToken);

        if (response.Code == 200)
            return Ok(response);
        else
            return NotFound(response);
    }

    [HttpGet("{id:Guid}")]
    public async Task<ApiResponse<GetTeacherDto>> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await mediator.Send(new GetTeacherById(id), cancellationToken);

        if (response.Code == 200)
            return response;
        else
            return response;
    }

    [HttpPost]
    public async Task<ApiResponse<CreateTeacherDto>> Create([FromForm] CreateTeacherCommmand createTeacherCommand, CancellationToken cancellationToken = default)
    {
        var response = await mediator.Send(createTeacherCommand, cancellationToken);

        if (response.Code == 201)
            return response;
        else
            return response;
    }

    [HttpPut]
    public async Task<ApiResponse> Update([FromForm] UpdateTeacherCommand updateTeacherCommand, CancellationToken cancellationToken = default)
    {
        if (updateTeacherCommand.TeacherId != updateTeacherCommand.TeacherId)
        {
            return new ApiResponse
            {
                Code = 400,
                Message = "ID in the URL does not match ID in the body",
            };
        }

        var response = await mediator.Send(updateTeacherCommand, cancellationToken);

        if (response.Code == 200)
            return response;
        else
            return response;
    }

    [HttpDelete("{id:Guid}")]
    public async Task<ApiResponse> Delete(Guid id, CancellationToken cancellationToken = default)
    {
        var response = await mediator.Send(new DeleteTeacherCommand(id), cancellationToken);

        if (response.Code == 200)
            return response;
        else
            return response;
    }
}
