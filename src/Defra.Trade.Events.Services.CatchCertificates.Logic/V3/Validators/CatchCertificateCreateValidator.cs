// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using Defra.Trade.Events.Services.CatchCertificates.Logic.Validators;
using FluentValidation;
using V3Inbound = Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Dto.Inbound;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V3.Validators;

public class CatchCertificateCreateValidator : AbstractValidator<V3Inbound.CatchCertificateCaseCreateInbound>
{
    public CatchCertificateCreateValidator()
    {
        this.AddMmoEntityValidationRules<V3Inbound.CatchCertificateCaseCreateInbound, V3Inbound.ExporterInbound>();
    }
}
