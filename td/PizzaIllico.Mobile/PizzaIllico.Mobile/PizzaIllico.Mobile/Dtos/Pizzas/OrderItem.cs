using System;
using Newtonsoft.Json;

namespace PizzaIllico.Mobile.Dtos.Pizzas
{
    public class OrderItem
    {
        [JsonProperty("shop")]
        public ShopItem Shop { get; set; }
        
        [JsonProperty("date")]
        public DateTime Date { get; set; }
        
        [JsonProperty("amount")]
        public double Amount { get; set; }
    }
}