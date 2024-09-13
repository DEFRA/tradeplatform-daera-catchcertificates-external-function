// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Common.Functions.Validation;
using Defra.Trade.Events.Services.CatchCertificates.Logic.Dto;
using FluentValidation;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Validators;

public static class MmoEntityValidatorExtensions
{
    public static void AddMmoEntityValidationRules<TModel, TExporter>(this AbstractValidator<TModel> validator, IValidator<TExporter> exporterValidator = null)
        where TModel : IMmoEntity<TExporter>
    {
        validator.RuleFor(x => x.DocumentNumber).NotNull().WithMessage(ValidationMessages.NullField);

        validator.RuleFor(x => x.CorrelationId).NotNull().NotEmpty();

        validator.RuleFor(x => x.Exporter).NotNull();

        validator.RuleFor(x => x.CaseType1).NotNull().NotEmpty();

        validator.RuleFor(x => x.CaseType2).NotNull().NotEmpty();

        validator.RuleFor(x => x.NumberOfFailedSubmissions).NotNull();

        validator.RuleFor(x => x.RequestedByAdmin).NotNull();

        if (exporterValidator is not null)
        {
            validator.RuleFor(x => x.Exporter).SetValidator(exporterValidator);
        }
    }
}
