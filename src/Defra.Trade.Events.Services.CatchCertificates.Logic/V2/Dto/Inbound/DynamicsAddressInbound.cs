// Copyright DEFRA (c). All rights reserved.
// Licensed under the Open Government License v3.0.

using System;
using System.Text.Json.Serialization;
using Newtonsoft.Json;

namespace Defra.Trade.Events.Services.CatchCertificates.Logic.V2.Dto.Inbound;

public class DynamicsAddressInbound
{
    [JsonProperty("defra_addressid")]
    [JsonPropertyName("defra_addressid")]
    public Guid? DefraAddressId { get; set; }

    [JsonProperty("defra_buildingname")]
    [JsonPropertyName("defra_buildingname")]
    public string DefraBuildingName { get; set; }

    [JsonProperty("_defra_country_value")]
    [JsonPropertyName("_defra_country_value")]
    public string DefraCountryValue { get; set; }

    [JsonProperty("_defra_country_value_Microsoft_Dynamics_CRM_associatednavigationproperty")]
    [JsonPropertyName("_defra_country_value_Microsoft_Dynamics_CRM_associatednavigationproperty")]
    public string DefraCountryValueMicrosoftDynamicsCrmAssociatedNavigationProperty { get; set; }

    [JsonProperty("_defra_country_value_Microsoft_Dynamics_CRM_lookuplogicalname")]
    [JsonPropertyName("_defra_country_value_Microsoft_Dynamics_CRM_lookuplogicalname")]
    public string DefraCountryValueMicrosoftDynamicsCrmLookupLogicalname { get; set; }

    [JsonProperty("_defra_country_value_OData_Community_Display_V1_FormattedValue")]
    [JsonPropertyName("_defra_country_value_OData_Community_Display_V1_FormattedValue")]
    public string DefraCountryValueODataCommunityDisplayV1FormattedValue { get; set; }

    [JsonProperty("defra_county")]
    [JsonPropertyName("defra_county")]
    public string DefraCounty { get; set; }

    [JsonProperty("defra_dependentlocality")]
    [JsonPropertyName("defra_dependentlocality")]
    public string DefraDependentLocality { get; set; }

    [JsonProperty("defra_fromcompanieshouse")]
    [JsonPropertyName("defra_fromcompanieshouse")]
    public bool DefraFromCompaniesHouse { get; set; }

    [JsonProperty("defra_fromcompanieshouse_OData_Community_Display_V1_FormattedValue")]
    [JsonPropertyName("defra_fromcompanieshouse_OData_Community_Display_V1_FormattedValue")]
    public string DefraFromCompaniesHouseODataCommunityDisplayV1FormattedValue { get; set; }

    [JsonProperty("defra_internationalpostalcode")]
    [JsonPropertyName("defra_internationalpostalcode")]
    public string DefraInternationalPostalCode { get; set; }

    [JsonProperty("defra_locality")]
    [JsonPropertyName("defra_locality")]
    public string DefraLocality { get; set; }

    [JsonProperty("defra_postcode")]
    [JsonPropertyName("defra_postcode")]
    public string DefraPostcode { get; set; }

    [JsonProperty("defra_premises")]
    [JsonPropertyName("defra_premises")]
    public string DefraPremises { get; set; }

    [JsonProperty("defra_street")]
    [JsonPropertyName("defra_street")]
    public string DefraStreet { get; set; }

    [JsonProperty("defra_subbuildingname")]
    [JsonPropertyName("defra_subbuildingname")]
    public string DefraSubBuildingName { get; set; }

    [JsonProperty("defra_towntext")]
    [JsonPropertyName("defra_towntext")]
    public string DefraTownText { get; set; }

    [JsonProperty("defra_uprn")]
    [JsonPropertyName("defra_uprn")]
    public string DefraUprn { get; set; }
}
