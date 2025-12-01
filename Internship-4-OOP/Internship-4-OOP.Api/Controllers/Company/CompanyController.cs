using Internship_4_OOP.Application.Companies.Commands.CreateCompany;
using Internship_4_OOP.Application.Companies.Commands.DeleteCompany;
using Internship_4_OOP.Application.Companies.Commands.GetCompany;
using Internship_4_OOP.Application.Companies.Commands.UpdateCompany;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.DTO.CompanyDto;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Errors;
using MediatR;
using Microsoft.AspNetCore.Mvc;

namespace Internship_4_OOP.Api.Controllers.Company;

[ApiController]
[Route("api/companies")]
public class CompanyController(IMediator mediator) : ControllerBase
{

    [HttpGet("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status401Unauthorized)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ActionName(nameof(GetByIdAsync))]
    
    public async Task<IActionResult> GetByIdAsync([FromRoute] int id,[FromQuery] string username,[FromQuery] string password)
    {
        var query=GetCompanyByIdQuery.FromDto(id,username,password);
        var result = await mediator.Send(query);

        if (result.IsFailure)
        {
            return result.Error!.ErrorType switch
            {
                ErrorType.Unauthorized => Unauthorized(result.Error),
                ErrorType.NotFound => NotFound(result.Error),
                _ => BadRequest(result.Error)
            };
        }
        return Ok(result.Value);
    }
    
    [HttpGet]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ActionName(nameof(GetByIdAsync))]
    
    public async Task<IActionResult> GetWithoutIdAsync([FromQuery] string username,[FromQuery] string password)
    {
        var query=GetCompanyWithoutCompanyIdQuery.FromQuery(username,password);
        var result = await mediator.Send(query);

        if (result.IsFailure)
            return NotFound(result.Error);

        return Ok(result.Value);
    }
    
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    public async Task<IActionResult> CreateAsync([FromBody] CreateCompanyDto dto)
    {

        var result = await mediator.Send(CreateCompanyCommand.FromDto(dto));
        if (result.IsFailure)
            return BadRequest(result.Error);
        return Created(string.Empty,new {result.Value,dto});
    }
    
    [HttpPut("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    [ProducesResponseType(StatusCodes.Status409Conflict)]
    public async Task<IActionResult> UpdateAsync([FromRoute] int id,[FromBody] UpdateCompanyDto dto)
    {
        Console.WriteLine($"Request CompanyId: {id}");
        var result = await mediator.Send(UpdateCompanyCommand.FromRouteAndDto(id,dto));

        if (result.IsFailure)
            return result.Error!.ErrorType switch
            {
                ErrorType.NotFound => NotFound(result.Error),
                ErrorType.Conflict => Conflict(result.Error),
                _ => BadRequest(result.Error)
            };

        return Ok(new { Id = result.Value });
    }
    
    [HttpDelete("{id:int}")]
    [ProducesResponseType(StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> DeleteCompanyAsync([FromRoute] int id,[FromQuery] string username,[FromQuery] string password)
    {
        Console.WriteLine($"{username} {password}");
        var command=DeleteCompanyCommand.FromRouteAndQuery(id,username,password);
        Console.WriteLine($"{command.Username} {command.Password}");
        var result = await mediator.Send(command);

        if (result.IsFailure)
            return NotFound(result.Error);
        
        return Ok(new {Id=result.Value});
        
    }
}
