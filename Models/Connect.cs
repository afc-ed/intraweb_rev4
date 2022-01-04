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


    }
}