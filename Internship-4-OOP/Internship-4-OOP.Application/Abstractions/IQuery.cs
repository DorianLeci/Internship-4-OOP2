using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using MediatR;

namespace Internship_4_OOP.Application.Abstractions;

public interface IRequestBase{}
public interface IQuery<TResponse>:IRequestBase,IRequest<Result<TResponse,IDomainError>>
where  TResponse:notnull
{
    
}