using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace JSonManager
{

    public class CatalogoExperticket
    {
        public DateTime LastUpdatedDateTime { get; set; }
        public Partnersettings PartnerSettings { get; set; }
        public Provider[] Providers { get; set; }
        public object[] CombinedProducts { get; set; }
        public bool Success { get; set; }
        public DateTime Timestamp { get; set; }
    }

    public class Partnersettings
    {
        public bool DemandClientData { get; set; }
        public bool DemandClientTaxData { get; set; }
        public bool EnableCancellationRequest { get; set; }
        public int PaymentType { get; set; }
    }

    public class Provider
    {
        public string ProviderId { get; set; }
        public string ProviderName { get; set; }
        public string ProviderDescription { get; set; }
        public string ProviderCommercialConditions { get; set; }
        public string ProviderAccessConditions { get; set; }
        public string ExchangeVoucherPoint { get; set; }
        public string AdvancedDateSelectorMethodName { get; set; }
        public bool IsForGroups { get; set; }
        public bool IsForSeasonTickets { get; set; }
        public bool RequiresReservation { get; set; }
        public int LimitOfNumberOfPeopleToBeGroup { get; set; }
        public string Logo { get; set; }
        public Promotionalimage[] PromotionalImages { get; set; }
        public string[] Tags { get; set; }
        public Location Location { get; set; }
        public Cancellationpolicy CancellationPolicy { get; set; }
        public Ticketenclosure[] TicketEnclosures { get; set; }
        public Productbas[] ProductBases { get; set; }
        public bool DemandAccessDate { get; set; }
        public int TaxType { get; set; }
        public int PurchaseFlowType { get; set; }
        public int Type { get; set; }
    }

    public class Location
    {
        public string CountryCode { get; set; }
        public string City { get; set; }
        public string Address { get; set; }
        public string ZipCode { get; set; }
        public float Lat { get; set; }
        public float Lng { get; set; }
    }

    public class Cancellationpolicy
    {
        public bool IsRefundable { get; set; }
        public Rule[] Rules { get; set; }
    }

    public class Rule
    {
        public float Percentage { get; set; }
        public float HoursInAdvanceOfAccess { get; set; }
    }

    public class Promotionalimage
    {
        public bool IsMainImage { get; set; }
        public string Url { get; set; }
    }

    public class Ticketenclosure
    {
        public string TicketEnclosureId { get; set; }
        public string TicketEnclosureName { get; set; }
        public int TypeOfPersonDefinitionTypeChild { get; set; }
        public float TypeOfPersonDefinitionValueChild { get; set; }
        public int TypeOfPersonDefinitionTypeAdult { get; set; }
        public float TypeOfPersonDefinitionValueAdult { get; set; }
        public int TypeOfPersonDefinitionTypeSenior { get; set; }
        public float TypeOfPersonDefinitionValueSenior { get; set; }
        public string Logo { get; set; }
        public string TicketEnclosureConditions { get; set; }
        public Sessions Sessions { get; set; }
    }

    public class Sessions
    {
        public Sessionsgroupsessioncontent[] SessionsGroupSessionContents { get; set; }
        public string SessionContentProfileId { get; set; }
        public string SessionsGroupProfileId { get; set; }
        public int TicketEnclosureAutoAssignSessionType { get; set; }
    }

    public class Sessionsgroupsessioncontent
    {
        public string SessionsGroupId { get; set; }
        public string SessionContentId { get; set; }
    }

    public class Productbas
    {
        public string ProductBaseId { get; set; }
        public string ProductBaseName { get; set; }
        public string ProductBaseDescription { get; set; }
        public Product[] Products { get; set; }
        public Productpaxgrouping[] ProductPaxGroupings { get; set; }
        public int LimitOfNumberOfPeopleToBeGroup { get; set; }
    }

    public class Product
    {
        public string ProductId { get; set; }
        public string ProductName { get; set; }
        public string ProductDescription { get; set; }
        public int HoursInAdvanceOfPurchase { get; set; }
        public string DaysWithLimitedCapacity { get; set; }
        public int MinimumNumberByTransaction { get; set; }
        public int NumberOfPeople { get; set; }
        public int NumberOfAdults { get; set; }
        public int NumberOfChildren { get; set; }
        public int NumberOfSenior { get; set; }
        public int NumberOfBabies { get; set; }
        public int NumberOfGeneric { get; set; }
        public bool RequiresRealTimePrice { get; set; }
        public Pricesanddate[] PricesAndDates { get; set; }
        public Ticket[] Tickets { get; set; }
        public int ValidDays { get; set; }
        public int ValidDaysType { get; set; }
        public int AccessDateCriteria { get; set; }
        public int BarcodeAssignment { get; set; }
        public Salesdocumentsettings SalesDocumentSettings { get; set; }
        public int PriceMode { get; set; }
        public Commission Commission { get; set; }
        public bool HasSaleFlowRule { get; set; }
        public bool IsForPackaging { get; set; }
        public DateTime StartIsActiveDate { get; set; }
        public Cancellationpolicy1 CancellationPolicy { get; set; }
        public string ProductPaxGroupingId { get; set; }
        public string SuggestedSalesProductName { get; set; }
        public string ProductCancellationConditions { get; set; }
        public int Release { get; set; }
        public int MaxHoursInAdvanceOfPurchase { get; set; }
    }

    public class Salesdocumentsettings
    {
        public bool ShowPrice { get; set; }
        public int AccessDateCriteriaOpenDateSalesDocument { get; set; }
        public bool Disable { get; set; }
    }

    public class Commission
    {
        public int Type { get; set; }
        public float Value { get; set; }
    }

    public class Cancellationpolicy1
    {
        public bool IsRefundable { get; set; }
    }

    public class Pricesanddate
    {
        public float Price { get; set; }
        public string Dates { get; set; }
        public string Currency { get; set; }
        public float OriginalPrice { get; set; }
        public string CurrencyName { get; set; }
        public Taxbreakdown[] TaxBreakdown { get; set; }
    }

    public class Taxbreakdown
    {
        public float TaxPercentage { get; set; }
        public float PriceWithoutTaxes { get; set; }
        public float PriceWithTaxes { get; set; }
    }

    public class Ticket
    {
        public string TicketId { get; set; }
        public string TicketName { get; set; }
        public string TicketConditions { get; set; }
        public string TicketEnclosureId { get; set; }
        public bool IsQuotaTicket { get; set; }
        public Typeofperson TypeOfPerson { get; set; }
        public int FromAccessDay { get; set; }
        public int ToAccessDay { get; set; }
    }

    public class Typeofperson
    {
        public int Type { get; set; }
        public int PersonNumber { get; set; }
    }

    public class Productpaxgrouping
    {
        public string ProductPaxGroupingId { get; set; }
        public string Dates { get; set; }
        public string ProductPaxGroupingName { get; set; }
    }

}
