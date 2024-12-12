

using MediatR.Pipeline;

using ILogger = Serilog.ILogger;

namespace SampleCleanArchitecture.Application.Common.Behaviours
{
    public class LoggingBehaviour<TRequest> : IRequestPreProcessor<TRequest> where TRequest : notnull
    {
        private readonly ILogger _logger;
        public LoggingBehaviour(ILogger logger)
        {
            _logger = logger;
        }
        public async Task Process(TRequest request, CancellationToken cancellationToken)
        {
            _logger.Information($"Request: {request.GetType().Name} : {request}");
        }
    }
}
