using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Infrastructure.Repositories;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Internship_4_OOP.Api.Controllers;

public class TheResultPatternUserController(Mediator mediator) : ControllerBase
{
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateUserDto dto)
    {
        var result = await mediator.Send(CreateUserCommand.FromDto(dto));
        if (result.IsFailure)
            return BadRequest(result.Error?.Errors);
        
    }
}
