using System;

namespace intraweb_rev3.Models
{
    public class RnD_Class
    {
        public class Menu
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
        }

        public class Option
        {
            public string Id { get; set; } = "";
            public string Name { get; set; } = "";
        }

        public class FormInput
        {
            public string Class { get; set; } = "";
            public int CopyFromId { get; set; } = 0;
            public string Action { get; set; } = "";
            public string Startdate { get; set; } = "";
            public string Enddate { get; set; } = "";
        }

        public class Customer
        {
            public string Number { get; set; } = "";
            public string Name { get; set; } = "";
            public string Class { get; set; } = "";
            public string Address1 { get; set; } = "";
            public string City { get; set; } = "";
            public string State { get; set; } = "";
            public string Zip { get; set; } = "";
            public string Country { get; set; } = "";
            public string Phone { get; set; } = "";
            public string ModifiedDate { get; set; } = "";
            public string Inactive { get; set; } = "";
            public string Hold { get; set; } = "";
        }

        public class ConnectUser
        {
            public string FCID { get; set; } = "";
            public int Id { get; set; } = 0;
            public string Name { get; set; } = "";
            public string Email { get; set; } = "";
            public string Password { get; set; } = "";
            public string Webactive { get; set; } = "";
        }

        public class CustomerClass
        {
            public int Id { get; set; } = 0;
            public string ClassId { get; set; } = "";
            public string Description { get; set; } = "";
            public bool SpecialBrownRice { get; set; } = false;
            public bool No6Container { get; set; } = false;
            public bool TunaSaku { get; set; } = false;
            public bool TunaTatakimi { get; set; } = false;
            public bool Eel { get; set; } = false;
            public bool RetailSeaweed { get; set; } = false;
            public bool RetailGingerBottle { get; set; } = false;
            public bool RetailGingerCup { get; set; } = false;
            public bool MSCTuna { get; set; } = false;
        }

        public class Safeway
        {
            public int Storenumber { get; set; } = 0;
            public string Storecode { get; set; } = "";
            public DateTime Salesdate { get; set; }
            public string UPC { get; set; } = "";
            public string Item { get; set; } = "";
            public decimal Quantity { get; set; } = 0;
            public decimal Gross { get; set; } = 0;
            public decimal Markdown { get; set; } = 0;
            public decimal Net { get; set; } = 0;
            public string Category { get; set; } = "";
            public string Region { get; set; } = "";
            public string Division { get; set; } = "";
            public string Brand { get; set; } = "";
            public DateTime Startdate { get; set; }
            public DateTime Enddate { get; set; }
        }
    }
}