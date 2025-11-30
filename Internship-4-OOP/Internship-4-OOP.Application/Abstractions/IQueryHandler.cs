using Internship_4_OOP.Domain.Common.Model;
using Internship_4_OOP.Domain.Errors;
using MediatR;

namespace Internship_4_OOP.Application.Abstractions;

public interface IQueryHandler<TRequest, TResponse> : IRequestHandler<TRequest,Result<TResponse,IDomainError>>
    where TRequest : IQuery<TResponse>
    where TResponse : notnull
{
    
}