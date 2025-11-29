using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Common.Events.Company;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.Company;
using MediatR;

namespace Internship_4_OOP.Application.Companies.Commands.CreateCompany;

public record CreateCompanyCommand(
    string Name
) : IRequest<Result<int, DomainError>>

{
    
    public static CreateCompanyCommand FromDto(CreateCompanyDto dto)
    {
        return new CreateCompanyCommand(dto.Name);
    }
}

public class CreateCompanyCommandHandler(ICompanyRepository companyRepository,IMediator mediator,ICompanyDbContext dbContext) : IRequestHandler<CreateCompanyCommand,Result<int,DomainError>>
{

    public async Task<Result<int,DomainError>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        if (await companyRepository.ExistsByNameAsync(request.Name))
        {
            return Result<int, DomainError>.Failure(
                DomainError.Conflict("Već postoji kompanija s istim imenom."));
        }

        var newCompany = new Company(request.Name);
        
        await companyRepository.InsertAsync(newCompany);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        newCompany.AddDomainEvent(new CompanyCreatedEvent(1,"CompanyCreatedEvent",newCompany.Id,DateTimeOffset.Now,newCompany));
        await mediator.Publish(newCompany.DomainEvents.Last());
        
        return Result<int,DomainError>.Success(newCompany.Id);

    }
    
}