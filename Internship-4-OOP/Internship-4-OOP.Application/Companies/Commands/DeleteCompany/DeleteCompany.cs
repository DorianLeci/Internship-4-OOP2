using Internship_4_OOP.Application.Common.Interfaces;
using Internship_4_OOP.Application.Companies.Commands.GetCompany;
using Internship_4_OOP.Application.DTO.CompanyDto;
using Internship_4_OOP.Domain.Common.Events.Company;
using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Entities.Users;
using Internship_4_OOP.Domain.Errors;
using Internship_4_OOP.Domain.Persistence.Company;
using Internship_4_OOP.Domain.Persistence.User;
using MediatR;

namespace Internship_4_OOP.Application.Companies.Commands.DeleteCompany;

public record DeleteCompanyCommand(int Id, string Username, string Password) : IRequest<Result<GetCompanyDto, IDomainError>>,IRequireAuthentification
{
    public User AuthenticatedUser { get;private set; }

    public static DeleteCompanyCommand FromRouteAndQuery(int id,string username, string password)
    {
        return new DeleteCompanyCommand(id,username, password);
    }
    

    public void SetAuthentificatedUser(User user)
    {
        AuthenticatedUser = user;
    }
}


public class DeleteCompanyCommandHandler(ICompanyRepository companyRepository,IMediator mediator,ICompanyDbContext dbContext) : IRequestHandler<DeleteCompanyCommand, Result<GetCompanyDto,IDomainError>>
{
    public async Task<Result<GetCompanyDto, IDomainError>> Handle(DeleteCompanyCommand request,
        CancellationToken cancellationToken)
    {
        var user = request.AuthenticatedUser;

        var company = await companyRepository.GetByIdAsync(request.Id);
        if (company == null)
            return Result<GetCompanyDto, IDomainError>.Failure(DomainError.NotFound("Kompanija s unesenim id-om ne postoji"));

        Console.WriteLine(user.CompanyId);
        if (company.Id!=user.CompanyId)
            return Result<GetCompanyDto, IDomainError>.Failure(DomainError.Unathorized("Kompanija nije povezana s korisnikom"));
        
        await companyRepository.DeleteAsync(company.Id);
        await dbContext.SaveChangesAsync(cancellationToken);
        
        company.AddDomainEvent(new CompanyDeletedEvent(3,"CompanyDeletedEvent",company.Id,DateTimeOffset.Now,company));
        await mediator.Publish(company.DomainEvents.Last());
            
        var companyDto=CompanyMapper.GetDtoFromCompany(company);
        return  Result<GetCompanyDto, IDomainError>.Success(companyDto);
    }


}