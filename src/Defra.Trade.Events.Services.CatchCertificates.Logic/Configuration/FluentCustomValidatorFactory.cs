// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Threading.Tasks;
using Defra.Trade.Common.Functions.Interfaces;
using FluentValidation;
using FluentValidation.Results;
using Microsoft.Extensions.DependencyInjection;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Configuration;

public sealed class FluentValidatorFactory(IServiceProvider services) : ICustomValidatorFactory
{
    private readonly IServiceProvider _services = services;

    public IValidator CreateInstance(Type validatorType)
    {
        return (IValidator)_services.GetRequiredService(validatorType);
    }

    public IValidator<T> CreateInstance<T>()
    {
        return _services.GetRequiredService<IValidator<T>>();
    }

    public Task<ValidationResult> Validate<T>(T model)
    {
        return CreateInstance<T>().ValidateAsync(model);
    }

    public Task ValidateAndThrow<T>(T model)
    {
        return CreateInstance<T>().ValidateAndThrowAsync<T>(model);
    }
}
