using Newtonsoft.Json;

namespace PizzaIllico.Mobile.Dtos.Pizzas
{
    public class ShopItem
    {
        [JsonProperty("id")]
        public long Id { get; set; }
        
        [JsonProperty("name")]
        public string Name { get; set; }
        
        [JsonProperty("address")]
        public string Address { get; set; }
        
        [JsonProperty("latitude")]
        public double Latitude { get; set; }
        
        [JsonProperty("longitude")]
        public double Longitude { get; set; }
        
        [JsonProperty("minutes_per_kilometer")]
        public double MinutesPerKilometer { get; set; }
    }
}