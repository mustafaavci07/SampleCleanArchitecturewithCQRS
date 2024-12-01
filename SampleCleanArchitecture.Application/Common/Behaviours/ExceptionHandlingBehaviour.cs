﻿using  Logger = Serilog.ILogger;

namespace SampleCleanArchitecture.Application.Common.Behaviours
{
    public class ExceptionHandlingBehaviour<TRequest, TResponse> : IPipelineBehavior<TRequest, TResponse> where TRequest : notnull
    {
        private Logger _logger { get; set; }

        public ExceptionHandlingBehaviour(Logger logger)
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

                _logger.LogError(ex, $" Unhandled Exception for Request {typeof(TRequest).Name} : {request}");

                throw;
            }
        }
    }
}
