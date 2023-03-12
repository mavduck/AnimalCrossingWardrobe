using System.Text.Json;
using Microsoft.AspNetCore.WebUtilities;

public interface INookiService {
    Task<ClothingItem[]> GetClothing(String? ClothingCategory, String? Color, String? Style);
    Task<Villager[]> GetVillagers();
}
public class NookiService : INookiService {
    private readonly IHttpClientFactory _httpClientFactory;

    public NookiService(IHttpClientFactory httpClientFactory){
        _httpClientFactory = httpClientFactory;
    }

    public async Task<ClothingItem[]> GetClothing(String? ClothingCategory, String? Color, String? Style){
        Dictionary<string,string?> queries = new Dictionary<string, string?>();

        if(!string.IsNullOrEmpty(ClothingCategory)){
            queries.Add("category", ClothingCategory);
        }
        if(!string.IsNullOrEmpty(Color)){
            queries.Add("color", Color);
        }
        if(!string.IsNullOrEmpty(Style)){
            queries.Add("style",Style);
        }

        String getClothing = QueryHelpers.AddQueryString("/nh/clothing", queries);

        HttpClient httpClient = _httpClientFactory.CreateClient("Nookipedia");
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(getClothing);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            IEnumerable<Clothing>? Clothing = await JsonSerializer.DeserializeAsync<IEnumerable<Clothing>>(contentStream);
            IEnumerable<ClothingItem> Variations = from item in Clothing
                                        from variation in item.Variations
                                        select new ClothingItem(item.Name, variation.VariationName, variation.Colors, item.Styles, variation.ImageUrl);
            return Variations.Take(50).ToArray();                     
        }

        return new ClothingItem[0];
    }

    public async Task<Villager[]> GetVillagers(){
        HttpClient httpClient = _httpClientFactory.CreateClient("Nookipedia");
        String getVillagers = "/villagers?game=nh&nhdetails=true";
        HttpResponseMessage httpResponseMessage = await httpClient.GetAsync(getVillagers);

        if (httpResponseMessage.IsSuccessStatusCode)
        {
            Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            var villagers = await JsonSerializer.DeserializeAsync<IEnumerable<Villager>>(contentStream);
            return villagers?.ToArray() ?? new Villager[0];
        }
        return new Villager[0];

    }


}