// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Linq;
using System.Reflection;
using AutoFixture;
using AutoMapper;
using Defra.Trade.Catch.Certificate.Internal.V2INTERNAL.ApiClient.Model;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;
using Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Mappers;
using FluentAssertions;
using FluentAssertions.Equivalency;
using Xunit;
using Catch_ = Defra.Trade.Catch.Certificate.Internal.V2INTERNAL.ApiClient.Model.Catch;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.Tests.V2.Mappers;

public class FesApiProfileTests
{
    private readonly Fixture _fixture;
    private readonly IMapper _sut;

    public FesApiProfileTests()
    {
        _sut = new MapperConfiguration(opt =>
        {
            opt.AddProfile<FesApiProfile>();
        }).CreateMapper();
        _fixture = new Fixture();
    }

    [Fact]
    public void FesApiProfile_Maps_CatchCertificateCase()
    {
        // arrange
        var input = _fixture.Create<CatchCertificateCaseCreateInbound>();
        var expected = Create<CatchCertificateCase>();
        expected.ExportedTo = Create<Country>();
        expected.Exporter = Create<Exporter>();
        expected.Exporter.Address = Create<Address>();
        expected.Exporter.DynamicsAddress = Create<DynamicsAddress>();
        expected.Transportation = Create<Transportation>();
        expected.CaseType1 = input.CaseType1;
        expected.CaseType2 = input.CaseType2;
        expected.CertStatus = input.CertStatus;
        expected.CorrelationId = input.CorrelationId.Value;
        expected.Da = input.DA;
        expected.DocumentDate = input.DocumentDate;
        expected.DocumentNumber = input.DocumentNumber;
        expected.DocumentUrl = input.DocumentUrl.ToString();
        expected.ExportedTo.IsoCodeAlpha2 = input.ExportedTo.IsoCodeAlpha2;
        expected.ExportedTo.IsoCodeAlpha3 = input.ExportedTo.IsoCodeAlpha3;
        expected.ExportedTo.IsoNumericCode = input.ExportedTo.IsoNumericCode;
        expected.ExportedTo.OfficialCountryName = input.ExportedTo.OfficialCountryName;
        expected.Exporter.AccountId = input.Exporter.AccountId;
        expected.Exporter.Address.BuildingName = input.Exporter.Address.BuildingName;
        expected.Exporter.Address.BuildingNumber = input.Exporter.Address.BuildingNumber;
        expected.Exporter.Address.City = input.Exporter.Address.City;
        expected.Exporter.Address.Country = input.Exporter.Address.Country;
        expected.Exporter.Address.County = input.Exporter.Address.County;
        expected.Exporter.Address.Line1 = input.Exporter.Address.Line1;
        expected.Exporter.Address.PostCode = input.Exporter.Address.PostCode;
        expected.Exporter.Address.StreetName = input.Exporter.Address.StreetName;
        expected.Exporter.Address.SubBuildingName = input.Exporter.Address.SubBuildingName;
        expected.Exporter.CompanyName = input.Exporter.CompanyName;
        expected.Exporter.ContactId = input.Exporter.ContactId;
        expected.Exporter.DynamicsAddress.DefraAddressid = input.Exporter.DynamicsAddress.DefraAddressId;
        expected.Exporter.DynamicsAddress.DefraBuildingname = input.Exporter.DynamicsAddress.DefraBuildingName;
        expected.Exporter.DynamicsAddress.DefraCountryValue = input.Exporter.DynamicsAddress.DefraCountryValue;
        expected.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCRMAssociatednavigationproperty = input.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCrmAssociatedNavigationProperty;
        expected.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCRMLookuplogicalname = input.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCrmLookupLogicalname;
        expected.Exporter.DynamicsAddress.DefraCounty = input.Exporter.DynamicsAddress.DefraCounty;
        expected.Exporter.DynamicsAddress.DefraDependentlocality = input.Exporter.DynamicsAddress.DefraDependentLocality;
        expected.Exporter.DynamicsAddress.DefraLocality = input.Exporter.DynamicsAddress.DefraLocality;
        expected.Exporter.DynamicsAddress.DefraFromcompanieshouse = input.Exporter.DynamicsAddress.DefraFromCompaniesHouse;
        expected.Exporter.DynamicsAddress.DefraInternationalpostalcode = input.Exporter.DynamicsAddress.DefraInternationalPostalCode;
        expected.Exporter.DynamicsAddress.DefraPostcode = input.Exporter.DynamicsAddress.DefraPostcode;
        expected.Exporter.DynamicsAddress.DefraPremises = input.Exporter.DynamicsAddress.DefraPremises;
        expected.Exporter.DynamicsAddress.DefraStreet = input.Exporter.DynamicsAddress.DefraStreet;
        expected.Exporter.DynamicsAddress.DefraSubbuildingname = input.Exporter.DynamicsAddress.DefraSubBuildingName;
        expected.Exporter.DynamicsAddress.DefraTowntext = input.Exporter.DynamicsAddress.DefraTownText;
        expected.Exporter.DynamicsAddress.DefraUprn = input.Exporter.DynamicsAddress.DefraUprn;
        expected.Exporter.FullName = input.Exporter.FullName;
        expected.IsDirectLanding = input.IsDirectLanding;
        expected.IsUnblocked = input.IsUnblocked;
        expected.NumberOfFailedSubmissions = input.NumberOfFailedSubmissions.Value;
        expected.RequestedByAdmin = input.RequestedByAdmin.Value;
        expected._Version = input.Version;
        expected.Exporter.DynamicsAddress.DefraFromcompanieshouseODataCommunityDisplayV1FormattedValue = input.Exporter.DynamicsAddress.DefraFromCompaniesHouseODataCommunityDisplayV1FormattedValue;
        expected.Exporter.DynamicsAddress.DefraCountryValueODataCommunityDisplayV1FormattedValue = input.Exporter.DynamicsAddress.DefraCountryValueODataCommunityDisplayV1FormattedValue;
        expected.FailureIrrespectiveOfRisk = input.FailureIrrespectiveOfRisk.Value;
        expected.VesselOverriddenByAdmin = input.VesselOverriddenByAdmin;
        expected.SpeciesOverriddenByAdmin = input.SpeciesOverriddenByAdmin;
        expected.MultiVesselSchedule = input.MultiVesselSchedule.Value;
        expected.Transportation.ModeofTransport = (ModeOfTransport?)(int?)input.Transportation.ModeOfTransport;
        expected.Transportation.ExportDate = input.Transportation.ExportDate;
        expected.Transportation.HasRoadTransportDocument = input.Transportation.HasRoadTransportDocument;
        expected.Transportation.Nationality = input.Transportation.Nationality;
        expected.Transportation.Registration = input.Transportation.Registration;
        expected.Transportation.ExportLocation = input.Transportation.ExportLocation;
        expected.Transportation.BillNumber = input.Transportation.BillNumber;
        expected.Transportation.FlightNumber = input.Transportation.FlightNumber;
        expected.Transportation.ContainerId = input.Transportation.ContainerId;
        expected.Transportation.Name = input.Transportation.Name;
        expected.Transportation.Flag = input.Transportation.Flag;
        expected.LastUpdated = input.DocumentDate.Value;
        expected.LastUpdatedBy = "FES";
        expected.LastUpdatedSystem = "FES";
        expected.Audits = input.Audits.Select(a =>
        {
            var expected = Create<Audit>();
            expected.AuditAt = a.AuditAt;
            expected.AuditOperation = a.AuditOperation;
            expected.InvestigationStatus = a.InvestigationStatus;
            expected.User = a.User;
            return expected;
        }).ToList();
        expected.Landings = input.Landings.Select(i =>
        {
            var expected = Create<Landing>();
            expected.Risking = Create<Risk>();
            expected.Validation = Create<LandingValidation>();
            expected.CnCode = i.CnCode;
            expected.CommodityCodeDescription = i.CommodityCodeDescription;
            expected.Id = i.Id;
            expected.Is14DayLimitReached = i.Is14DayLimitReached;
            expected.LandingDate = i.LandingDate;
            expected.LicenceHolder = i.LicenceHolder;
            expected.NumberOfTotalSubmissions = i.NumberOfTotalSubmissions;
            expected.Presentation = i.Presentation;
            expected.Risking.ExporterRiskScore = i.Risking.ExporterRiskScore;
            expected.Risking.LandingRiskScore = i.Risking.LandingRiskScore;
            expected.Risking.HighOrLowRisk = i.Risking.HighOrLowRisk;
            expected.Risking.IsSpeciesRiskEnabled = i.Risking.IsSpeciesRiskEnabled;
            expected.Risking.OveruseInfo = i.Risking.OveruseInfo.ToList();
            expected.Risking.SpeciesRisk = i.Risking.SpeciesRisk;
            expected.Risking.Vessel = i.Risking.Vessel;
            expected.ScientificName = i.ScientificName;
            expected.Source = i.Source;
            expected.Species = i.Species;
            expected.SpeciesAlias = i.SpeciesAlias;
            expected.State = i.State;
            expected.Status = i.Status;
            expected.Validation.IsLegallyDue = i.Validation.IsLegallyDue;
            expected.Validation.LandedWeightExceededBy = i.Validation.LandedWeightExceededBy;
            expected.Validation.LiveExportWeight = (double)i.Validation.LiveExportWeight;
            expected.Validation.RawLandingsUrl = i.Validation.RawLandingsUrl.ToString();
            expected.Validation.SalesNoteUrl = i.Validation.SalesNoteUrl.ToString();
            expected.Validation.TotalEstimatedForExportSpecies = i.Validation.TotalEstimatedForExportSpecies;
            expected.Validation.TotalEstimatedWithTolerance = i.Validation.TotalEstimatedWithTolerance;
            expected.Validation.TotalLiveForExportSpecies = i.Validation.TotalLiveForExportSpecies;
            expected.Validation.TotalRecordedAgainstLanding = i.Validation.TotalRecordedAgainstLanding;
            expected.Validation.TotalWeightForSpecies = i.Validation.TotalWeightForSpecies;
            expected.WasValidationDueAtPointOfApplication = i.WasValidationDueAtPointOfApplication;
            expected.VesselName = i.VesselName;
            expected.VesselPln = i.VesselPln;
            expected.VesselLength = i.VesselLength;
            expected.Weight = (double)i.Weight;
            expected.VesselOverriddenByAdmin = i.VesselOverriddenByAdmin;
            expected.SpeciesOverriddenByAdmin = i.SpeciesOverriddenByAdmin;
            expected.AdminCommodityCode = i.AdminCommodityCode;
            expected.VesselAdministration = i.VesselAdministration;
            expected.Flag = i.Flag;
            expected.HomePort = i.HomePort;
            expected.CatchArea = i.CatchArea;
            expected.FishingLicenceNumber = i.FishingLicenceNumber;
            expected.FishingLicenceValidTo = i.FishingLicenceValidTo;
            expected.Imo = i.Imo;
            expected.SpeciesAnomaly = i.SpeciesAnomaly;
            expected.DataEverExpected = i.DataEverExpected;
            expected.LandingDataExpectedDate = i.LandingDataExpectedDate;
            expected.LandingDataEndDate = i.LandingDataEndDate;
            expected.LandingDataExpectedAtSubmission = i.LandingDataExpectedAtSubmission;
            expected.IsLate = i.IsLate;
            expected.AdminSpecies = i.AdminSpecies;
            expected.AdminState = i.AdminState;
            expected.AdminPresentation = i.AdminPresentation;
            return expected;
        }).ToList();

        // act
        var actual = _sut.Map<CatchCertificateCase>(input);

        // assert
        actual.Should().BeEquivalentTo(expected, opt => CompareAllByMember(opt));
    }

