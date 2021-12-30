using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace intraweb_rev3.Models
{
    public class Legal_Class
    {
        public class Menu
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class ConnectFilter
        {
            public string Region { get; set; } = "";
            public string Storegroup { get; set; } = "";
            public string State { get; set; } = "";
        }

        public class ConnectMemo
        {
            public int Id { get; set; } = 0;
            public string Title { get; set; } = "";
            public string Active { get; set; } = "";
            public string CreatedOn { get; set; } = "";
            public string ModifiedOn { get; set; } = "";
            public string PageContent { get; set; } = "";
            public string Region { get; set; } = "";
            public string Storegroup { get; set; } = "";
            public string State { get; set; } = "";
        }



    }
}