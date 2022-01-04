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
                    Id = "/Connect/Memo",
                    Name = "Connect Memo"
                });
                menuList.Add(new Legal_Class.Menu()
                {
                    Id = "/Connect/Announcement",
                    Name = "Connect Announcement"
                });
                menuList.Add(new Legal_Class.Menu()
                {
                    Id = "/Connect/Video",
                    Name = "Connect Video"
                });
                menuList.Add(new Legal_Class.Menu()
                {
                    Id = "/Connect/Form",
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

       



    }
}