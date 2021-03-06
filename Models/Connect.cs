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
                    memo.Active = row[2].ToString();
                    memo.ActiveStatus = memo.Active != "0" ? "item-label text-success" : "item-label text-danger";
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
                    memo.Active = row[1].ToString();
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

        public static object FilterOptions(Connect_Class.Filter filter)
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
                        grid.Code = row[2].ToString();
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
                throw Utilities.ErrHandler(ex, "Model.Connect.FilterOptions()");
            }
        }
        public static object AnnouncementGrid()
        {
            try
            {
                Connect_Class.Announcement announcement = new Connect_Class.Announcement();
                List<Connect_Class.Announcement> announcementList = new List<Connect_Class.Announcement>();
                DataTable table = Connect_DB.Announcement("search");
                foreach (DataRow row in table.Rows)
                {
                    announcement.Id = Convert.ToInt32(row[0]);
                    announcement.Title = HttpContext.Current.Server.HtmlEncode(row[1].ToString());
                    announcement.Active = row[2].ToString();
                    announcement.ActiveStatus = announcement.Active != "0" ? "item-label text-success" : "item-label text-danger";
                    announcement.ModifiedOn = row[3].ToString();
                    announcementList.Add(announcement);
                    announcement = new Connect_Class.Announcement();
                }
                return announcementList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Connect.AnnouncementGrid()");
            }
        }

        public static object AnnouncementDetailGetData(Connect_Class.Announcement announcement)
        {
            try
            {
                DataTable table = Connect_DB.Announcement("detail", announcement.Id);
                foreach (DataRow row in table.Rows)
                {
                    announcement.Title = HttpContext.Current.Server.HtmlEncode(row[0].ToString());
                    announcement.Active = row[1].ToString();
                    announcement.PageContent = row[2].ToString();
                    announcement.Region = row[3].ToString();
                    announcement.Storegroup = row[4].ToString();
                    announcement.State = row[5].ToString();
                    announcement.CreatedOn = row[6].ToString();
                    announcement.ModifiedOn = row[7].ToString();
                }
                return announcement;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Connect.AnnoucementDetailGetData()");
            }
        }

        public static object FormGrid()
        {
            try
            {
                Connect_Class.Form form = new Connect_Class.Form();
                List<Connect_Class.Form> formList = new List<Connect_Class.Form>();
                DataTable table = Connect_DB.Form("search");
                foreach (DataRow row in table.Rows)
                {
                    form.Id = Convert.ToInt32(row[0]);
                    form.Title = HttpContext.Current.Server.HtmlEncode(row[1].ToString());
                    form.Active = row[2].ToString();
                    form.ActiveStatus = form.Active != "0" ? "item-label text-success" : "item-label text-danger";
                    form.SubmitFlag = row[3].ToString();
                    form.FileFlag = row[4].ToString();
                    form.Country = row[5].ToString();
                    form.ModifiedOn = row[6].ToString();
                    formList.Add(form);
                    form = new Connect_Class.Form();
                }
                return formList;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Connect.FormGrid()");
            }
        }

        public static object FormDetailGetData(Connect_Class.Form form)
        {
            try
            {
                DataTable table = Connect_DB.Form("detail", form.Id);
                foreach (DataRow row in table.Rows)
                {
                    form.Title = HttpContext.Current.Server.HtmlEncode(row[0].ToString());
                    form.Active = row[1].ToString();
                    form.PageContent = row[2].ToString();
                    form.Region = row[3].ToString();
                    form.Storegroup = row[4].ToString();
                    form.State = row[5].ToString();
                    form.CreatedOn = row[6].ToString();
                    form.ModifiedOn = row[7].ToString();
                }
                return form;
            }
            catch (Exception ex)
            {
                throw Utilities.ErrHandler(ex, "Model.Connect.FormDetailGetData()");
            }
        }




    }
}