# Cours-Xamarin
Cours Xamarin pour l'Université d'Orléans en février 2020

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