    [Fact]
    public void FesApiProfile_Maps_EmptyCatchCertificateCase()
    {
        // arrange
        var input = new CatchCertificateCaseCreateInbound();

        // act
        var actual = _sut.Map<CatchCertificateCase>(input);

        // assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public void FesApiProfile_Maps_EmptyProcessingStatement()
    {
        // arrange
        var input = new ProcessingStatementCreateInbound();

        // act
        var actual = _sut.Map<ProcessingStatement>(input);

        // assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public void FesApiProfile_Maps_EmptyStorageDocument()
    {
        // arrange
        var input = new StorageDocumentCreateInbound();

        // act
        var actual = _sut.Map<StorageDocument>(input);

        // assert
        actual.Should().NotBeNull();
    }

    [Fact]
    public void FesApiProfile_Maps_ProcessingStatement()
    {
        // arrange
        var input = _fixture.Create<ProcessingStatementCreateInbound>();
        var expected = Create<ProcessingStatement>();
        expected.ExportedTo = Create<Country>();
        expected.Exporter = Create<Exporter>();
        expected.Exporter.Address = Create<Address>();
        expected.Exporter.DynamicsAddress = Create<DynamicsAddress>();
        expected.Authority = Create<Authority>();
        expected.Authority.Address = Create<Address>();
        expected.PlantAddress = Create<Address>();
        expected.Da = input.DA;
        expected.DocumentNumber = input.DocumentNumber;
        expected.CaseType1 = input.CaseType1;
        expected.CaseType2 = input.CaseType2;
        expected.ExportedTo.OfficialCountryName = input.ExportedTo.OfficialCountryName;
        expected.ExportedTo.IsoCodeAlpha2 = input.ExportedTo.IsoCodeAlpha2;
        expected.ExportedTo.IsoCodeAlpha3 = input.ExportedTo.IsoCodeAlpha3;
        expected.ExportedTo.IsoNumericCode = input.ExportedTo.IsoNumericCode;
        expected.CorrelationId = input.CorrelationId.Value;
        expected.DocumentDate = input.DocumentDate;
        expected.DocumentUrl = input.DocumentUrl.ToString();
        expected.Exporter.ContactId = input.Exporter.ContactId;
        expected.Exporter.AccountId = input.Exporter.AccountId;
        expected.Exporter.FullName = input.Exporter.FullName;
        expected.Exporter.CompanyName = input.Exporter.CompanyName;
        expected.Exporter.Address.BuildingNumber = input.Exporter.Address.BuildingNumber;
        expected.Exporter.Address.SubBuildingName = input.Exporter.Address.SubBuildingName;
        expected.Exporter.Address.BuildingName = input.Exporter.Address.BuildingName;
        expected.Exporter.Address.StreetName = input.Exporter.Address.StreetName;
        expected.Exporter.Address.County = input.Exporter.Address.County;
        expected.Exporter.Address.Country = input.Exporter.Address.Country;
        expected.Exporter.Address.Line1 = input.Exporter.Address.Line1;
        expected.Exporter.Address.City = input.Exporter.Address.City;
        expected.Exporter.Address.PostCode = input.Exporter.Address.PostCode;
        expected.Exporter.DynamicsAddress.DefraAddressid = input.Exporter.DynamicsAddress.DefraAddressId;
        expected.Exporter.DynamicsAddress.DefraBuildingname = input.Exporter.DynamicsAddress.DefraBuildingName;
        expected.Exporter.DynamicsAddress.DefraCounty = input.Exporter.DynamicsAddress.DefraCounty;
        expected.Exporter.DynamicsAddress.DefraDependentlocality = input.Exporter.DynamicsAddress.DefraDependentLocality;
        expected.Exporter.DynamicsAddress.DefraFromcompanieshouse = input.Exporter.DynamicsAddress.DefraFromCompaniesHouse;
        expected.Exporter.DynamicsAddress.DefraInternationalpostalcode = input.Exporter.DynamicsAddress.DefraInternationalPostalCode;
        expected.Exporter.DynamicsAddress.DefraLocality = input.Exporter.DynamicsAddress.DefraLocality;
        expected.Exporter.DynamicsAddress.DefraPostcode = input.Exporter.DynamicsAddress.DefraPostcode;
        expected.Exporter.DynamicsAddress.DefraPremises = input.Exporter.DynamicsAddress.DefraPremises;
        expected.Exporter.DynamicsAddress.DefraStreet = input.Exporter.DynamicsAddress.DefraStreet;
        expected.Exporter.DynamicsAddress.DefraSubbuildingname = input.Exporter.DynamicsAddress.DefraSubBuildingName;
        expected.Exporter.DynamicsAddress.DefraTowntext = input.Exporter.DynamicsAddress.DefraTownText;
        expected.Exporter.DynamicsAddress.DefraUprn = input.Exporter.DynamicsAddress.DefraUprn;
        expected.Exporter.DynamicsAddress.DefraCountryValue = input.Exporter.DynamicsAddress.DefraCountryValue;
        expected.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCRMAssociatednavigationproperty = input.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCrmAssociatedNavigationProperty;
        expected.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCRMLookuplogicalname = input.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCrmLookupLogicalname;
        expected.ExporterId = input.ExporterId;
        expected.PersonResponsible = input.PersonResponsible;
        expected.PlantName = input.PlantName;
        expected.ProcessedFisheryProducts = input.ProcessedFisheryProducts;
        expected.NumberOfFailedSubmissions = input.NumberOfFailedSubmissions.Value;
        expected.RequestedByAdmin = input.RequestedByAdmin.Value;
        expected.Exporter.DynamicsAddress.DefraFromcompanieshouseODataCommunityDisplayV1FormattedValue = input.Exporter.DynamicsAddress.DefraFromCompaniesHouseODataCommunityDisplayV1FormattedValue;
        expected.Exporter.DynamicsAddress.DefraCountryValueODataCommunityDisplayV1FormattedValue = input.Exporter.DynamicsAddress.DefraCountryValueODataCommunityDisplayV1FormattedValue;
        expected.LastUpdated = input.DocumentDate;
        expected.Authority.Address.BuildingNumber = input.Authority.Address.BuildingNumber;
        expected.Authority.Address.SubBuildingName = input.Authority.Address.SubBuildingName;
        expected.Authority.Address.BuildingName = input.Authority.Address.BuildingName;
        expected.Authority.Address.StreetName = input.Authority.Address.StreetName;
        expected.Authority.Address.County = input.Authority.Address.County;
        expected.Authority.Address.Country = input.Authority.Address.Country;
        expected.Authority.Address.Line1 = input.Authority.Address.Line1;
        expected.Authority.Address.City = input.Authority.Address.City;
        expected.Authority.Address.PostCode = input.Authority.Address.PostCode;
        expected.Authority.CompanyName = input.Authority.CompanyName;
        expected.Authority.DateIssued = input.Authority.DateIssued;
        expected.Authority.Email = input.Authority.Email;
        expected.Authority.Name = input.Authority.Name;
        expected.Authority.Tel = input.Authority.Tel;
        expected.HealthCertificateDate = input.HealthCertificateDate;
        expected.HealthCertificateNumber = input.HealthCertificateNumber;
        expected.PlantAddress.Line1 = input.PlantAddress.Line1;
        expected.PlantAddress.SubBuildingName = input.PlantAddress.SubBuildingName;
        expected.PlantAddress.BuildingNumber = input.PlantAddress.BuildingNumber;
        expected.PlantAddress.BuildingName = input.PlantAddress.BuildingName;
        expected.PlantAddress.StreetName = input.PlantAddress.StreetName;
        expected.PlantAddress.County = input.PlantAddress.County;
        expected.PlantAddress.Country = input.PlantAddress.Country;
        expected.PlantAddress.City = input.PlantAddress.City;
        expected.PlantAddress.PostCode = input.PlantAddress.PostCode;
        expected.PlantApprovalNumber = input.PlantApprovalNumber;
        expected.PlantDateOfAcceptance = input.PlantDateOfAcceptance;
        expected._Version = input.Version;
        expected.LastUpdatedBy = "FES";
        expected.LastUpdatedSystem = "FES";
        expected.Catches = input.Catches.Select(c =>
        {
            var expected = Create<Catch_>();
            expected.Validation = Create<CatchValidation>();
            expected.ForeignCatchCertificateNumber = c.ForeignCatchCertificateNumber;
            expected.Species = c.Species;
            expected.Id = c.Id;
            expected.ScientificName = c.ScientificName;
            expected.CnCode = c.CnCode;
            expected.ImportedWeight = (double)c.ImportedWeight;
            expected.UsedWeightAgainstCertificate = (double)c.UsedWeightAgainstCertificate;
            expected.ProcessedWeight = (double)c.ProcessedWeight;
            expected.Validation.Status = c.Validation.Status;
            expected.Validation.TotalUsedWeightAgainstCertificate = (double)c.Validation.TotalUsedWeightAgainstCertificate;
            expected.Validation.WeightExceededAmount = c.Validation.WeightExceededAmount;
            expected.Validation.OveruseInfo = [.. c.Validation.OveruseInfo];
            return expected;
        }).ToList();

        // act
        var actual = _sut.Map<ProcessingStatement>(input);

        // assert
        actual.Should().BeEquivalentTo(expected, opt => CompareAllByMember(opt));
    }

    [Fact]
    public void FesApiProfile_Maps_StorageDocument()
    {
        // arrange
        var input = _fixture.Create<StorageDocumentCreateInbound>();
        var expected = Create<StorageDocument>();
        expected.ExportedTo = Create<Country>();
        expected.Exporter = Create<Exporter>();
        expected.Exporter.Address = Create<Address>();
        expected.Exporter.DynamicsAddress = Create<DynamicsAddress>();
        expected.Authority = Create<Authority>();
        expected.Authority.Address = Create<Address>();
        expected.Transportation = Create<Transportation>();
        expected.Da = input.DA;
        expected.DocumentNumber = input.DocumentNumber;
        expected.CaseType1 = input.CaseType1;
        expected.CaseType2 = input.CaseType2;
        expected.ExportedTo.OfficialCountryName = input.ExportedTo.OfficialCountryName;
        expected.ExportedTo.IsoCodeAlpha2 = input.ExportedTo.IsoCodeAlpha2;
        expected.ExportedTo.IsoCodeAlpha3 = input.ExportedTo.IsoCodeAlpha3;
        expected.ExportedTo.IsoNumericCode = input.ExportedTo.IsoNumericCode;
        expected.CorrelationId = input.CorrelationId.Value;
        expected.CompanyName = input.CompanyName;
        expected.DocumentDate = input.DocumentDate;
        expected.DocumentUrl = input.DocumentUrl.ToString();
        expected.Exporter.ContactId = input.Exporter.ContactId;
        expected.Exporter.AccountId = input.Exporter.AccountId;
        expected.Exporter.FullName = input.Exporter.FullName;
        expected.Exporter.CompanyName = input.Exporter.CompanyName;
        expected.Exporter.Address.BuildingNumber = input.Exporter.Address.BuildingNumber;
        expected.Exporter.Address.SubBuildingName = input.Exporter.Address.SubBuildingName;
        expected.Exporter.Address.BuildingName = input.Exporter.Address.BuildingName;
        expected.Exporter.Address.StreetName = input.Exporter.Address.StreetName;
        expected.Exporter.Address.County = input.Exporter.Address.County;
        expected.Exporter.Address.Country = input.Exporter.Address.Country;
        expected.Exporter.Address.Line1 = input.Exporter.Address.Line1;
        expected.Exporter.Address.City = input.Exporter.Address.City;
        expected.Exporter.Address.PostCode = input.Exporter.Address.PostCode;
        expected.Exporter.DynamicsAddress.DefraAddressid = input.Exporter.DynamicsAddress.DefraAddressId;
        expected.Exporter.DynamicsAddress.DefraBuildingname = input.Exporter.DynamicsAddress.DefraBuildingName;
        expected.Exporter.DynamicsAddress.DefraCounty = input.Exporter.DynamicsAddress.DefraCounty;
        expected.Exporter.DynamicsAddress.DefraDependentlocality = input.Exporter.DynamicsAddress.DefraDependentLocality;
        expected.Exporter.DynamicsAddress.DefraFromcompanieshouse = input.Exporter.DynamicsAddress.DefraFromCompaniesHouse;
        expected.Exporter.DynamicsAddress.DefraInternationalpostalcode = input.Exporter.DynamicsAddress.DefraInternationalPostalCode;
        expected.Exporter.DynamicsAddress.DefraLocality = input.Exporter.DynamicsAddress.DefraLocality;
        expected.Exporter.DynamicsAddress.DefraPostcode = input.Exporter.DynamicsAddress.DefraPostcode;
        expected.Exporter.DynamicsAddress.DefraPremises = input.Exporter.DynamicsAddress.DefraPremises;
        expected.Exporter.DynamicsAddress.DefraStreet = input.Exporter.DynamicsAddress.DefraStreet;
        expected.Exporter.DynamicsAddress.DefraSubbuildingname = input.Exporter.DynamicsAddress.DefraSubBuildingName;
        expected.Exporter.DynamicsAddress.DefraTowntext = input.Exporter.DynamicsAddress.DefraTownText;
        expected.Exporter.DynamicsAddress.DefraUprn = input.Exporter.DynamicsAddress.DefraUprn;
        expected.Exporter.DynamicsAddress.DefraCountryValue = input.Exporter.DynamicsAddress.DefraCountryValue;
        expected.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCRMAssociatednavigationproperty = input.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCrmAssociatedNavigationProperty;
        expected.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCRMLookuplogicalname = input.Exporter.DynamicsAddress.DefraCountryValueMicrosoftDynamicsCrmLookupLogicalname;
        expected.ExporterId = input.ExporterId;
        expected.NumberOfFailedSubmissions = input.NumberOfFailedSubmissions.Value;
        expected.RequestedByAdmin = input.RequestedByAdmin.Value;
        expected.Exporter.DynamicsAddress.DefraFromcompanieshouseODataCommunityDisplayV1FormattedValue = input.Exporter.DynamicsAddress.DefraFromCompaniesHouseODataCommunityDisplayV1FormattedValue;
        expected.Exporter.DynamicsAddress.DefraCountryValueODataCommunityDisplayV1FormattedValue = input.Exporter.DynamicsAddress.DefraCountryValueODataCommunityDisplayV1FormattedValue;
        expected.LastUpdated = input.DocumentDate.Value;
        expected.Authority.Address.BuildingNumber = input.Authority.Address.BuildingNumber;
        expected.Authority.Address.SubBuildingName = input.Authority.Address.SubBuildingName;
        expected.Authority.Address.BuildingName = input.Authority.Address.BuildingName;
        expected.Authority.Address.StreetName = input.Authority.Address.StreetName;
        expected.Authority.Address.County = input.Authority.Address.County;
        expected.Authority.Address.Country = input.Authority.Address.Country;
        expected.Authority.Address.Line1 = input.Authority.Address.Line1;
        expected.Authority.Address.City = input.Authority.Address.City;
        expected.Authority.Address.PostCode = input.Authority.Address.PostCode;
        expected.Authority.CompanyName = input.Authority.CompanyName;
        expected.Authority.DateIssued = input.Authority.DateIssued;
        expected.Authority.Email = input.Authority.Email;
        expected.Authority.Name = input.Authority.Name;
        expected.Authority.Tel = input.Authority.Tel;
        expected.Transportation.ModeofTransport = (ModeOfTransport?)(int?)input.Transportation.ModeOfTransport;
        expected.Transportation.ExportDate = input.Transportation.ExportDate;
        expected.Transportation.HasRoadTransportDocument = input.Transportation.HasRoadTransportDocument;
        expected.Transportation.Nationality = input.Transportation.Nationality;
        expected.Transportation.Registration = input.Transportation.Registration;
        expected.Transportation.ExportLocation = input.Transportation.ExportLocation;
        expected.Transportation.BillNumber = input.Transportation.BillNumber;
        expected.Transportation.FlightNumber = input.Transportation.FlightNumber;
        expected.Transportation.ContainerId = input.Transportation.ContainerId;
        expected.Transportation.Name = input.Transportation.Name;
        expected.Transportation.Flag = input.Transportation.Flag;
        expected._Version = input.Version;

        expected.LastUpdatedBy = "FES";
        expected.LastUpdatedSystem = "FES";
        expected.Products = input.Products.Select(c =>
        {
            var expected = Create<Product>();
            expected.Validation = Create<ProductValidation>();
            expected.ForeignCatchCertificateNumber = c.ForeignCatchCertificateNumber;
            expected.Species = c.Species;
            expected.Id = c.Id;
            expected.ScientificName = c.ScientificName;
            expected.CnCode = c.CnCode;
            expected.ImportedWeight = (double)c.ImportedWeight;
            expected.ExportedWeight = (double)c.ExportedWeight;
            expected.Validation.Status = c.Validation.Status;
            expected.Validation.WeightExceededAmount = c.Validation.WeightExceededAmount;
            expected.Validation.TotalWeightExported = (double)c.Validation.TotalWeightExported;
            expected.Validation.OveruseInfo = c.Validation.OveruseInfo.ToList();
            expected.DateOfUnloading = c.DateOfUnloading;
            expected.PlaceOfUnloading = c.PlaceOfUnloading;
            expected.TransportUnloadedFrom = c.TransportUnloadedFrom;
            return expected;
        }).ToList();
        expected.StorageFacilities = input.StorageFacilities.Select(c =>
        {
            var expected = Create<StorageFacility>();
            expected.Address = Create<Address>();
            expected.Address.Line1 = c.Address.Line1;
            expected.Address.SubBuildingName = c.Address.SubBuildingName;
            expected.Address.BuildingNumber = c.Address.BuildingNumber;
            expected.Address.BuildingName = c.Address.BuildingName;
            expected.Address.StreetName = c.Address.StreetName;
            expected.Address.County = c.Address.County;
            expected.Address.Country = c.Address.Country;
            expected.Address.City = c.Address.City;
            expected.Address.PostCode = c.Address.PostCode;
            expected.Name = c.Name;
            return expected;
        }).ToList();

        // act
        var actual = _sut.Map<StorageDocument>(input);

        // assert
        actual.Should().BeEquivalentTo(expected, opt => CompareAllByMember(opt));
    }

    private static EquivalencyAssertionOptions<T> CompareAllByMember<T>(EquivalencyAssertionOptions<T> opt)
    {
        foreach (var type in typeof(ProcessingStatement).Assembly.GetTypes())
        {
            opt.ComparingByMembers(type);
        }

        return opt;
    }

    private static T Create<T>()
    {
        var ctors = typeof(T).GetConstructors(BindingFlags.Public | BindingFlags.NonPublic | BindingFlags.Instance);
        var ctor = Array.Find(ctors, c => c.GetParameters().Length == 0) ?? Array.Find(ctors, c => Array.TrueForAll(c.GetParameters(), p => p.HasDefaultValue));
        return ctor == null
            ? throw new MissingMethodException($"Failed to create {typeof(T)} Cannot find a constructor which can be called with no arguments")
            : (T)ctor.Invoke(ctor.GetParameters().Select(p => p.DefaultValue).ToArray());
    }
}
