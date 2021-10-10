using System;

namespace intraweb_rev3.Models
{
    public class Distribution_Class
    {
        public class Menu
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
        }

        public class Option
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
            public int Count { get; set; } = 0;
        }

        public class FormInput
        {
            public string Item { get; set; } = "";
            public string UOM { get; set; } = "";
            public string Lot { get; set; } = "";
            public string StartDate { get; set; } = "";
            public string EndDate { get; set; } = "";
            public string Batch { get; set; } = "";
            public string Type { get; set; } = "";
            public string Store { get; set; } = "";
            public string Order { get; set; } = "";
            public string NewBatch { get; set; } = "";
            public string SelectedBatch { get; set; } = "";
            public int Id { get; set; } = 0;
            public string Vendor { get; set; } = "";
            public string Comment { get; set; } = "";
            public string Allocate { get; set; } = "";
            public string RemoveZeroAmount { get; set; } = "";
            public decimal Freight { get; set; } = 0;
            public string Location { get; set; } = "";
            public string PriceLevel { get; set; } = "";
        }

        public class Item
        {
            public string Number { get; set; } = "";
            public string Description { get; set; } = "";
            public string Category { get; set; } = "";
            public string Lot { get; set; } = "";
            public int OnHand { get; set; } = 0;
            public int Allocated { get; set; } = 0;
            public int Available { get; set; } = 0;
            public int Variance { get; set; } = 0;
            public int OnOrder { get; set; } = 0;
            public int Fulfilled { get; set; } = 0;
            public string Ratio { get; set; } = "";
            public string Location { get; set; } = "";
            public string Date { get; set; } = "";
            public decimal Weight { get; set; } = 0;
            public string UOM { get; set; } = "";
            public int UOMQty { get; set; } = 0;
            public string UOMBase { get; set; } = "";
            public int LotQty { get; set; } = 0;
            public decimal Cost { get; set; } = 0;
            public decimal UnitCost { get; set; } = 0;
            public decimal Price { get; set; } = 0;
            public decimal UnitPrice { get; set; } = 0;
            public int PriceListFromQty { get; set; } = 0;
            public int Sold { get; set; } = 0;
            public int Receipt { get; set; } = 0;
            public int Adjust { get; set; } = 0;
            public int Transfer { get; set; } = 0;
            public int Return { get; set; } = 0;
            public int LocationsSoldAt { get; set; } = 0;
            public decimal Sales { get; set; } = 0;
            public int SalesMonth1 { get; set; } = 0;
            public int SalesMonth2 { get; set; } = 0;
            public int SalesMonth3 { get; set; } = 0;
            public int SalesMonth4 { get; set; } = 0;
            public int SalesMonth5 { get; set; } = 0;
            public int SalesMonth6 { get; set; } = 0;
            public string Storecode { get; set; } = "";            
            public decimal ShipWt { get; set; } = 0;
            public int UnitsSold { get; set; } = 0;
            public string Vendor { get; set; } = "";
            public int LineSeq { get; set; } = 0;
            public string OrderNumber { get; set; } = "";
            public int QtyEntered { get; set; } = 0;
            public string LotDateReceived { get; set; } = "";
            public int LotDateSequence { get; set; } = 0;
            public string DocumentNumber { get; set; } = "";
            public string Batch { get; set; } = "";
            public string PriceLevel { get; set; } = "";
        }

        public class Recall
        {
            public string InvoiceNo { get; set; } = "";
            public string DocDate { get; set; } = "";
            public string ShipDate { get; set; } = "";
            public string Item { get; set; } = "";
            public string UOM { get; set; } = "";
            public string Lot { get; set; } = "";
            public decimal Quantity { get; set; } = 0;
            public decimal Return { get; set; } = 0;
            public string Storecode { get; set; } = "";
            public string Storename { get; set; } = "";
            public string Address { get; set; } = "";
            public string City { get; set; } = "";
            public string State { get; set; } = "";
            public string Zip { get; set; } = "";
            public string RM { get; set; } = "";
            public string RMcell { get; set; } = "";
            public string RMeMail { get; set; } = "";
            public string FCId { get; set; } = "";
            public string FC { get; set; } = "";
            public string FCcell { get; set; } = "";
            public string FCeMail { get; set; } = "";
            public string Region { get; set; } = "";
            public string Storegroup { get; set; } = "";
            public string Storecorp { get; set; } = "";
        }

        public class StoreSalesOrder
        {
            public string DocumentDate { get; set; } = "";
            public string Code { get; set; } = "";
            public string Name { get; set; } = "";
            public string ShipTo { get; set; } = "";
            public string BillTo { get; set; } = "";
            public string PaymentTerm { get; set; } = "";
            public string TaxScheduleId { get; set; } = "";
            public string ShipMethod { get; set; } = "";
            public string Flag { get; set; } = "";
            public string FCId { get; set; } = "";
            public string FCPhone { get; set; } = "";
            public decimal ShipWeight { get; set; } = 0;
            public string Location { get; set; } = "1";
        }

        public class BatchListStore
        {
            public string Code { get; set; } = "";
            public string Name { get; set; } = "";
            public string State { get; set; } = "";
            public string ShipMethod { get; set; } = "";
            public string OrderNo { get; set; } = "";
        }

        public class PickListStore
        {
            public string Store1 { get; set; } = "";
            public string Store2 { get; set; } = "";
            public string Store3 { get; set; } = "";
            public string Store4 { get; set; } = "";
            public string Store5 { get; set; } = "";
            public string Store6 { get; set; } = "";
            public string Store7 { get; set; } = "";
            public string Store8 { get; set; } = "";
            public string Store9 { get; set; } = "";
            public string Store10 { get; set; } = "";
        }

        public class PicklistItem
        {
            public string Name { get; set; } = "";
            public string Lot { get; set; } = "";
            public string Category { get; set; } = "";
            public string UOM { get; set; } = "";
            public int UOMQty { get; set; } = 0;
            public string Qty1 { get; set; } = "";
            public string Qty2 { get; set; } = "";
            public string Qty3 { get; set; } = "";
            public string Qty4 { get; set; } = "";
            public string Qty5 { get; set; } = "";
            public string Qty6 { get; set; } = "";
            public string Qty7 { get; set; } = "";
            public string Qty8 { get; set; } = "";
            public string Qty9 { get; set; } = "";
            public string Qty10 { get; set; } = "";
            public int Total { get; set; } = 0;
            public decimal LineTotal { get; set; } = 0;
        }

        public class Order
        {
            public string Number { get; set; } = "";
            public string OrderDate { get; set; } = "";
            public string Storecode { get; set; } = "";
            public string Storename { get; set; } = "";
            public string PurchaseOrderNo { get; set; } = "";
            public string StoreState { get; set; } = "";
            public string ShipMethod { get; set; } = "";
            public string ShipWt { get; set; } = "";
            public string Comment { get; set; } = "";
            public string ShipDate { get; set; } = "";
            public decimal DocAmount { get; set; } = 0;
            public string FCID { get; set; } = "";
            public string Location { get; set; } = "";
            public string Batch { get; set; } = "";
            public string BatchDate { get; set; } = "";
            public int BatchCount { get; set; } = 0;
            public string OriginalBatch { get; set; } = "";
            public string OriginalSite { get; set; } = "";
            public string OriginalNumber { get; set; } = "";
            public int Allocated { get; set; } = 0;
            public string Region { get; set; } = "";
        }

        public class Promo
        {
            public int Id { get; set; } = 0;
            public string Startdate { get; set; } = "";
            public string Enddate { get; set; } = "";
            public string Description { get; set; } = "";
            public string Storeprefix { get; set; } = "";
            public string State { get; set; } = "";
            public int Ordercount { get; set; } = 0;
            public int Invoicecount { get; set; } = 0;
            public string Storecode { get; set; } = "";
            public string IsActive { get; set; } = "0";
        }

        public class Lot
        {
            public string Id { get; set; } = "";
            public string DateReceived { get; set; } = "";
            public int Onhand { get; set; } = 0;
            public string Vendor { get; set; } = "";
            public int Qty { get; set; } = 0;
            public int DateSequence { get; set; } = 0;
            public string ItemNo { get; set; } = "";
            public int ItemLineSeq { get; set; } = 0;
        }

        public class Dropship
        {
            public int Id { get; set; } = 0;
            public string Description { get; set; } = "";
            public string RateType { get; set; } = "";
            public decimal Rate { get; set; } = 0;
            public string Batch { get; set; } = "";
            public string PONumber { get; set; } = "";
            public decimal Freight { get; set; } = 0;
            public int CompanyId { get; set; } = 0;
            public string Company { get; set; } = "";
            public string InvoiceAppend { get; set; } = "";
            public string ItemPrefix { get; set; } = "";
            public string ReplaceAFCInCustomerNo { get; set; } = "";
            public string FreightMarker { get; set; } = "";
            public string CreatePayable { get; set; } = "";
            public string ItemNumber { get; set; } = "";
            public decimal ItemCost { get; set; } = 0;
            public string VendorNumber { get; set; } = "";
            public string Customer { get; set; } = "";
            public string Invoice { get; set; } = "";
            public string InvoiceDate { get; set; } = "";
            public string Item { get; set; } = "";
            public string ItemDesc { get; set; } = "";
            public string UOM { get; set; } = "";
            public string Quantity { get; set; } = "";
            public string Cost { get; set; } = "";
            public string ExtendedCost { get; set; } = "";
            public string Tax { get; set; } = "";
            public string Return { get; set; } = "";
            public string Vendor { get; set; } = "";
            public int CopyFromId { get; set; } = 0;
        }

        public class DropshipVendor
        {
            public int DropId { get; set; } = 0;
            public int Id { get; set; } = 0;
            public string Source { get; set; } = "";
            public string Destination { get; set; } = "";
        }

        public class DropshipItem
        {
            public int DropId { get; set; } = 0;
            public string Customer { get; set; } = "";
            public string Invoice { get; set; } = "";
            public string Date { get; set; } = "";
            public string Item { get; set; } = "";
            public string ItemDesc { get; set; } = "";
            public string UOM { get; set; } = "";
            public decimal Quantity { get; set; } = 0;
            public decimal Cost { get; set; } = 0;
            public decimal ExtCost { get; set; } = 0;
            public decimal Price { get; set; } = 0;
            public decimal ExtPrice { get; set; } = 0;
            public decimal Tax { get; set; } = 0;
            public int ReturnFlag { get; set; } = 0;
            public int FreightFlag { get; set; } = 0;
            public string Vendor { get; set; } = "";
            public decimal Total { get; set; } = 0;
            public int CustomerCount { get; set; } = 0;
            public string TaxSchedule { get; set; } = "";
            public decimal TotalSale { get; set; } = 0;
        }

        public class Company
        {
            public int Id { get; set; } = 0;
            public string Name { get; set; } = "";
        }

        public class ItemBin
        {
            public int Id { get; set; } = 0;
            public string Item { get; set; } = "";
            public string ItemDesc { get; set; } = "";
            public string Location { get; set; } = "";
            public string BinCap { get; set; } = "";
            public string Secondary { get; set; } = "";
            public string Third { get; set; } = "";
            public string Priority { get; set; } = "";
        }

        public class DropLabel
        {
            public int Id { get; set; } = 0;
            public string City { get; set; } = "";
            public string State { get; set; } = "";
        }

        public class BillofLading
        {
            public string DocNumber { get; set; } = "";
            public string DocDate { get; set; } = "";
            public string Customer { get; set; } = "";
            public string CustomerName { get; set; } = "";
            public string Street1 { get; set; } = "";
            public string Street2 { get; set; } = "";
            public string City { get; set; } = "";
            public string State { get; set; } = "";
            public string Zip { get; set; } = "";
            public string Phone { get; set; } = "";
            public decimal FrozenWeight { get; set; } = 0;
            public decimal DryWeight { get; set; } = 0;
            public decimal TotalWeight { get; set; } = 0;
            public string FCID { get; set; } = "";
            public string FCPhone { get; set; } = "";
            public string Shipper { get; set; } = "";
            public string ShipperPhone { get; set; } = "";
            public string FromSite { get; set; } = "";
            public string ToSite { get; set; } = "";
            public string DocStatus { get; set; } = "";
        }

        public class WarehouseMgmtSystem
        {
            public string Status { get; set; } = "";
            public string TrxType { get; set; } = "";
            public string UserId { get; set; } = "";
            public string TerminalId { get; set; } = "";
            public string TrxDate { get; set; } = "";
            public string DocNumber { get; set; } = "";
            public string Item { get; set; } = "";
            public string Lot { get; set; } = "";
            public int Qty { get; set; } = 0;
            public string FromSite { get; set; } = "";
            public string FromBin { get; set; } = "";
            public string ToSite { get; set; } = "";
            public string ToBin { get; set; } = "";
        }

        public class TunaShip
        {
            public int StoreID { get; set; } = 0;
            public string Storecode { get; set; } = "";
            public string Storename { get; set; } = "";
            public string Address { get; set; } = "";
            public string City { get; set; } = "";
            public string State { get; set; } = "";
            public string Zipcode { get; set; } = "";
            public string Phone { get; set; } = "";
            public string Region { get; set; } = "";
            public int Qty { get; set; } = 0;
            public int QtyEntered { get; set; } = 0;
            public string ModifiedOn { get; set; } = "";
        }

    }
}