using System.Collections.Generic;
using Newtonsoft.Json;

namespace PizzaIllico.Mobile.Dtos.Pizzas
{
    public class CreateOrderRequest
    {
        [JsonProperty("pizza_ids")]
        public List<long> PizzaIds { get; set; }
    }
}