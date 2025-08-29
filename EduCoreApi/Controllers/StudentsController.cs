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

        //return Ok(students);
    }

    [HttpGet("{id}")]
    public async Task<GetStudentDto> GetById(Guid id, CancellationToken cancellationToken = default)
    {
        return await _mediator.Send(new GetStudentByIdQuery(id), cancellationToken);
    }

    //[HttpPost]
    //public async Task<IActionResult> CreateStudent([FromBody] StudentCreateDto studentCreateDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    var createdStudent = await _studentService.CreateStudentAsync(studentCreateDto);
    //    return CreatedAtAction(nameof(GetStudentById), new { id = createdStudent.Id }, createdStudent);
    //}
    //[HttpPut("{id}")]
    //public async Task<IActionResult> UpdateStudent(int id, [FromBody] StudentUpdateDto studentUpdateDto)
    //{
    //    if (!ModelState.IsValid)
    //    {
    //        return BadRequest(ModelState);
    //    }
    //    var updatedStudent = await _studentService.UpdateStudentAsync(id, studentUpdateDto);
    //    if (updatedStudent == null)
    //    {
    //        return NotFound();
    //    }
    //    return Ok(updatedStudent);
    //}
    //[HttpDelete("{id}")]
    //public async Task<IActionResult> DeleteStudent(int id)
    //{
    //    var isDeleted = await _studentService.DeleteStudentAsync(id);
    //    if (!isDeleted)
    //    {
    //        return NotFound();
    //    }
    //    return NoContent();
    //}
}
