using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.DTO.UserDto;
using Internship_4_OOP.Application.Users.Commands.ActivateUser;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Application.Users.Commands.DeactivateUser;
using Internship_4_OOP.Application.Users.Commands.DeleteUserById;
using Internship_4_OOP.Application.Users.Commands.GetAllUsers;
using Internship_4_OOP.Application.Users.Commands.GetUserById;
using Internship_4_OOP.Application.Users.Commands.ImportExternal;
using Internship_4_OOP.Application.Users.Commands.UpdateUser;
using Internship_4_OOP.Application.Users.Mappers;
using Internship_4_OOP.Application.Users.Service;
using Internship_4_OOP.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Internship_4_OOP.Api.Controllers.User;

[ApiController]
[Route("api/users")]
public class TheResultPatternController(IMediator mediator,ExternalUsersService service) : ControllerBase
{
    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ActionName(nameof(GetByIdAsync))]
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id)
    {
        var query = GetUserByIdQuery.FromId(id);
        var result=await mediator.Send(query);

        if (result.IsFailure)
            return NotFound(result.Error);
        
        return Ok(result.Value);

    }

    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> GetAllUsersAync()
    {
        var command = new GetAllUsersQuery();
        var result = await mediator.Send(command);
        
        if(result.IsFailure)
            return NotFound(result.Error);
        
        return Ok(new {Id=result.Value});
        
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
        return CreatedAtAction(nameof(GetByIdAsync),new {id=createdId},new { id = createdId, dto });
    }

    [HttpPost("import-external")]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]

    public async Task<IActionResult> ImportExternalAsync()
    {
        var extUsers = await service.ListExternalUsersAsync();
        if(extUsers==null)
            return BadRequest("Vanjski API nije dostupan");

        var command = ImportExternalUsersCommand.FromExternalDto(extUsers.Select(ExternalUserMapper.GetDtoFromExternal).ToList());
        var result=await mediator.Send(command);

        if (result.IsFailure)
            return BadRequest(result.Error);
        
        var createdUsers=new List<GetUserDto>();
        
        foreach (var id in result.Value)
        {
            var userResult=await mediator.Send(GetUserByIdQuery.FromId(id));
            
            if (userResult.IsFailure)
                return BadRequest(userResult.Error);
            
            createdUsers.Add(userResult.Value);
        }
        
        return Created("api/users/import-external",createdUsers);
    }


    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteUserAsync([FromRoute] int id)
    {
        var result = await mediator.Send(new DeleteUserByIdCommand(id));

        if (result.IsFailure)
            return NotFound(result.Error);
        
        return Ok(new {Id=result.Value});
        
    }

    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]

    public async Task<IActionResult> UpdateUserAsync([FromRoute] int id,[FromBody] UpdateUserDto dto)
    {
        var command = UpdateUserCommand.FromDto(id, dto);
        var result=await mediator.Send(command);

        if (result.IsFailure)
            return result.Error!.ErrorType switch
            {
                ErrorType.NotFound => NotFound(result.Error),
                ErrorType.Conflict => Conflict(result.Error),
                _ => BadRequest(result.Error)
            };

        return Ok(new { Id = result.Value });

    }
    
    [HttpPut("deactivate/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]

    public async Task<IActionResult> DeactivateUserAsync([FromRoute] int id)
    {
        var command = new DeactivateUserCommand(id);
        var result=await mediator.Send(command);

        if (result.IsFailure)
            return result.Error!.ErrorType switch
            {
                ErrorType.NotFound => NotFound(result.Error),
                ErrorType.Conflict => Conflict(result.Error),
                _ => BadRequest(result.Error)
            };


        return Ok(new { Id = result.Value });

    }
    
    [HttpPut("activate/{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]

    public async Task<IActionResult> ActivateUserAsync([FromRoute] int id)
    {
        var command = new ActivateUserCommand(id);
        var result=await mediator.Send(command);

        if (result.IsFailure)
            return result.Error!.ErrorType switch
            {
                ErrorType.NotFound => NotFound(result.Error),
                ErrorType.Conflict => Conflict(result.Error),
                _ => BadRequest(result.Error)
            };

        return Ok(new { Id = result.Value });

    }


}
