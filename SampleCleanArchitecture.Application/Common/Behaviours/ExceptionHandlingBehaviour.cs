using  ILogger = Serilog.ILogger;

namespace SampleCleanArchitecture.Application.Common.Behaviours
{
    public class ExceptionHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private ILogger _logger { get; set; }

        public ExceptionHandlingBehaviour(ILogger logger)
        {
                _logger = logger;
        }

        public async Task<TResponse> Handle(TRequest request, RequestHandlerDelegate<TResponse> next, CancellationToken cancellationToken)
        {
            try
            {
                return await next();
            }
            catch (Exception ex)
            {

                _logger.Error(ex, $" Unhandled Exception for Request {typeof(TRequest).Name} : {request}");

                throw;
            }
        }
    }
}
