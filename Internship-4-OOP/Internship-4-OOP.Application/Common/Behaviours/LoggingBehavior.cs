using Internship_4_OOP.Application.DTO;
using MediatR.Pipeline;
using Microsoft.Extensions.Logging;

namespace Internship_4_OOP.Application.Common.Behaviours;

public class LoggingBehavior<TRequest> : IRequestPreProcessor<TRequest> where TRequest:notnull
{
    private readonly ILogger<TRequest> _logger;
    private readonly UserLoggingDTO _user;

    public LoggingBehavior(ILogger<TRequest> logger, UserLoggingDTO user)
    {
        _logger = logger;
        _user = user;
    }
    public async Task Process(TRequest request, CancellationToken cancellationToken)
    {
        var nameOfRequest=typeof(TRequest).Name;
        
        _logger.LogInformation("Request info:{nameOfRequest} {@_item}", nameOfRequest, request);
    }
}

