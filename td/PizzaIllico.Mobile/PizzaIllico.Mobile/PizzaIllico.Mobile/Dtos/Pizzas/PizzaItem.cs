using Newtonsoft.Json;

namespace PizzaIllico.Mobile.Dtos.Pizzas
{
    public class PizzaItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("description")]
        public string Description { get; set; }
        
        [JsonProperty("price")]
        public double Price { get; set; }
        
        [JsonProperty("out_of_stock")]
        public bool OutOfStock { get; set; }
    }
}