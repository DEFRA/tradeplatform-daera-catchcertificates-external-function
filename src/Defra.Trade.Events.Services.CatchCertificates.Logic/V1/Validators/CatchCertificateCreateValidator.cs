// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.Services.CatchCertificates.Logic.Validators;
using FluentValidation;
using V1Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V1.Validators;

public class CatchCertificateCreateValidator : AbstractValidator<V1Inbound.CatchCertificateCaseCreateInbound>
{
    public CatchCertificateCreateValidator()
    {
        this.AddMmoEntityValidationRules<V1Inbound.CatchCertificateCaseCreateInbound, V1Inbound.ExporterInbound>();
    }
}
