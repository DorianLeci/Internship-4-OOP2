using Internship_4_OOP.Application.Abstractions;
using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.DTO;
using Internship_4_OOP.Application.DTO.CompanyDto;
using Internship_4_OOP.Application.Users.Commands.CreateUser;
using Internship_4_OOP.Domain.Common.Events.Company;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Company;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.Company;
using MediatR;

namespace Internship_4_OOP.Application.Companies.Commands.CreateCompany;

public record CreateCompanyCommand(string CompanyName ) : IRequest<Result<int, IDomainError>>,ICompanyRequest

{
    
    public static CreateCompanyCommand FromDto(CreateCompanyDto dto)
    {
        return new CreateCompanyCommand(dto.Name);
    }
}

public class CreateCompanyCommandHandler(ICompanyRepository companyRepository,IMediator mediator,ICompanyDbContext dbContext) : IRequestHandler<CreateCompanyCommand,Result<int,IDomainError>>
{

    public async Task<Result<int,IDomainError>> Handle(CreateCompanyCommand request, CancellationToken cancellationToken)
    {
        if (await companyRepository.ExistsByNameAsync(request.CompanyName))
        {
            return Result<int, IDomainError>.Failure(
                DomainError.Conflict("Već postoji kompanija s istim imenom."));
        }

        var newCompany = new Company(request.CompanyName);
        
        await companyRepository.InsertAsync(newCompany);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        newCompany.AddDomainEvent(new CompanyCreatedEvent(1,"CompanyCreatedEvent",newCompany.Id,DateTimeOffset.Now,newCompany));
        await mediator.Publish(newCompany.DomainEvents.Last());
        
        return Result<int,IDomainError>.Success(newCompany.Id);

    }
    
}