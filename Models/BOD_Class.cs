using System;

namespace intraweb_rev3.Models
{
    public class BOD_Class
    {
        public class Menu
        {
            public string Id { get; set; }
            public string Name { get; set; }
        }

        public class FormInput
        {
            public string Id { get; set; } = "";
            public string FromDate { get; set; } = "";
        }

        public class Item
        {
            public string Code { get; set; } = "";
            public string Description { get; set; } = "";
            public string UOM { get; set; } = "";
            public int UOMQty { get; set; } = 0;
            public decimal Cost { get; set; } = 0;
            public decimal ExtCost { get; set; } = 0;
            public decimal Price { get; set; } = 0;
            public decimal ExtPrice { get; set; } = 0;
            public string Status { get; set; } = "";
            public string Type { get; set; } = "";
            public string Category { get; set; } = "";  
        }
    }
}