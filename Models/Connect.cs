using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace intraweb_rev3.Models
{
    public class Connect
    {
        public static object MemoGrid()
        {
            try
            {
                Connect_Class.Memo memo = new Connect_Class.Memo();
                List<Connect_Class.Memo> memoList = new List<Connect_Class.Memo>();
                DataTable table = Connect_DB.Memo("search");
                foreach (DataRow row in table.Rows)
                {
                    memo.Id = Convert.ToInt32(row[0]);
                    memo.Title = HttpContext.Current.Server.HtmlEncode(row[1].ToString());
                    memo.Active = Convert.ToInt32(row[2]) > 0 ? "Yes" : "No";
                    memo.ActiveStatus = Convert.ToInt32(row[2]) > 0 ? "item-label text-success" : "item-label text-danger";
                    memo.ModifiedOn = row[3].ToString();
                    memoList.Add(memo);
                    memo = new Connect_Class.Memo();
                }
                return memoList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Connect.MemoGrid()");
            }
        }

        public static object MemoDetailGetData(Connect_Class.Memo memo)
        {
            try
            {
                DataTable table = Connect_DB.Memo("detail", memo.Id);
                foreach (DataRow row in table.Rows)
                {                    
                    memo.Title = HttpContext.Current.Server.HtmlEncode(row[0].ToString());
                    memo.Active = Convert.ToInt32(row[1]) > 0 ? "Yes" : "No";
                    memo.PageContent = row[2].ToString();
                    memo.Region = row[3].ToString();
                    memo.Storegroup = row[4].ToString();
                    memo.State = row[5].ToString();
                    memo.CreatedOn = row[6].ToString();
                    memo.ModifiedOn = row[7].ToString();
                }
                return memo;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Connect.MemoDetailGetData()");
            }
        }


    }
}