using System;

namespace intraweb_rev3.Models
{
    public class Ecommerce_Class
    {
        public class Menu
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
        }

        public class FormInput
        {
            public string Class { get; set; } = "";
            public string Product { get; set; } = "";
            public string Region { get; set; } = "";
            public string Login { get; set; } = "";
            public string Type { get; set; } = "";
            public string Item { get; set; } = "";
            public string All { get; set; } = "";
        }

        public class Option
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
        }

        public class Item
        {
            public int Id { get; set; } = 0;
            public string Code { get; set; } = "";
            public string Description { get; set; } = "";
            public decimal Price { get; set; } = 0;
            public string UOM { get; set; } = "";
            public string Class { get; set; } = "";
            public string IsAllowed { get; set; } = "";
            public string Status { get; set; } = "";
            public string IsActive { get; set; } = "";
        }

        public class Analytics
        {
            public string Column0 { get; set; } = "";
            public string Column1 { get; set; } = "";
            public string Column2 { get; set; } = "";
        }
    }
}