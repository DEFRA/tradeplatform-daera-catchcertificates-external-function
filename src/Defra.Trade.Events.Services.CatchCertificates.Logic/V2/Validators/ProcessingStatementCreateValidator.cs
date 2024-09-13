// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.Services.CatchCertificates.Logic.Validators;
using FluentValidation;
using V2Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Validators;

public class ProcessingStatementCreateValidator : AbstractValidator<V2Inbound.ProcessingStatementCreateInbound>
{
    public ProcessingStatementCreateValidator()
    {
        this.AddMmoEntityValidationRules<V2Inbound.ProcessingStatementCreateInbound, V2Inbound.ExporterInbound>();

        RuleFor(x => x.PlantName).NotNull().NotEmpty();

        RuleFor(x => x.PersonResponsible).NotNull().NotEmpty();
    }
}
