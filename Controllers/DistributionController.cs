using intraweb_rev3.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Globalization;
using System.IO;
using System.Web;
using System.Web.Mvc;

namespace intraweb_rev3.Controllers
{
    public class DistributionController : Controller
    {
        public ActionResult Index() => View();

        [HttpPost]
        public JsonResult LoadMenu()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.MenuList()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        private string GetFilePath(string dir, string filename)
        {
            try
            {
                string str = Server.MapPath("~/" + dir);
                Utilities.DeleteOldFiles(str);
                return Path.Combine(str, filename);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Controller.DistributionController.GetFilePath()");
            }
        }

        public ActionResult LowStock() => View();

        [HttpPost]
        public JsonResult LowStockData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "LowStock_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.LowStock(filePath, form));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult PriceList() => View();

        [HttpPost]
        public JsonResult PriceListData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PriceList_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.PriceList(filePath));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public JsonResult PriceListLevelData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PriceListWithLevel_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.PriceListWithLevel(filePath, form));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult Recall() => View();

        [HttpPost]
        public JsonResult RecallData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "Recall_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.RecallItem(form, filePath));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ItemLevel() => View();

        [HttpPost]
        public JsonResult ItemLevelData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "ItemLevel_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.ItemLevel(filePath, form));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult Sales() => View();

        [HttpPost]
        public JsonResult SalesData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "Sales_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.Sales(filePath, form));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult StoreSales() => View();

        [HttpPost]
        public JsonResult StoreSalesDroplist()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.StoreDropList()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult StoreSalesData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = filename = "StoreSales_" + Utilities.GetRandom() + ".csv";
                Distribution.StoreSales(GetFilePath("Download", filename), form);                    
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult InventoryQuantity() => View();

        [HttpPost]
        public JsonResult InventoryQuantityData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "InventoryQuantity_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.InventoryQuantity(filePath, form));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ItemSales() => View();

        [HttpPost]
        public JsonResult ItemSalesData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "";
                switch (form.Type)
                {
                    case "item_sales":
                        filename = "ItemSales_" + Utilities.GetRandom() + ".csv";
                        Distribution.ItemSales(GetFilePath("Download", filename), form);
                        break;
                    case "item_turnover":
                        filename = "ItemTurnover_" + Utilities.GetRandom() + ".csv";
                        Distribution.ItemTurnover(GetFilePath("Download", filename), form);
                        break;                   
                }
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult BatchPicklist() => View();

        [HttpPost]
        public JsonResult BatchPicklistIds()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.BatchPicklistIds()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult BatchPicklistData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename1 = form.Batch + "_BatchPicklist.pdf";
                string filePath1 = GetFilePath("Download", filename1);
                List<Distribution_Class.BatchListStore> storeList = Distribution.BatchPicklistStores(form.Batch);
                List<Distribution_Class.PicklistItem> pickList = Distribution.BatchPicklistItems(filePath1, form, storeList);
                Distribution_Pdf.BatchPicklist(form, filePath1, storeList, pickList);
                objectList.Add("../Download/" + filename1);
                string filename2 = form.Batch + "_PickTicket.pdf";
                string filePath2 = GetFilePath("Download", filename2);
                Distribution_Pdf.PickTicket(form, filePath2);
                objectList.Add("../Download/" + filename2);
                DataTable DropLabels = Distribution_DB.DropLabel("records", new Distribution_Class.DropLabel());
                string filename3 = form.Batch + "ShippingTagFrozen.pdf";
                Distribution_Pdf.ShippingTags(GetFilePath("Download", filename3), form, "frozen", DropLabels);
                objectList.Add("../Download/" + filename3);
                string filename4 = form.Batch + "_ShippingTagDry.pdf";
                Distribution_Pdf.ShippingTags(GetFilePath("Download", filename4), form, "dry", DropLabels);
                objectList.Add("../Download/" + filename4);
                string filename5 = form.Batch + "_PalletCount.pdf";
                Distribution_Pdf.PalletCount(GetFilePath("Download", filename5), form);
                objectList.Add("../Download/" + filename5);
                string filename6 = form.Batch + "_BatchPicklist.csv";
                Distribution.WriteBatchPickListFile(GetFilePath("Download", filename6), pickList, storeList);
                objectList.Add("../Download/" + filename6);
                string filename7 = form.Batch + "_BillofLading.pdf";
                Distribution_Pdf.BillofLading(GetFilePath("Download", filename7), form);
                objectList.Add("../Download/" + filename7);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult BatchOrder() => View();

        [HttpPost]
        public JsonResult BatchOrderIds()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.BatchOrderIds()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult BatchOrderData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string batchID = Utilities.cleanInput(form.Batch);
                // csv download for table list.
                string filename1 = batchID + "_Orders.csv";
                string filePath1 = GetFilePath("Download", filename1);
                objectList.Add(Distribution.BatchOrderData(form, filePath1));
                objectList.Add("../Download/" + filename1);
                // Lanter csv file for import into their system.
                string filename2 = batchID + "_PickTicketLanter.csv";
                string filePath2 = GetFilePath("Download", filename2);
                Distribution.PickTicketLanter(form, filePath2);
                objectList.Add("../Download/" + filename2);
                // pdf pick ticket for batch.
                string filename3 = batchID + "_PickTicket.pdf";
                string filePath3 = GetFilePath("Download", filename3);
                Distribution_Pdf.PickTicket(form, filePath3);
                objectList.Add("../Download/" + filename3);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult BatchOrderUpdate(Distribution_Class.FormInput form)
        {
            try
            {
                return Json(new List<object>()
                {
                   Distribution.BatchOrderUpdateRun(form)
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }
        // Change Site ID for order and items.
        [HttpPost]
        public JsonResult BatchOrderChangeSiteID(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution.BatchOrderChangeSiteID(form);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }
        // set the Qty to Invoice value to [Qty fulfilled] instead of [QTY Allocated].
        [HttpPost]
        public JsonResult BatchOrderModifyQtyToInvoice(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.BatchOrderModifyQtyToInvoice(form.Batch);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult Promo() => View();

        [HttpPost]
        public JsonResult PromoRecords()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.PromoRecords()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult StateList()
        {
            try
            {
                return Json(new List<object>()
                {
                    Distribution.StateDroplist()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult PromoNew() => View();

        [HttpPost]
        public JsonResult PromoDelete(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.PromoUpdate("promo_delete", promo);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult PromoSave(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                if (promo.Id == 0)
                    Distribution_DB.PromoUpdate("create", promo);
                else
                    Distribution_DB.PromoUpdate("edit", promo);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpGet]
        public ActionResult PromoEdit(int id)
        {
            ViewBag.promoid = id;
            return View();
        }

        [HttpPost]
        public JsonResult PromoEditRecord(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Distribution.PromoDetail(promo.Id));
                objectList.Add(Distribution.StateDroplist());
                string filename1 = "PromoItems_" + Utilities.GetRandom() + ".csv";
                string filePath1 = GetFilePath("Download", filename1);
                objectList.Add(Distribution.PromoItemList(promo, filePath1));
                objectList.Add("../Download/" + filename1);
                string filename2 = "OrderWithPromo_" + Utilities.GetRandom() + ".csv";
                string filePath2 = GetFilePath("Download", filename2);
                objectList.Add(Distribution.PromoAddedToOrderList(promo, filePath2));
                objectList.Add("../Download/" + filename2);
                string filename3 = "InvoiceWithPromo_" + Utilities.GetRandom() + ".csv";
                string filePath3 = GetFilePath("Download", filename3);
                objectList.Add(Distribution.PromoAddedToInvoiceList(promo, filePath3));
                objectList.Add("../Download/" + filename3);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult PromoItem() => View();

        [HttpPost]
        public JsonResult PromoItemList(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PromoItems_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.PromoItemList(promo, filePath));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult PromoItemUpdate()
        {
            List<object> objectList = new List<object>();
            try
            {
                Distribution_Class.Promo promo = new Distribution_Class.Promo()
                {
                    Id = Convert.ToInt32(Request.Form["id"])
                };
                promo.Startdate = promo.Enddate = promo.Description = promo.Storeprefix = promo.State = "";
                HttpPostedFileBase file = Request.Files["loadfile"];
                string filePath = GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                Distribution.PromoItemSave(filePath, promo);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                objectList.Add(ex.Message.ToString());
                return Json(objectList);
            }
        }

        public ActionResult PromoStore() => View();

        [HttpPost]
        public JsonResult PromoStoreUpdate()
        {
            List<object> objectList = new List<object>();
            try
            {
                Distribution_Class.Promo promo = new Distribution_Class.Promo();
                Distribution_Class.FormInput form = new Distribution_Class.FormInput();
                promo.Id = Convert.ToInt32(Request.Form["id"]);
                form.Batch = Request.Form["batch"].ToUpper().Trim();
                form.Freight = Convert.ToDecimal(Request.Form["freight"]);
                form.Comment = string.IsNullOrEmpty(Request.Form["comment"]) ? "Sales Promotional" : Request.Form["comment"].Trim();
                form.Location = Request.Form["location"];
                HttpPostedFileBase file = Request.Files["loadfile"];
                string filePath = GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                if (Convert.ToInt32(Distribution_DB.Dropship("check_for_batch_usa", invoiceNumber: form.Batch).Rows[0]["recordcount"]) > 0)
                    throw new Exception("Integration halted.   Found an existing batch: " + form.Batch + ".   The batch must be deleted in GP before continuing.");
                Distribution.PromoStoreCreateSalesOrder(filePath, promo, form);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult PromoByStore() => View();

        [HttpPost]
        public JsonResult PromoByStoreUpdate()
        {
            List<object> objectList = new List<object>();
            try
            {
                Distribution_Class.Promo promo = new Distribution_Class.Promo();
                Distribution_Class.FormInput formInput = new Distribution_Class.FormInput();
                promo.Id = Convert.ToInt32(Request.Form["id"]);
                HttpPostedFileBase file = Request.Files["loadfile"];
                string filePath = GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                Distribution_DB.PromoUpdate("by_store_delete", promo);
                Distribution.PromoByStore(filePath, promo);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                objectList.Add(ex.Message.ToString());
                return Json(objectList);
            }
        }

        [HttpPost]
        public JsonResult PromoByStoreDelete(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.PromoUpdate("by_store_delete", promo);
                return Json("Done.");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult Purchases() => View();

        [HttpPost]
        public JsonResult VendorDroplist()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.VendorDropList()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult PurchaseList(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "Purchases_" + Utilities.GetRandom() + ".csv";
                Distribution.PurchaseList(GetFilePath("Download", filename), form);
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult Dropship() => View();

        [HttpPost]
        public JsonResult DropshipRecords()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.DropshipRecords()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult DropshipNew() => View();

        [HttpPost]
        public JsonResult DropshipCompany()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.DropshipCompany(),
                  Distribution.DropshipCopyFrom()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropshipSave(Distribution_Class.Dropship drop)
        {
            try
            {
                if (drop.Id == 0)
                {
                    int num = Distribution_DB.DropshipUpdate("create", drop);
                    if (drop.CopyFromId > 0)
                    {
                        drop.Id = num;
                        Distribution.DropshipCopyFromTemplateToNewOne(drop);
                    }
                }
                else
                    Distribution_DB.DropshipUpdate("edit", drop);
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult DropshipEdit() => View();

        [HttpPost]
        public JsonResult DropshipEditRecord(Distribution_Class.Dropship drop)
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.DropshipEditRecord(drop.Id),
                  Distribution.DropshipVendorRecord(drop.Id),
                  Distribution.DropshipCompany()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropshipDelete(Distribution_Class.Dropship drop)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.DropshipUpdate("delete", drop);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult DropshipVendor() => View();

        [HttpPost]
        public JsonResult DropshipVendorSave(Distribution_Class.DropshipVendor vendor)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.DropshipVendorUpdate("create", vendor);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropshipVendorDelete(Distribution_Class.DropshipVendor vendor)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.DropshipVendorUpdate("delete", vendor);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult DropshipImport() => View();

        [HttpPost]
        public JsonResult DropshipImportData()
        {
            List<object> objectList = new List<object>();
            try
            {
                Distribution_Class.Dropship drop = new Distribution_Class.Dropship();
                Distribution_Class.FormInput formInput = new Distribution_Class.FormInput();
                drop.Id = Convert.ToInt32(Request.Form["id"]);
                HttpPostedFileBase file = Request.Files["loadfile"];
                string filePath = GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                Distribution_DB.DropshipUpdate("delete_import_data", drop);
                Distribution.DropshipImport(filePath, drop.Id);
                objectList.Add("Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                objectList.Add(ex.Message.ToString());
                return Json(objectList);
            }
        }

        [HttpPost]
        public JsonResult DropshipCustomerCheck(Distribution_Class.Dropship drop)
        {
            try
            {
                List<object> objectList = new List<object>();
                string storeNotFound = Distribution.DropshipCustomerCheck(drop);
                objectList.Add(storeNotFound != "" ? "Error: The following stores are not found in GP:\r\n" + storeNotFound : "Done");
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropshipGPIntegration(Distribution_Class.Dropship drop)
        {
            try
            {
                List<object> objectList = new List<object>();
                bool flag = false;
                drop = (Distribution_Class.Dropship)Distribution.DropshipEditRecord(drop.Id);
                // check for existing GP batch, if found throw exception.
                DataTable dt = Distribution_DB.Dropship(drop.CompanyId == 2 ? "check_for_batch_usa" : "check_for_batch_canada", invoiceNumber: (drop.Batch + "-" + DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US"))));
                if (Convert.ToInt32(dt.Rows[0]["recordcount"]) > 0)
                    throw new Exception("Integration halted.   Found an existing batch: " + drop.Batch + "-" + DateTime.Now.ToString("MMddyy", CultureInfo.CreateSpecificCulture("en-US")) + ".   The batch must be deleted in GP before continuing.");
                // invoice.
                DataTable invoiceTable1 = Distribution_DB.Dropship(drop.CompanyId == 2 ? "invoice_header_us" : "invoice_header_canada", drop.Id);
                if (invoiceTable1.Rows.Count > 0)
                {
                    Distribution.DropshipGPInvoice(drop, invoiceTable1, "INVOICE");
                    flag = true;
                }
                invoiceTable1.Clear();
                // return that has invoice.
                DataTable invoiceTable2 = Distribution_DB.Dropship(drop.Id == 2 ? "invoice_header_return_us" : "invoice_header_return_canada", drop.Id);
                if (invoiceTable2.Rows.Count > 0)
                {
                    Distribution.DropshipGPInvoice(drop, invoiceTable2, "DRTN");
                    flag = true;
                }
                // no invoice, such as tuna 56, 57.
                DataTable invoiceTable3 = Distribution_DB.Dropship("no_invoice_header", drop.Id);
                if (invoiceTable3.Rows.Count > 0)
                {
                    Distribution.DropshipGPNoInvoiceFromVendor(drop, invoiceTable3, "INVOICE");
                    flag = true;
                }
                // return with no invoice.
                DataTable invoiceTable4 = Distribution_DB.Dropship("no_invoice_header_return", drop.Id);
                if (invoiceTable4.Rows.Count > 0)
                {
                    Distribution.DropshipGPNoInvoiceFromVendor(drop, invoiceTable4, "DRTN");
                    flag = true;
                }
                if (!flag)
                    throw new Exception("No import data found.");
                // delete import data from table.
                Distribution_DB.DropshipUpdate("delete_import_data", drop);
                return Json("Done. Please verify batch before posting in GP.");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult OrderItems() => View();

        [HttpPost]
        public JsonResult OrderItemsList(Distribution_Class.Order order)
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.OrderItemsList(order.Number.Trim())
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult OrderItemsAdd() => View();

        [HttpPost]
        public JsonResult OrderItemsAddUpdate(Distribution_Class.Item item)
        {
            try
            {
                item.LineSeq = Distribution.OrderItemNextLineSequence(item.OrderNumber);
                GP.OrderItemUpdate(item, "add");
                return Json("Done.");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult OrderItemsChangeQuantity(Distribution_Class.Item item)
        {
            try
            {
                GP.OrderItemUpdate(item, "change_quantity");
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult OrderItemsDelete(Distribution_Class.Item item)
        {
            try
            {
                GP.OrderItemUpdate(item, "delete");
                return Json("Done");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult OrderItemsEdit() => View();

        [HttpPost]
        public JsonResult OrderItemsLotList(Distribution_Class.Item item)
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.OrderItemsLotAssigned(item),
                  Distribution.OrderItemsLotAvailable(item.Number)
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult OrderItemsLotDelete(Distribution_Class.Item item)
        {
            try
            {
                return Json(new List<object>()
                {
                   Distribution_DB.ItemLotUpdate("item_lot_delete", item)
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult OrderItemsLotAdd(Distribution_Class.Item item)
        {
            try
            {
                return Json(new List<object>()
                {
                   Distribution_DB.ItemLotUpdate("item_lot_insert", item)
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        //public ActionResult ItemAdjustment() => View();

        //[HttpPost]
        //public JsonResult ItemAdjustmentRun()
        //{
        //    List<object> objectList = new List<object>();
        //    try
        //    {
        //        HttpPostedFileBase file = Request.Files[0];
        //        string filePath = GetFilePath("Upload", file.FileName);
        //        Stream inputStream = file.InputStream;
        //        file.SaveAs(filePath);
        //        objectList.Add(Distribution.ItemAdjustmentRun(filePath));
        //        return Json(objectList);
        //    }
        //    catch (Exception ex)
        //    {
        //        objectList.Add(ex.Message.ToString());
        //        return Json(objectList);
        //    }
        //}

        public ActionResult ItemBin() => View();

        [HttpPost]
        public JsonResult ItemBinData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "ItemBin_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.ItemBin(filePath));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult ItemBinDelete(Distribution_Class.ItemBin itemBin)
        {
            try
            {
                Distribution_DB.ItemBinUpdate("delete", itemBin);
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult ItemBinSave(Distribution_Class.ItemBin itemBin)
        {
            try
            {
                if (itemBin.Id == 0)
                {
                    if (Convert.ToInt32(Distribution_DB.ItemBin("duplicate", itemBin).Rows[0]["recordcount"]) > 0)
                        throw new Exception("Exception: Item already exist, cannot create duplicate.");
                    Distribution_DB.ItemBinUpdate("create", itemBin);
                }
                else
                    Distribution_DB.ItemBinUpdate("edit", itemBin);
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult ItemBinNew() => View();

        public ActionResult ItemBinEdit() => View();

        public ActionResult DropLabel() => View();

        [HttpPost]
        public JsonResult DropLabelData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "DropLabel_" + Utilities.GetRandom() + ".csv";
                string filePath = GetFilePath("Download", filename);
                objectList.Add(Distribution.DropLabel(filePath));
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropLabelDelete(Distribution_Class.DropLabel drop)
        {
            try
            {
                Distribution_DB.DropLabelUpdate("delete", drop);
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropLabelSave(Distribution_Class.DropLabel drop)
        {
            try
            {
                if (drop.Id == 0)
                {
                    if (Convert.ToInt32(Distribution_DB.DropLabel("duplicate", drop).Rows[0]["recordcount"]) > 0)
                        throw new Exception("Exception: Item already exist, cannot create duplicate.");
                    Distribution_DB.DropLabelUpdate("create", drop);
                }
                else
                    Distribution_DB.DropLabelUpdate("edit", drop);
                return Json("Ok");
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult DropLabelNew() => View();

        public ActionResult DropLabelEdit() => View();

        public ActionResult InTransitBillOfLading() => View();

        [HttpPost]
        public JsonResult InTransitBillOfLadingIds()
        {
            try
            {
                return Json(new List<object>()
                {
                  Distribution.InTransitBillOfLading()
                });
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult InTransitBillofLadingData(Distribution_Class.BillofLading lading)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = lading.DocNumber + "_BillOfLading.pdf";
                string filePath = GetFilePath("Download", filename);
                Distribution_Pdf.IntransitBillOfLading(lading, filePath);
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        //public ActionResult ExternalDistributionCenterBatch() => View();

        //[HttpPost]
        //public JsonResult ExternalDistributionCenterBatchRecords(
        //  Distribution_Class.FormInput form)
        //{
        //    try
        //    {
        //        return Json(new List<object>()
        //        {
        //          Distribution.ExternalDistributionCenterBatchRecords(form.Location)
        //        });
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message.ToString());
        //    }
        //}

        //public ActionResult ExternalDistributionCenterBatchDetail() => View();

        //[HttpPost]
        //public JsonResult ExternalDistributionCenterBatchDetailData(
        //  Distribution_Class.Order order)
        //{
        //    try
        //    {
        //        List<object> objectList = new List<object>();
        //        string filename = "EDC_" + order.Batch + ".csv";
        //        string filePath = GetFilePath("Download", filename);
        //        objectList.Add(Distribution.ExternalDistributionCenterBatchDetail(order, filePath));
        //        objectList.Add("../Download/" + filename);
        //        return Json(objectList);
        //    }
        //    catch (Exception ex)
        //    {
        //        return Json(ex.Message.ToString());
        //    }
        //}

        public ActionResult WMSTrxLog() => View();

        [HttpPost]
        public JsonResult WMSTrxLogData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "WMSTrxLog.csv";
                string filePath = GetFilePath("Download", filename);
                Distribution.WMSTrxLogData(form, filePath);
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }

        public ActionResult LanterReconcile() => View();

        [HttpPost]
        public JsonResult LanterReconcileData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "LanterReconcile " + form.StartDate.Replace('/', '-') + "_" + form.EndDate.Replace('/', '-') + ".csv";
                string filePath = GetFilePath("Download", filename);
                Distribution.LanterReconcileData(form, filePath);
                objectList.Add("../Download/" + filename);
                return Json(objectList);
            }
            catch (Exception ex)
            {
                return Json(ex.Message.ToString());
            }
        }





    }
}