// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class AuditInbound
{
    public string AuditAt { get; set; }

    public string AuditOperation { get; set; }

    public string InvestigationStatus { get; set; }

    public string User { get; set; }
}
