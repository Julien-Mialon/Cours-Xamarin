namespace PizzaIllico.Mobile.Services
{
    public static class Urls
    {
        private const string ROOT = "api/v1";

        public const string REFRESH_TOKEN = ROOT + "/authentication/refresh";
        public const string LOGIN_WITH_CREDENTIALS = ROOT + "/authentication/credentials";
        public const string SET_PASSWORD = ROOT + "/authentication/credentials/set";
        
        public const string USER_PROFILE = ROOT + "/accounts/me";
        public const string CREATE_USER = ROOT + "/accounts/register";
        public const string SET_USER_PROFILE = ROOT + "/accounts/me";
        
        public const string LIST_SHOPS = ROOT + "/shops";
        public const string LIST_PIZZA = ROOT + "/shops/{shopId}/pizzas";
        public const string GET_IMAGE = ROOT + "/shops/{shopId}/pizzas/{pizzaId}/image";
        public const string DO_ORDER = ROOT + "/shops/{shopId}";
        public const string LIST_ORDERS = ROOT + "/orders";
    }
}