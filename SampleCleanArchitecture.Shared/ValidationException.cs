﻿

using FluentValidation.Results;

namespace SampleCleanArchitecture.Shared
{
    public class ValidationException :Exception
    {
        public IDictionary<string, string[]> Errors { get; set; }

        public ValidationException():base("Doğrulama hatası")
        {}

        public ValidationException(IEnumerable<ValidationFailure> failures):this()
        {
            Errors = failures
            .GroupBy(e => e.PropertyName, e => e.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
        }
        
    }
}
