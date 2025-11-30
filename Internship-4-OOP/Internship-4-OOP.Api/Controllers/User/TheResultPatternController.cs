using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Application.Users.Commands.GetUserById;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Internship_4_OOP.Api.Controllers.User;

[ApiController]
[Route("api/users")]
public class TheResultPatternController(IMediator mediator) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var query = GetUserByIdQuery.FromId(id);
        var result=await mediator.Send(query);

        if (result.IsFailure)
            return NotFound(result.Error);
        
        return Ok(result.Value);

    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto dto)
    {

        var result = await mediator.Send(CreateUserCommand.FromDto(dto));
        if (result.IsFailure)
            return BadRequest(result.Error);

        var createdId = result.Value;
        return CreatedAtAction(nameof(GetByIdAsync),new {id=createdId},dto );
    }


}
