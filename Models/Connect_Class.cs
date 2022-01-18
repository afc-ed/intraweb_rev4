using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace intraweb_rev3.Models
{
    public class Connect_Class
    {

        public class Filter
        {
            public int Id { get; set; } = 0;
            public string Parent { get; set; } = "";
            public string Type { get; set; } = "";
            public string Region { get; set; } = "";
            public string Storegroup { get; set; } = "";
            public string State { get; set; } = "";
            public string RegionId { get; set; } = "";
            public string StoregroupId { get; set; } = "";
            public string StateId { get; set; } = "";
        }

        public class FilterGrid
        {
            public int Id { get; set; } = 0;
            public string Name { get; set; } = "";
            public string Code { get; set; } = "";
            public string Country { get; set; } = "";
            public string Region { get; set; } = "";            
        }

        public class Memo
        {
            public int Id { get; set; } = 0;
            public string Title { get; set; } = "";
            public string Active { get; set; } = "";
            public string ActiveStatus { get; set; } = "";
            public string PageContent { get; set; } = "";
            public string CreatedOn { get; set; } = "";
            public string ModifiedOn { get; set; } = "";
            public string Region { get; set; } = "";
            public string Storegroup { get; set; } = "";
            public string State { get; set; } = "";
        }

        public class Announcement
        {
            public int Id { get; set; } = 0;
            public string Title { get; set; } = "";
            public string Active { get; set; } = "";
            public string ActiveStatus { get; set; } = "";
            public string PageContent { get; set; } = "";
            public string CreatedOn { get; set; } = "";
            public string ModifiedOn { get; set; } = "";
            public string Region { get; set; } = "";
            public string Storegroup { get; set; } = "";
            public string State { get; set; } = "";
        }

        public class Form
        {
            public int Id { get; set; } = 0;
            public string Title { get; set; } = "";
            public string Category { get; set; } = "";
            public string PageContent { get; set; } = "";
            public int NotificationID { get; set; } = 0;
            public int SubmitFlag { get; set; } = 0;
            public int FileFlag { get; set; } = 0;
            public string Country { get; set; } = "";
            public string Filelink { get; set; } = "";
            public string Active { get; set; } = "";
            public string ActiveStatus { get; set; } = "";            
            public string CreatedOn { get; set; } = "";
            public string ModifiedOn { get; set; } = "";
            public string Region { get; set; } = "";
            public string Storegroup { get; set; } = "";
            public string State { get; set; } = "";
        }


    }
}