# Cours-Xamarin
Cours Xamarin pour l'Université d'Orléans en février 2020

Correction des TD: https://github.com/VJubert/XamarinTD
QCM https://docs.google.com/forms/d/e/1FAIpQLSeJnDumlcTuNvRkKIow8eAPY2ULiy9DB2qRhhXuRNIoaB8LdA/viewform?usp=sf_link

Plugin à utiliser : 
- Xamarin.Essentials => plein d'essentiel pour vous simplifier l'utilisation des API natives
- Newtonsoft.Json => JSON
- MonkeyCache.SQLite => cache 
  - cocher pre-release ou "inclure les versions préliminaire"

Appel HTTP =>
  - System.Net.Http.HttpClient (pas un package)
  - HttpRequestMessage pour créer la requête
  - request.Headers => pour ajouter le token
  - request.Content = new StringContent() : mettre le body dans la requête

Code pour l'utilisation des API
```csharp
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

public class ApiClient
	{
		private readonly HttpClient _client = new HttpClient();
		
		public async Task<HttpResponseMessage> Execute(HttpMethod method, string url, object data = null, string token = null)
		{
			HttpRequestMessage request = new HttpRequestMessage(method, url);

			if (data != null)
			{
				request.Content = new StringContent(JsonConvert.SerializeObject(data), Encoding.UTF8, "application/json");
			}

			if (token != null)
			{
				request.Headers.Add("Authorization", $"Bearer {token}");
			}

			return await _client.SendAsync(request);
		}

		public async Task<T> ReadFromResponse<T>(HttpResponseMessage response)
		{
			string result = await response.Content.ReadAsStringAsync();

			return JsonConvert.DeserializeObject<T>(result);
		}
	}
```


Type de retour : Response<T>
    
ex: Response<List<PlaceItemSummary>> 

string content;

JsonConvert.DeserializeObject<Response<...>>(content);

les objets à déserializer sont a récupérer sur le git : 
- /api/src/Common.Api/Dtos/Response.cs
- /api/src/TD.Api/Dtos/*.cs

Code d'exemple permettant d'uploader une image sur l'API
```csharp
HttpClient client = new HttpClient();
byte[] imageData = await client.GetByteArrayAsync("https://bnetcmsus-a.akamaihd.net/cms/blog_header/x6/X6KQ96B3LHMY1551140875276.jpg");

HttpRequestMessage request = new HttpRequestMessage(HttpMethod.Post, "https://td-api.julienmialon.com/images");
request.Headers.Authorization = new AuthenticationHeaderValue("Bearer", "__access__token__");

MultipartFormDataContent requestContent = new MultipartFormDataContent();

var imageContent = new ByteArrayContent(imageData);
imageContent.Headers.ContentType = MediaTypeHeaderValue.Parse("image/jpeg");

// Le deuxième paramètre doit absolument être "file" ici sinon ça ne fonctionnera pas
requestContent.Add(imageContent, "file", "file.jpg");

request.Content = requestContent;

HttpResponseMessage response = await client.SendAsync(request);

string result = await response.Content.ReadAsStringAsync();

if (response.IsSuccessStatusCode)
{
  Console.WriteLine("Image uploaded!");
}
else
{
  Debugger.Break();
}
```
