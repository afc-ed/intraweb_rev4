using intraweb_rev3.Models;
using Microsoft.CSharp.RuntimeBinder;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Runtime.CompilerServices;
using System.Web;
using System.Web.Mvc;

namespace intraweb_rev3.Controllers
{
    public class DistributionController : Controller
    {
        public ActionResult Index() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult LoadMenu()
        {
            try
            {
                return this.Json((object)new List<object>()
                {
                  Distribution.MenuList()
                });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        private string GetFilePath(string dir, string filename)
        {
            try
            {
                string str = this.Server.MapPath("~/" + dir);
                Utilities.DeleteOldFiles(str);
                return Path.Combine(str, filename);
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Controller.DistributionController.GetFilePath()");
            }
        }

        public ActionResult LowStock() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult LowStockData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "LowStock_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.LowStock(filePath, form));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult PriceList() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult PriceListData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PriceList_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.PriceList(filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult Recall() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult RecallData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "Recall_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.RecallItem(form, filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ItemLevel() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ItemLevelData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "ItemLevel_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.ItemLevel(filePath, form));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult Sales() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult SalesData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "Sales_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.Sales(filePath, form));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult StoreSales() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult StoreSalesDroplist()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.StoreDropList()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult StoreSalesData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "StoreSales_" + (object)Utilities.GetRandom() + ".csv";
                Distribution.StoreSales(this.GetFilePath("Download", filename), form);
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult InventoryQuantity() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult InventoryQuantityData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "InventoryQuantity_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.InventoryQuantity(filePath, form));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ItemSales() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ItemSalesData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "";
                string type = form.Type;
                if (!(type == "item_sales"))
                {
                    if (type == "item_turnover")
                    {
                        filename = "ItemTurnover_" + (object)Utilities.GetRandom() + ".csv";
                        Distribution.ItemTurnover(this.GetFilePath("Download", filename), form);
                    }
                }
                else
                {
                    filename = "ItemSales_" + (object)Utilities.GetRandom() + ".csv";
                    Distribution.ItemSales(this.GetFilePath("Download", filename), form);
                }
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult BatchPicklist() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult BatchPicklistIds()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.BatchPicklistIds()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult BatchPicklistData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename1 = form.Batch + "_BatchPicklist.pdf";
                string filePath1 = this.GetFilePath("Download", filename1);
                List<Distribution_Class.BatchListStore> storeList = Distribution.BatchPicklistStores(form.Batch);
                List<Distribution_Class.PicklistItem> pickList = Distribution.BatchPicklistItems(filePath1, form, storeList);
                Distribution_Pdf.BatchPicklist(form, filePath1, storeList, pickList);
                objectList.Add((object)("../Download/" + filename1));
                string filename2 = form.Batch + "_PickTicket.pdf";
                string filePath2 = this.GetFilePath("Download", filename2);
                Distribution_Pdf.PickTicket(form, filePath2);
                objectList.Add((object)("../Download/" + filename2));
                DataTable DropLabels = Distribution_DB.DropLabel("records", new Distribution_Class.DropLabel());
                string filename3 = form.Batch + "ShippingTagFrozen.pdf";
                Distribution_Pdf.ShippingTags(this.GetFilePath("Download", filename3), form, "frozen", DropLabels);
                objectList.Add((object)("../Download/" + filename3));
                string filename4 = form.Batch + "_ShippingTagDry.pdf";
                Distribution_Pdf.ShippingTags(this.GetFilePath("Download", filename4), form, "dry", DropLabels);
                objectList.Add((object)("../Download/" + filename4));
                string filename5 = form.Batch + "_PalletCount.pdf";
                Distribution_Pdf.PalletCount(this.GetFilePath("Download", filename5), form);
                objectList.Add((object)("../Download/" + filename5));
                string filename6 = form.Batch + "_BatchPicklist.csv";
                Distribution.WriteBatchPickListFile(this.GetFilePath("Download", filename6), pickList, storeList);
                objectList.Add((object)("../Download/" + filename6));
                string filename7 = form.Batch + "_BillofLading.pdf";
                Distribution_Pdf.BillofLading(this.GetFilePath("Download", filename7), form);
                objectList.Add((object)("../Download/" + filename7));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult BatchOrder() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult BatchOrderIds()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.BatchOrderIds()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult BatchOrderData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Distribution.BatchOrderData(form));
                string filename = Utilities.cleanInput(form.Batch) + "_PickTicket.csv";
                string filePath = this.GetFilePath("Download", filename);
                Distribution.PickTicketLanter(form, filePath);
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult BatchOrderUpdate(Distribution_Class.FormInput form)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          (object) Distribution.BatchOrderUpdateRun(form)
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult BatchOrderChangeSiteID(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution.BatchOrderChangeSiteID(form);
                return this.Json((object)"Done");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult Promo() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult PromoRecords()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.PromoRecords()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult StateList()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.StateDroplist()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult PromoNew() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult PromoDelete(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.PromoUpdate("promo_delete", promo);
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
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
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpGet]
        public ActionResult PromoEdit(int id)
        {
            ViewBag.promoid = id;
            return View();
      //      if (DistributionController.\u003C\u003Eo__34.\u003C\u003Ep__0 == null)
      //{
                
      //          DistributionController.\u003C\u003Eo__34.\u003C\u003Ep__0 = CallSite<Func<CallSite, object, int, object>>.Create(Binder.SetMember(CSharpBinderFlags.None, "promoId", typeof(DistributionController), (IEnumerable<CSharpArgumentInfo>)new CSharpArgumentInfo[2]
      //          {
      //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.None, (string) null),
      //    CSharpArgumentInfo.Create(CSharpArgumentInfoFlags.UseCompileTimeType, (string) null)
      //          }));
      //      }
      //      // ISSUE: reference to a compiler-generated field
      //      // ISSUE: reference to a compiler-generated field
      //      object obj = DistributionController.\u003C\u003Eo__34.\u003C\u003Ep__0.Target((CallSite)DistributionController.\u003C\u003Eo__34.\u003C\u003Ep__0, ((ControllerBase)this).get_ViewBag(), id);
      //      return (ActionResult)this.View();
        }

        [HttpPost]
        public JsonResult PromoEditRecord(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                objectList.Add(Distribution.PromoDetail(promo.Id));
                objectList.Add(Distribution.StateDroplist());
                string filename1 = "PromoItems_" + (object)Utilities.GetRandom() + ".csv";
                string filePath1 = this.GetFilePath("Download", filename1);
                objectList.Add(Distribution.PromoItemList(promo, filePath1));
                objectList.Add((object)("../Download/" + filename1));
                string filename2 = "OrderWithPromo_" + (object)Utilities.GetRandom() + ".csv";
                string filePath2 = this.GetFilePath("Download", filename2);
                objectList.Add(Distribution.PromoAddedToOrderList(promo, filePath2));
                objectList.Add((object)("../Download/" + filename2));
                string filename3 = "InvoiceWithPromo_" + (object)Utilities.GetRandom() + ".csv";
                string filePath3 = this.GetFilePath("Download", filename3);
                objectList.Add(Distribution.PromoAddedToInvoiceList(promo, filePath3));
                objectList.Add((object)("../Download/" + filename3));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult PromoItem() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult PromoItemList(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "PromoItems_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.PromoItemList(promo, filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
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
                    Id = Convert.ToInt32(this.Request.Form["id"])
                };
                promo.Startdate = promo.Enddate = promo.Description = promo.Storeprefix = promo.State = "";
                HttpPostedFileBase file = this.Request.Files["loadfile"];
                string filePath = this.GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                Distribution.PromoItemSave(filePath, promo);
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                objectList.Add((object)ex.Message.ToString());
                return this.Json((object)objectList);
            }
        }

        public ActionResult PromoStore() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult PromoStoreUpdate()
        {
            List<object> objectList = new List<object>();
            try
            {
                Distribution_Class.Promo promo = new Distribution_Class.Promo();
                Distribution_Class.FormInput form = new Distribution_Class.FormInput();
                promo.Id = Convert.ToInt32(this.Request.Form["id"]);
                form.Batch = this.Request.Form["batch"].ToUpper().Trim();
                form.Freight = Convert.ToDecimal(this.Request.Form["freight"]);
                form.Comment = string.IsNullOrEmpty(this.Request.Form["comment"]) ? "Sales Promotional" : this.Request.Form["comment"].Trim();
                form.Location = this.Request.Form["location"];
                HttpPostedFileBase file = this.Request.Files["loadfile"];
                string filePath = this.GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                if (Convert.ToInt32(Distribution_DB.Dropship("check_for_batch_usa", invoiceNumber: form.Batch).Rows[0]["recordcount"]) > 0)
                    throw new Exception("Integration halted.   Found an existing batch: " + form.Batch + ".   The batch must be deleted in GP before continuing.");
                Distribution.PromoStoreCreateSalesOrder(filePath, promo, form);
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult PromoByStore() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult PromoByStoreUpdate()
        {
            List<object> objectList = new List<object>();
            try
            {
                Distribution_Class.Promo promo = new Distribution_Class.Promo();
                Distribution_Class.FormInput formInput = new Distribution_Class.FormInput();
                promo.Id = Convert.ToInt32(this.Request.Form["id"]);
                HttpPostedFileBase file = this.Request.Files["loadfile"];
                string filePath = this.GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                Distribution_DB.PromoUpdate("by_store_delete", promo);
                Distribution.PromoByStore(filePath, promo);
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                objectList.Add((object)ex.Message.ToString());
                return this.Json((object)objectList);
            }
        }

        [HttpPost]
        public JsonResult PromoByStoreDelete(Distribution_Class.Promo promo)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.PromoUpdate("by_store_delete", promo);
                return this.Json((object)"Done.");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult Purchases() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult VendorDroplist()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.VendorDropList()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult PurchaseList(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "Purchases_" + (object)Utilities.GetRandom() + ".csv";
                Distribution.PurchaseList(this.GetFilePath("Download", filename), form);
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult Dropship() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult DropshipRecords()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.DropshipRecords()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult DropshipNew() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult DropshipCompany()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.DropshipCompany(),
          Distribution.DropshipCopyFrom()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
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
                    if ((uint)drop.CopyFromId > 0U)
                    {
                        drop.Id = num;
                        Distribution.DropshipCopyFromTemplateToNewOne(drop);
                    }
                }
                else
                    Distribution_DB.DropshipUpdate("edit", drop);
                return this.Json((object)"Done");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult DropshipEdit() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult DropshipEditRecord(Distribution_Class.Dropship drop)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.DropshipEditRecord(drop.Id),
          Distribution.DropshipVendorRecord(drop.Id),
          Distribution.DropshipCompany()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropshipDelete(Distribution_Class.Dropship drop)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.DropshipUpdate("delete", drop);
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult DropshipVendor() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult DropshipVendorSave(Distribution_Class.DropshipVendor vendor)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.DropshipVendorUpdate("create", vendor);
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropshipVendorDelete(Distribution_Class.DropshipVendor vendor)
        {
            try
            {
                List<object> objectList = new List<object>();
                Distribution_DB.DropshipVendorUpdate("delete", vendor);
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult DropshipImport() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult DropshipImportData()
        {
            List<object> objectList = new List<object>();
            try
            {
                Distribution_Class.Dropship drop = new Distribution_Class.Dropship();
                Distribution_Class.FormInput formInput = new Distribution_Class.FormInput();
                drop.Id = Convert.ToInt32(this.Request.Form["id"]);
                HttpPostedFileBase file = this.Request.Files["loadfile"];
                string filePath = this.GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                Distribution_DB.DropshipUpdate("delete_import_data", drop);
                Distribution.DropshipImport(filePath, drop.Id);
                objectList.Add((object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                objectList.Add((object)ex.Message.ToString());
                return this.Json((object)objectList);
            }
        }

        [HttpPost]
        public JsonResult DropshipCustomerCheck(Distribution_Class.Dropship drop)
        {
            try
            {
                List<object> objectList = new List<object>();
                string str = Distribution.DropshipCustomerCheck(drop);
                objectList.Add(str != "" ? (object)("The following store(s) have an error, check the import file and GP:\r\n" + str) : (object)"Done");
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
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
                if (Convert.ToInt32(Distribution_DB.Dropship(drop.CompanyId == 2 ? "check_for_batch_usa" : "check_for_batch_canada", invoiceNumber: (drop.Batch + "-" + DateTime.UtcNow.ToString("MMddyy"))).Rows[0]["recordcount"]) > 0)
                    throw new Exception("Integration halted.   Found an existing batch: " + drop.Batch + "-" + DateTime.UtcNow.ToString("MMddyy") + ".   The batch must be deleted in GP before continuing.");
                DataTable invoiceTable1 = Distribution_DB.Dropship(drop.CompanyId == 2 ? "invoice_header_us" : "invoice_header_canada", drop.Id);
                if (invoiceTable1.Rows.Count > 0)
                {
                    Distribution.DropshipGPInvoice(drop, invoiceTable1, "INVOICE");
                    flag = true;
                }
                invoiceTable1.Clear();
                DataTable invoiceTable2 = Distribution_DB.Dropship(drop.Id == 2 ? "invoice_header_return_us" : "invoice_header_return_canada", drop.Id);
                if (invoiceTable2.Rows.Count > 0)
                {
                    Distribution.DropshipGPInvoice(drop, invoiceTable2, "DRTN");
                    flag = true;
                }
                DataTable invoiceTable3 = Distribution_DB.Dropship("no_invoice_header", drop.Id);
                if (invoiceTable3.Rows.Count > 0)
                {
                    Distribution.DropshipGPNoInvoiceFromVendor(drop, invoiceTable3, "INVOICE");
                    flag = true;
                }
                DataTable invoiceTable4 = Distribution_DB.Dropship("no_invoice_header_return", drop.Id);
                if (invoiceTable4.Rows.Count > 0)
                {
                    Distribution.DropshipGPNoInvoiceFromVendor(drop, invoiceTable4, "DRTN");
                    flag = true;
                }
                if (!flag)
                    throw new Exception("No import data found.");
                Distribution_DB.DropshipUpdate("delete_import_data", drop);
                return this.Json((object)"Done. Please verify batch before posting in GP.");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult OrderItems() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult OrderItemsList(Distribution_Class.Order order)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.OrderItemsList(order.Number)
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult OrderItemsAdd() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult OrderItemsAddUpdate(Distribution_Class.Item item)
        {
            try
            {
                item.LineSeq = Distribution.OrderItemNextLineSequence(item.OrderNumber);
                GP.OrderItemUpdate(item, "add");
                return this.Json((object)"Done.");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult OrderItemsChangeQuantity(Distribution_Class.Item item)
        {
            try
            {
                GP.OrderItemUpdate(item, "change_quantity");
                return this.Json((object)"Done");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult OrderItemsDelete(Distribution_Class.Item item)
        {
            try
            {
                GP.OrderItemUpdate(item, "delete");
                return this.Json((object)"Done");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult OrderItemsEdit() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult OrderItemsLotList(Distribution_Class.Item item)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.OrderItemsLotAssigned(item),
          Distribution.OrderItemsLotAvailable(item.Number)
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult OrderItemsLotDelete(Distribution_Class.Item item)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          (object) Distribution_DB.ItemLotUpdate("item_lot_delete", item)
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult OrderItemsLotAdd(Distribution_Class.Item item)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          (object) Distribution_DB.ItemLotUpdate("item_lot_insert", item)
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ItemAdjustment() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ItemAdjustmentRun()
        {
            List<object> objectList = new List<object>();
            try
            {
                HttpPostedFileBase file = this.Request.Files[0];
                string filePath = this.GetFilePath("Upload", file.FileName);
                Stream inputStream = file.InputStream;
                file.SaveAs(filePath);
                objectList.Add((object)Distribution.ItemAdjustmentRun(filePath));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                objectList.Add((object)ex.Message.ToString());
                return this.Json((object)objectList);
            }
        }

        public ActionResult ItemBin() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ItemBinData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "ItemBin_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.ItemBin(filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult ItemBinDelete(Distribution_Class.ItemBin itemBin)
        {
            try
            {
                Distribution_DB.ItemBinUpdate("delete", itemBin);
                return this.Json((object)"Ok");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
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
                return this.Json((object)"Ok");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ItemBinNew() => (ActionResult)this.View();

        public ActionResult ItemBinEdit() => (ActionResult)this.View();

        public ActionResult DropLabel() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult DropLabelData()
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "DropLabel_" + (object)Utilities.GetRandom() + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.DropLabel(filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult DropLabelDelete(Distribution_Class.DropLabel drop)
        {
            try
            {
                Distribution_DB.DropLabelUpdate("delete", drop);
                return this.Json((object)"Ok");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
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
                return this.Json((object)"Ok");
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult DropLabelNew() => (ActionResult)this.View();

        public ActionResult DropLabelEdit() => (ActionResult)this.View();

        public ActionResult InTransitBillOfLading() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult InTransitBillOfLadingIds()
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.InTransitBillOfLading()
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        [HttpPost]
        public JsonResult InTransitBillofLadingData(Distribution_Class.BillofLading lading)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = lading.DocNumber + "_BillOfLading.pdf";
                string filePath = this.GetFilePath("Download", filename);
                Distribution_Pdf.IntransitBillOfLading(lading, filePath);
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ExternalDistributionCenterBatch() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ExternalDistributionCenterBatchRecords(
          Distribution_Class.FormInput form)
        {
            try
            {
                return this.Json((object)new List<object>()
        {
          Distribution.ExternalDistributionCenterBatchRecords(form.Location)
        });
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult ExternalDistributionCenterBatchDetail() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult ExternalDistributionCenterBatchDetailData(
          Distribution_Class.Order order)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "EDC_" + order.Batch + ".csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.ExternalDistributionCenterBatchDetail(order, filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        public ActionResult WMSTrxLog() => (ActionResult)this.View();

        [HttpPost]
        public JsonResult WMSTrxLogData(Distribution_Class.FormInput form)
        {
            try
            {
                List<object> objectList = new List<object>();
                string filename = "WMSTrxLog.csv";
                string filePath = this.GetFilePath("Download", filename);
                objectList.Add(Distribution.WMSTrxLogData(form, filePath));
                objectList.Add((object)("../Download/" + filename));
                return this.Json((object)objectList);
            }
            catch (Exception ex)
            {
                return this.Json((object)ex.Message.ToString());
            }
        }

        
    }
}