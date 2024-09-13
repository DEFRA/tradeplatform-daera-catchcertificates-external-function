// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.Services.CatchCertificates.Logic.Validators;
using FluentValidation;
using V1Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Validators;

public class ProcessingStatementCreateValidator : AbstractValidator<V1Inbound.ProcessingStatementCreateInbound>
{
    public ProcessingStatementCreateValidator()
    {
        this.AddMmoEntityValidationRules<V1Inbound.ProcessingStatementCreateInbound, V1Inbound.ExporterInbound>();

        RuleFor(x => x.PlantName).NotNull().NotEmpty();

        RuleFor(x => x.PersonResponsible).NotNull().NotEmpty();
    }
}
