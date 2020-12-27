using intraweb_rev3.GPService;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.ServiceModel;

namespace intraweb_rev3.Models
{
    public class GP
    {
        private static Context GetContext(int companyId = 2)
        {
            try
            {
                return new Context()
                {
                    OrganizationKey = (OrganizationKey)new CompanyKey()
                    {
                        Id = companyId
                    },
                    CultureName = "en-US"
                };
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "GP.GetContext()");
            }
        }

        private static DynamicsGPClient GetClient()
        {
            try
            {
                DynamicsGPClient dynamicsGpClient = new DynamicsGPClient();
                dynamicsGpClient.ClientCredentials.Windows.ClientCredential.Domain = "HQ";
                dynamicsGpClient.ClientCredentials.Windows.ClientCredential.UserName = "app_service";
                dynamicsGpClient.ClientCredentials.Windows.ClientCredential.Password = "sushiroll90220";
                return dynamicsGpClient;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "GP.GetClient()");
            }
        }

        private static PhoneNumber SetPhone(string sNumber)
        {
            try
            {
                PhoneNumber phoneNumber = new PhoneNumber();
                if (!string.IsNullOrEmpty(sNumber))
                {
                    string str1 = sNumber.Substring(0, 10);
                    while (str1.Length < 10)
                        str1 += "0";
                    phoneNumber.Value = str1;
                    string str2 = sNumber.Substring(10);
                    while (str2.Length < 4)
                        str2 += "0";
                    phoneNumber.Extension = str2;
                }
                else
                {
                    phoneNumber.Value = "0000000000";
                    phoneNumber.Extension = "0000";
                }
                return phoneNumber;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "GP.SetPhone()");
            }
        }

        public static void PromoSalesOrder(
          Distribution_Class.FormInput form,
          List<Distribution_Class.Item> itemList,
          Distribution_Class.StoreSalesOrder store)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext();
                SalesOrder salesOrder = new SalesOrder();
                salesOrder.DocumentTypeKey = new SalesDocumentTypeKey();
                salesOrder.DocumentTypeKey.Type = SalesDocumentType.Order;
                salesOrder.DocumentTypeKey.Id = "STD";
                salesOrder.CustomerKey = new CustomerKey()
                {
                    Id = store.Code
                };
                salesOrder.Date = new DateTime();
                salesOrder.Date = Convert.ToDateTime(store.DocumentDate);
                salesOrder.BatchKey = new BatchKey()
                {
                    Id = form.Batch
                };
                salesOrder.CommentKey = new CommentKey();
                salesOrder.CommentKey.Id = "AFC_PROMO";
                salesOrder.Comment = form.Comment;
                salesOrder.WarehouseKey = new WarehouseKey()
                {
                    Id = form.Location
                };
                salesOrder.UserDefined = new SalesUserDefined()
                {
                    Text01 = store.FCId,
                    Text02 = store.FCPhone,
                    Text03 = store.ShipWeight.ToString(),
                    Text04 = store.Flag
                };
                if (form.Freight > 0M)
                    salesOrder.FreightAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = form.Freight
                    };
                salesOrder.Lines = new SalesOrderLine[itemList.Count];
                int index = 0;
                foreach (Distribution_Class.Item obj in itemList)
                {
                    salesOrder.Lines[index] = new SalesOrderLine();
                    salesOrder.Lines[index].ItemKey = new ItemKey();
                    salesOrder.Lines[index].ItemKey.Id = obj.Number;
                    salesOrder.Lines[index].UofM = obj.UOM;
                    salesOrder.Lines[index].Quantity = new Quantity();
                    salesOrder.Lines[index].Quantity.Value = (Decimal)obj.Sold;
                    salesOrder.Lines[index].WarehouseKey = new WarehouseKey()
                    {
                        Id = form.Location
                    };
                    ++index;
                }
                Policy policyByOperation = client.GetPolicyByOperation("CreateSalesOrder", context);
                client.CreateSalesOrder(salesOrder, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "GP.PromoSalesOrder()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void OrderAllocateFulfill(string orderNumber, List<Distribution_Class.Item> itemList)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext();
                SalesOrder salesOrder = new SalesOrder();
                SalesOrder salesOrderByKey = client.GetSalesOrderByKey(new SalesDocumentKey()
                {
                    Id = orderNumber
                }, context);
                salesOrderByKey.DocumentTypeKey = new SalesDocumentTypeKey();
                salesOrderByKey.DocumentTypeKey.Id = "FULFILLED";
                salesOrderByKey.Lines = new SalesOrderLine[itemList.Count];
                int index = 0;
                foreach (Distribution_Class.Item obj in itemList)
                {
                    salesOrderByKey.Lines[index] = new SalesOrderLine();
                    salesOrderByKey.Lines[index].ItemKey = new ItemKey();
                    salesOrderByKey.Lines[index].ItemKey.Id = obj.Number;
                    salesOrderByKey.Lines[index].UofM = obj.UOM;
                    salesOrderByKey.Lines[index].Key = new SalesLineKey();
                    salesOrderByKey.Lines[index].Key.LineSequenceNumber = obj.LineSeq;
                    salesOrderByKey.Lines[index].Quantity = new Quantity();
                    salesOrderByKey.Lines[index].Quantity.Value = (Decimal)obj.Sold;
                    salesOrderByKey.Lines[index].QuantityAllocated = new Quantity();
                    salesOrderByKey.Lines[index].QuantityAllocated.Value = (Decimal)obj.Sold;
                    salesOrderByKey.Lines[index].QuantityFulfilled = new Quantity();
                    salesOrderByKey.Lines[index].QuantityFulfilled.Value = (Decimal)obj.Sold;
                    ++index;
                }
                Policy policyByOperation = client.GetPolicyByOperation("UpdateSalesOrder", context);
                client.UpdateSalesOrder(salesOrderByKey, context, policyByOperation);
            }
            catch
            {
                throw new Exception("Order# " + orderNumber + ", could not allocate/fulfull due to low stock.");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void OrderItemUpdate(Distribution_Class.Item item, string processType = "")
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext();
                SalesOrder salesOrder = new SalesOrder();
                SalesOrder salesOrderByKey = client.GetSalesOrderByKey(new SalesDocumentKey()
                {
                    Id = item.OrderNumber
                }, context);
                salesOrderByKey.Lines = new SalesOrderLine[1];
                salesOrderByKey.Lines[0] = new SalesOrderLine();
                salesOrderByKey.Lines[0].Key = new SalesLineKey();
                salesOrderByKey.Lines[0].Key.LineSequenceNumber = item.LineSeq;
                salesOrderByKey.Lines[0].ItemKey = new ItemKey();
                salesOrderByKey.Lines[0].ItemKey.Id = item.Number;
                salesOrderByKey.Lines[0].UofM = item.UOM;
                salesOrderByKey.Lines[0].WarehouseKey = new WarehouseKey()
                {
                    Id = item.Location
                };
                switch (processType)
                {
                    case "add": 
                    case "change_quantity":
                        salesOrderByKey.Lines[0].Quantity = new Quantity()
                        {
                            Value = (Decimal)item.QtyEntered                           
                        };
                        break;
                    case "delete":
                        salesOrderByKey.Lines[0].DeleteOnUpdate = true;
                        break;
                }
                Policy policyByOperation = client.GetPolicyByOperation("UpdateSalesOrder", context);
                client.UpdateSalesOrder(salesOrderByKey, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "GP.OrderItemUpdate()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void DropshipSalesInvoice(
          Distribution_Class.Dropship drop,
          Distribution_Class.DropshipItem header,
          List<Distribution_Class.DropshipItem> itemList)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext(drop.CompanyId);
                SalesInvoice salesInvoice = new SalesInvoice();
                salesInvoice.DocumentTypeKey = new SalesDocumentTypeKey()
                {
                    Type = SalesDocumentType.Invoice,
                    Id = "INV-FULFILL"
                };
                salesInvoice.CustomerKey = new CustomerKey()
                {
                    Id = header.Customer
                };                
                salesInvoice.Date = Convert.ToDateTime(header.Date);
                salesInvoice.BatchKey = new BatchKey()
                { 
                    Id = string.Concat(drop.Batch, "-", DateTime.Now.ToString("MMddyy", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US")))
                };
                salesInvoice.FreightAmount = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = drop.Freight
                };
                salesInvoice.CustomerPONumber = !string.IsNullOrEmpty(drop.PONumber) ? drop.PONumber : (header.Vendor.Length < 3 ? header.Vendor : header.Vendor.Substring(0, 3)) + "-" + header.Invoice;
                salesInvoice.Lines = new SalesInvoiceLine[itemList.Count];
                int index = 0;
                foreach (Distribution_Class.DropshipItem dropshipItem in itemList)
                {
                    salesInvoice.Lines[index] = new SalesInvoiceLine();
                    salesInvoice.Lines[index].Key = new SalesLineKey()
                    {
                        LineSequenceNumber = index + 1
                    };
                    salesInvoice.Lines[index].ItemKey = new ItemKey()
                    {
                        Id = dropshipItem.Item
                    };
                    if (!string.IsNullOrEmpty(dropshipItem.ItemDesc))
                        salesInvoice.Lines[index].ItemDescription = dropshipItem.ItemDesc;
                    if (string.IsNullOrEmpty(drop.ItemNumber))
                        salesInvoice.Lines[index].IsNonInventory = new bool?(true);
                    salesInvoice.Lines[index].IsDropShip = new bool?(true);
                    if (!string.IsNullOrEmpty(dropshipItem.UOM))
                        salesInvoice.Lines[index].UofM = dropshipItem.UOM;
                    salesInvoice.Lines[index].UnitCost = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.Cost
                    };
                    salesInvoice.Lines[index].UnitPrice = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.Price
                    };
                    salesInvoice.Lines[index].Quantity = new Quantity()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.Quantity
                    };
                    salesInvoice.Lines[index].QuantityAllocated = new Quantity()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.Quantity
                    };
                    salesInvoice.Lines[index].QuantityFulfilled = new Quantity()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.Quantity
                    };
                    salesInvoice.Lines[index].TotalAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.ExtPrice
                    };
                    if (dropshipItem.Tax > 0M)
                    {
                        salesInvoice.Lines[index].TaxScheduleKey = new TaxScheduleKey()
                        {
                            Id = header.TaxSchedule
                        };
                        salesInvoice.Lines[index].TaxAmount = new MoneyAmount()
                        {
                            DecimalDigits = Utilities.decimalDigit,
                            Value = dropshipItem.Tax
                        };
                        salesInvoice.Lines[index].Taxes = new SalesLineTax[1];
                        salesInvoice.Lines[index].Taxes[0] = new SalesLineTax();
                        salesInvoice.Lines[index].Taxes[0].Key = new SalesLineTaxKey();
                        salesInvoice.Lines[index].Taxes[0].Key.SalesLineKey = salesInvoice.Lines[index].Key;
                        salesInvoice.Lines[index].Taxes[0].Key.TaxDetailKey = new TaxDetailKey()
                        {
                            Id = header.TaxSchedule
                        };
                        salesInvoice.Lines[index].Taxes[0].TotalAmount = new MoneyAmount()
                        {
                            DecimalDigits = Utilities.decimalDigit,
                            Value = dropshipItem.ExtPrice
                        };
                        salesInvoice.Lines[index].Taxes[0].TaxableAmount = new MoneyAmount()
                        {
                            DecimalDigits = Utilities.decimalDigit,
                            Value = dropshipItem.ExtPrice
                        };
                        salesInvoice.Lines[index].Taxes[0].TaxAmount = new MoneyAmount()
                        {
                            DecimalDigits = Utilities.decimalDigit,
                            Value = dropshipItem.Tax
                        };
                    }
                    ++index;
                }
                Policy policyByOperation = client.GetPolicyByOperation("CreateSalesInvoice", context);
                client.CreateSalesInvoice(salesInvoice, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(new Exception(ex.Message.ToString() + "  Customer#  " + header.Customer), "GP.DropshipSalesInvoice()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void DropshipPayablesInvoice(
          Distribution_Class.Dropship drop,
          Distribution_Class.DropshipItem header)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext(drop.CompanyId);
                PayablesInvoice payablesInvoice = new PayablesInvoice();
                payablesInvoice.Key = new PayablesDocumentKey()
                { 
                    Id = header.Vendor.Length < 3 ? header.Vendor : string.Concat(header.Vendor.Substring(0, 3), "-", header.Invoice)
                };
                payablesInvoice.BatchKey = new BatchKey()
                { 
                    Id = string.Concat(drop.Batch, "-", DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US")))
                };                
                payablesInvoice.VendorKey = new VendorKey()
                { 
                    Id = header.Vendor
                };
                payablesInvoice.VendorDocumentNumber = header.Invoice;
                payablesInvoice.Description = header.Customer;
                payablesInvoice.Date = new DateTime();
                payablesInvoice.Date = Convert.ToDateTime(header.Date);
                if (drop.PONumber != "")
                    payablesInvoice.PONumber = drop.PONumber;
                payablesInvoice.PurchasesAmount = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = header.Total
                };
                if (header.Tax > 0M)
                    payablesInvoice.MiscellaneousAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = header.Tax
                    };
                if (drop.Freight > 0M)
                    payablesInvoice.FreightAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = drop.Freight
                    };
                Policy policyByOperation = client.GetPolicyByOperation("CreatePayablesInvoice", context);
                client.CreatePayablesInvoice(payablesInvoice, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(new Exception(ex.Message.ToString() + " Vendor Invoice#  " + header.Invoice), "GP.DropshipPayablesInvoice()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void DropshipSalesReturn(
          Distribution_Class.Dropship drop,
          Distribution_Class.DropshipItem header,
          List<Distribution_Class.DropshipItem> itemList)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext(drop.CompanyId);
                SalesReturn salesReturn = new SalesReturn();
                salesReturn.DocumentTypeKey = new SalesDocumentTypeKey()
                {
                    Type = SalesDocumentType.Return,
                    Id = "DRTN"
                };
                salesReturn.CustomerKey = new CustomerKey()
                {
                    Id = header.Customer
                };
                salesReturn.Date = new DateTime();
                salesReturn.Date = Convert.ToDateTime(header.Date);
                salesReturn.BatchKey = new BatchKey()
                {
                    Id = string.Concat(drop.Batch, "-", DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US")))
                };
                salesReturn.CustomerPONumber = (!string.IsNullOrEmpty(drop.PONumber) ? drop.PONumber : (header.Vendor.Length < 3 ? header.Vendor : header.Vendor.Substring(0, 3)) + "-" + header.Invoice) + "-RTN";
                salesReturn.Lines = new SalesReturnLine[itemList.Count];
                int index = 0;
                foreach (Distribution_Class.DropshipItem dropshipItem in itemList)
                {
                    salesReturn.Lines[index] = new SalesReturnLine();
                    salesReturn.Lines[index].Key = new SalesLineKey()
                    {
                        LineSequenceNumber = index + 1
                    };
                    salesReturn.Lines[index].ItemKey = new ItemKey();
                    salesReturn.Lines[index].ItemKey.Id = dropshipItem.Item;
                    if (!string.IsNullOrEmpty(dropshipItem.ItemDesc))
                        salesReturn.Lines[index].ItemDescription = dropshipItem.ItemDesc;
                    if (string.IsNullOrEmpty(drop.ItemNumber))
                        salesReturn.Lines[index].IsNonInventory = new bool?(true);
                    salesReturn.Lines[index].IsDropShip = new bool?(true);
                    if (!string.IsNullOrEmpty(dropshipItem.UOM))
                        salesReturn.Lines[index].UofM = dropshipItem.UOM;
                    salesReturn.Lines[index].UnitCost = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.Cost
                    };
                    salesReturn.Lines[index].UnitPrice = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.Price
                    };
                    salesReturn.Lines[index].Quantity = new Quantity()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.Quantity
                    };
                    salesReturn.Lines[index].TotalAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = dropshipItem.ExtPrice
                    };
                    if (dropshipItem.Tax > 0M)
                    {
                        salesReturn.Lines[index].TaxScheduleKey = new TaxScheduleKey()
                        {
                            Id = header.TaxSchedule
                        };
                        salesReturn.Lines[index].TaxAmount = new MoneyAmount()
                        {
                            Value = dropshipItem.Tax
                        };
                        salesReturn.Lines[index].Taxes = new SalesLineTax[1];
                        salesReturn.Lines[index].Taxes[0] = new SalesLineTax();
                        salesReturn.Lines[index].Taxes[0].Key = new SalesLineTaxKey();
                        salesReturn.Lines[index].Taxes[0].Key.SalesLineKey = salesReturn.Lines[index].Key;
                        salesReturn.Lines[index].Taxes[0].Key.TaxDetailKey = new TaxDetailKey()
                        {
                            Id = header.TaxSchedule
                        };
                        salesReturn.Lines[index].Taxes[0].TotalAmount = new MoneyAmount()
                        {
                            DecimalDigits = Utilities.decimalDigit,
                            Value = dropshipItem.ExtPrice
                        };
                        salesReturn.Lines[index].Taxes[0].TaxableAmount = new MoneyAmount()
                        {
                            DecimalDigits = Utilities.decimalDigit,
                            Value = dropshipItem.ExtPrice
                        };
                        salesReturn.Lines[index].Taxes[0].TaxAmount = new MoneyAmount()
                        {
                            DecimalDigits = Utilities.decimalDigit,
                            Value = dropshipItem.Tax
                        };
                    }
                    ++index;
                }
                Policy policyByOperation = client.GetPolicyByOperation("CreateSalesReturn", context);
                client.CreateSalesReturn(salesReturn, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(new Exception(ex.Message.ToString() + "   Customer#  " + header.Customer), "GP.DropshipSalesReturn()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void DropshipPayablesCreditMemo(
          Distribution_Class.Dropship drop,
          Distribution_Class.DropshipItem header)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext(drop.CompanyId);
                PayablesCreditMemo payablesCreditMemo = new PayablesCreditMemo();
                payablesCreditMemo.Key = new PayablesDocumentKey()
                {
                    Id = (header.Vendor.Length < 3 ? header.Vendor : header.Vendor.Substring(0, 3)) + "-" + header.Invoice + "-RTN"
                };
                payablesCreditMemo.BatchKey = new BatchKey()
                {
                    Id = drop.Batch + "-" + DateTime.Now.ToString("MMddyy", (IFormatProvider)CultureInfo.CreateSpecificCulture("en-US"))
                };
                payablesCreditMemo.VendorKey = new VendorKey()
                {
                    Id = header.Vendor
                };
                payablesCreditMemo.VendorDocumentNumber = header.Invoice;
                payablesCreditMemo.Description = header.Customer;
                payablesCreditMemo.Date = new DateTime();
                payablesCreditMemo.Date = Convert.ToDateTime(header.Date);
                if (!string.IsNullOrEmpty(drop.PONumber))
                    payablesCreditMemo.PONumber = drop.PONumber;
                payablesCreditMemo.PurchasesAmount = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = header.Total
                };
                if (header.Tax > 0M)
                    payablesCreditMemo.MiscellaneousAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = header.Tax
                    };
                Policy policyByOperation = client.GetPolicyByOperation("CreatePayablesCreditMemo", context);
                client.CreatePayablesCreditMemo(payablesCreditMemo, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(new Exception(ex.Message.ToString() + " Vendor Invoice#  " + header.Invoice), "GP.DropshipPayablesCreditMemo()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void DropshipSalesNoVendorInvoice(
          Distribution_Class.Dropship drop,
          Distribution_Class.DropshipItem header,
          Distribution_Class.DropshipItem item)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext(drop.CompanyId);
                SalesInvoice salesInvoice = new SalesInvoice();
                salesInvoice.DocumentTypeKey = new SalesDocumentTypeKey()
                {
                    Type = SalesDocumentType.Invoice,
                    Id = "INV-FULFILL"
                };
                salesInvoice.CustomerKey = new CustomerKey()
                {
                    Id = item.Customer
                };
                salesInvoice.Date = new DateTime();
                salesInvoice.Date = Convert.ToDateTime(item.Date);
                salesInvoice.BatchKey = new BatchKey()
                {
                    Id = drop.Batch + "-" + DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US"))
                };
                if (drop.Freight > 0M)
                    salesInvoice.FreightAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = drop.Freight
                    };
                if (!string.IsNullOrEmpty(drop.PONumber))
                    salesInvoice.CustomerPONumber = drop.PONumber;
                salesInvoice.Lines = new SalesInvoiceLine[1];
                int index = 0;
                salesInvoice.Lines[index] = new SalesInvoiceLine();
                salesInvoice.Lines[index].ItemKey = new ItemKey()
                {
                    Id = item.Item
                };
                salesInvoice.Lines[index].IsDropShip = new bool?(true);
                salesInvoice.Lines[index].UnitCost = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Cost
                };
                salesInvoice.Lines[index].UnitPrice = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Price
                };
                salesInvoice.Lines[index].Quantity = new Quantity()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Quantity
                };
                salesInvoice.Lines[index].QuantityAllocated = new Quantity()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Quantity
                };
                salesInvoice.Lines[index].QuantityFulfilled = new Quantity()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Quantity
                };
                salesInvoice.Lines[index].TotalAmount = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.ExtPrice
                };
                Policy policyByOperation = client.GetPolicyByOperation("CreateSalesInvoice", context);
                client.CreateSalesInvoice(salesInvoice, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(new Exception(ex.Message.ToString() + "   Customer#  " + item.Customer), "GP.DropshipSalesNoVendorInvoice()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void DropshipSalesNoVendorReturn(
          Distribution_Class.Dropship drop,
          Distribution_Class.DropshipItem header,
          Distribution_Class.DropshipItem item)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext(drop.CompanyId);
                SalesReturn salesReturn = new SalesReturn();
                salesReturn.DocumentTypeKey = new SalesDocumentTypeKey()
                {
                    Type = SalesDocumentType.Return,
                    Id = "DRTN"
                };
                salesReturn.CustomerKey = new CustomerKey()
                {
                    Id = item.Customer
                };
                salesReturn.Date = new DateTime();
                salesReturn.Date = Convert.ToDateTime(item.Date);
                salesReturn.BatchKey = new BatchKey()
                {
                    Id = drop.Batch + "-" + DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US"))
                };
                if (drop.Freight > 0M)
                    salesReturn.FreightAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = drop.Freight
                    };
                if (!string.IsNullOrEmpty(drop.PONumber))
                    salesReturn.CustomerPONumber = drop.PONumber;
                salesReturn.Lines = new SalesReturnLine[1];
                int index = 0;
                salesReturn.Lines[index] = new SalesReturnLine();
                salesReturn.Lines[index].ItemKey = new ItemKey()
                {
                    Id = item.Item
                };
                salesReturn.Lines[index].IsDropShip = new bool?(true);
                salesReturn.Lines[index].UnitCost = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Cost
                };
                salesReturn.Lines[index].UnitPrice = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Price
                };
                salesReturn.Lines[index].Quantity = new Quantity()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Quantity
                };
                salesReturn.Lines[index].Quantity = new Quantity()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.Quantity
                };
                salesReturn.Lines[index].TotalAmount = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = item.ExtPrice
                };
                Policy policyByOperation = client.GetPolicyByOperation("CreateSalesReturn", context);
                client.CreateSalesReturn(salesReturn, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "GP.DropshipSalesNoVendorReturn()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void DropshipPayablesForNoVendorInvoice(
          Distribution_Class.Dropship drop,
          Distribution_Class.DropshipItem header,
          Distribution_Class.DropshipItem item)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext(drop.CompanyId);
                PayablesInvoice payablesInvoice = new PayablesInvoice();
                payablesInvoice.Key = new PayablesDocumentKey() 
                {
                    Id = (header.Vendor.Length < 3 ? header.Vendor : header.Vendor.Substring(0, 3)) + "-" + item.Item + "-" + DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US"))
                };
                payablesInvoice.BatchKey = new BatchKey()
                {
                    Id = string.Concat(drop.Batch, "-", DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US")))
                };
                payablesInvoice.VendorKey = new VendorKey()
                { 
                    Id = header.Vendor
                };
                payablesInvoice.VendorDocumentNumber = payablesInvoice.Key.Id;
                payablesInvoice.Date = new DateTime();
                payablesInvoice.Date = Convert.ToDateTime(header.Date);
                if (!string.IsNullOrEmpty(drop.PONumber))
                    payablesInvoice.PONumber = drop.PONumber;
                payablesInvoice.PurchasesAmount = new MoneyAmount()
                {
                    DecimalDigits = Utilities.decimalDigit,
                    Value = header.Total
                };
                if (drop.Freight > 0M)
                    payablesInvoice.FreightAmount = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = drop.Freight * (Decimal)header.CustomerCount
                    };
                Policy policyByOperation = client.GetPolicyByOperation("CreatePayablesInvoice", context);
                client.CreatePayablesInvoice(payablesInvoice, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "GP.DropshipPayablesForNoVendorInvoice()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void ItemVariance(
          List<Distribution_Class.Item> itemList,
          string documentNumber,
          string documentDate)
        {
            DynamicsGPClient client = GP.GetClient();
            string str = "";
            try
            {
                Context context = GP.GetContext(9);
                InventoryVariance inventoryVariance = new InventoryVariance();
                inventoryVariance.Key = new InventoryKey();
                inventoryVariance.Key.Id = documentNumber;
                inventoryVariance.GLPostingDate = new DateTime();
                inventoryVariance.GLPostingDate = Convert.ToDateTime(documentDate);
                inventoryVariance.Date = new DateTime();
                inventoryVariance.Date = Convert.ToDateTime(documentDate);
                inventoryVariance.BatchKey = new BatchKey()
                {
                    Id = string.Concat("INVADJ-", DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US")))
                };
                inventoryVariance.Lines = new InventoryVarianceLine[itemList.Count];
                int index = 0;
                foreach (Distribution_Class.Item obj in itemList)
                {
                    str = "Item = " + obj.Number + ", Lot = " + obj.Lot + ", Lot Recd = " + obj.LotDateReceived + ", Variance = " + (object)obj.Variance;
                    inventoryVariance.Lines[index] = new InventoryVarianceLine();
                    inventoryVariance.Lines[index].Key = new InventoryLineKey()
                    {
                        SequenceNumber = (Decimal)obj.LineSeq
                    };
                    inventoryVariance.Lines[index].ItemKey = new ItemKey()
                    {
                        Id = obj.Number
                    };
                    inventoryVariance.Lines[index].UofM = obj.UOM;
                    inventoryVariance.Lines[index].UnitCost = new MoneyAmount()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = obj.Cost
                    };
                    inventoryVariance.Lines[index].Quantity = new Quantity()
                    {
                        DecimalDigits = Utilities.decimalDigit,
                        Value = (Decimal)obj.Variance
                    };
                    inventoryVariance.Lines[index].WarehouseKey = new WarehouseKey()
                    {
                        Id = obj.Location
                    };
                    if (obj.Category != "DRYNON")
                    {
                        inventoryVariance.Lines[index].Lots = new InventoryLot[1];
                        inventoryVariance.Lines[index].Lots[0] = new InventoryLot();
                        inventoryVariance.Lines[index].Lots[0].Key = new InventoryLotKey()
                        {
                            SequenceNumber = obj.LineSeq
                        };
                        inventoryVariance.Lines[index].Lots[0].LotNumber = obj.Lot;
                        inventoryVariance.Lines[index].Lots[0].ReceivedDate = new DateTime();
                        inventoryVariance.Lines[index].Lots[0].ReceivedDate = Convert.ToDateTime(obj.LotDateReceived);
                        inventoryVariance.Lines[index].Lots[0].ExpirationDate = new DateTime();
                        inventoryVariance.Lines[index].Lots[0].ExpirationDate = Convert.ToDateTime("1900-01-01 00:00:00.000");
                        inventoryVariance.Lines[index].Lots[0].Quantity = new Quantity()
                        {
                            DecimalDigits = Utilities.decimalDigit,
                            Value = (Decimal)Math.Abs(obj.Variance)
                        };
                    }
                    ++index;
                }
                Policy policyByOperation = client.GetPolicyByOperation("CreateInventoryVariance", context);
                client.CreateInventoryVariance(inventoryVariance, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(new Exception(str + " | " + ex.Message), "GP.ItemVariance()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }

        public static void OrderSiteChange(
          Distribution_Class.Order order,
          List<Distribution_Class.Item> itemList)
        {
            DynamicsGPClient client = GP.GetClient();
            try
            {
                Context context = GP.GetContext();
                SalesOrder salesOrder = new SalesOrder();
                SalesDocumentKey key = new SalesDocumentKey()
                {
                    Id = order.Number
                };
                SalesOrder salesOrderByKey = client.GetSalesOrderByKey(key, context);
                salesOrderByKey.WarehouseKey = new WarehouseKey()
                {
                    Id = order.Location
                };
                salesOrderByKey.Lines = new SalesOrderLine[itemList.Count];
                int index = 0;
                foreach (Distribution_Class.Item obj in itemList)
                {
                    salesOrderByKey.Lines[index] = new SalesOrderLine();
                    salesOrderByKey.Lines[index].Key = new SalesLineKey();
                    salesOrderByKey.Lines[index].Key.LineSequenceNumber = obj.LineSeq;
                    salesOrderByKey.Lines[index].ItemKey = new ItemKey()
                    {
                        Id = obj.Number
                    };
                    salesOrderByKey.Lines[index].UofM = obj.UOM;
                    salesOrderByKey.Lines[index].Quantity = new Quantity()
                    {
                        Value = (Decimal)obj.Sold
                    };
                    salesOrderByKey.Lines[index].WarehouseKey = new WarehouseKey()
                    {
                        Id = order.Location
                    };
                    ++index;
                }
                Policy policyByOperation = client.GetPolicyByOperation("UpdateSalesOrder", context);
                client.UpdateSalesOrder(salesOrderByKey, context, policyByOperation);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "GP.OrderSiteChange()");
            }
            finally
            {
                if (client.State != CommunicationState.Faulted)
                    client.Close();
            }
        }
    }
}