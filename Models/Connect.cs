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
                    memo.Active = Convert.ToInt32(row[2]);
                    memo.ActiveStatus = memo.Active > 0 ? "item-label text-success" : "item-label text-danger";
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
                    memo.Active = Convert.ToInt32(row[1]);
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

        public static object FilterGrid(Connect_Class.Filter filter)
        {
            try
            {
                Connect_Class.FilterGrid grid = new Connect_Class.FilterGrid();
                List<Connect_Class.FilterGrid> gridList = new List<Connect_Class.FilterGrid>();
                DataTable table = Connect_DB.FilterGrid(filter);
                foreach (DataRow row in table.Rows)
                {
                    switch ( filter.Type )
                    {
                    case "region":
                        grid.Id = Convert.ToInt32(row[0]);
                        grid.Name = row[1].ToString(); 
                        grid.Code = row[1].ToString();
                        break;
                    case "state":
                        grid.Id = Convert.ToInt32(row[0]);
                        grid.Code = row[1].ToString();
                        grid.Name = row[2].ToString();
                        grid.Country = row[3].ToString();
                        grid.Region = row[4].ToString();
                        break;
                    case "storegroup":
                        grid.Id = Convert.ToInt32(row[0]);
                        grid.Name = row[1].ToString();
                        break;
                    }
                    gridList.Add(grid);
                    // reset
                    grid = new Connect_Class.FilterGrid();
                }
                return gridList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Connect.Filter()");
            }
        }




    }
}