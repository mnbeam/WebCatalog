﻿using FluentValidation.Results;

namespace WebCatalog.Logic.Common.Exceptions;

public class WebCatalogValidationException : Exception
{
    private WebCatalogValidationException()
        : base("One or more validation failures have occured")
    {
        Errors = new Dictionary<string, string[]>();
    }

    public WebCatalogValidationException(IEnumerable<ValidationFailure> failures)
        : this()
    {
        Errors = failures
            .GroupBy(f => f.PropertyName, f => f.ErrorMessage)
            .ToDictionary(failureGroup => failureGroup.Key, failureGroup => failureGroup.ToArray());
    }

    public WebCatalogValidationException(params string[]? errors) : this()
    {
        if (errors != null)
        {
            Errors.Add(string.Empty, errors);
        }
    }

    public IDictionary<string, string[]> Errors { get; }
}