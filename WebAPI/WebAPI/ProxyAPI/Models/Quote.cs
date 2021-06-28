using System;

namespace ProxyAPI
{
    public class Quote
    {
        public string sPrice { get { return $"{price/100.0:0.##}"; } }

        public int price { get; set; }

        public int providerId;

        public string description { get; set; }

        public string date { get; set; }

        public Quote() { }
        public Quote(int price, int id, string desc)
        {
            date = DateTime.Now.ToShortDateString();
            this.price = price;
            providerId = id;
            description = desc;
        }
    }
}
