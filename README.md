# Cours-Xamarin-2019
Cours Xamarin pour l'Université d'Orléans en février/mars 2019


Plugin à utiliser : 
- Xam.Plugins.Settings => stockage simple
- Xam.Plugin.Media => prise de photo/gallery
- Xam.Plugin.Geolocator => geolocalisation
- Newtonsoft.Json => JSON
- MonkeyCache.SQLite => cache 
  - cocher pre-release ou "inclure les versions préliminaire"

Appel HTTP =>
  - System.Net.Http.HttpClient (pas un package)
  - HttpRequestMessage pour créer la requête
  - request.Headers => pour ajouter le token
  - request.Content = new StringContent() : mettre le body dans la requête
  
Type de retour : Response<T>
    
ex: Response<List<PlaceItemSummary>> 

string content;

JsonConvert.DeserializeObject<Response<...>>(content);

les objets à déserializer sont a récupérer sur le git : 
- /api/src/Common.Api/Dtos/Response.cs
- /api/src/TD.Api/Dtos/*.cs

