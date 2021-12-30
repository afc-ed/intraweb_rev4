using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace intraweb_rev3.Models
{
    public class Legal
    {
        public static object GetMenuList()
        {
            try
            {
                List<Legal_Class.Menu> menuList = new List<Legal_Class.Menu>();
                menuList.Add(new Legal_Class.Menu()
                {
                    Id = "ConnectMemo",
                    Name = "Connect Memo"
                });
                menuList.Add(new Legal_Class.Menu()
                {
                    Id = "ConnectAnnouncement",
                    Name = "Connect Announcement"
                });
                menuList.Add(new Legal_Class.Menu()
                {
                    Id = "ConnectVideo",
                    Name = "Connect Video"
                });
                menuList.Add(new Legal_Class.Menu()
                {
                    Id = "ConnectForm",
                    Name = "Connect Form"
                });
                menuList.Sort((x, y) => x.Name.CompareTo(y.Name));
                return menuList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Legal.MenuList()");
            }
        }

        public static object ConnectMemoList()
        {
            try
            {
                BOD_Class.Item item = new BOD_Class.Item();
                List<BOD_Class.Item> itemList = new List<BOD_Class.Item>();
                DataTable table = BOD_DB.GetProduct("priceList");
                foreach (DataRow row in table.Rows)
                {
                    item.Code = row["item"].ToString().Trim();
                    item.Description = row["itemdesc"].ToString();
                    item.UOM = row["uom"].ToString().Trim();
                    item.UOMQty = Convert.ToInt32(row["uomqty"]);
                    item.Cost = Convert.ToDecimal(row["currcost"]);
                    item.ExtCost = item.Cost * (Decimal)item.UOMQty;
                    item.Price = Convert.ToDecimal(row["price"]);
                    item.ExtPrice = item.Price * (Decimal)item.UOMQty;
                    item.Status = row["itemstatus"].ToString();
                    item.Type = row["itemtype"].ToString();
                    item.Category = row["category"].ToString();
                    itemList.Add(item);
                    item = new BOD_Class.Item();
                }
              
                return itemList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.BOD.GetPriceList()");
            }
        }



    }
}