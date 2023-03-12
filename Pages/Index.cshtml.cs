using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.Extensions.Caching.Memory;
using System.Text.Json;

namespace AnimalCrossingWardrobe.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;
    private IMemoryCache _cache;

    string cacheKey = "123";
    string clothingCacheKey = "456";

    public Clothing[] ClothingList {get; set;} = new Clothing[0];
    public Villager[] VillagerList {get; set;} = new Villager[0];
    public string[] CompareStyles = new string[0];
    public string[] CompareColors = new string[0];

    [BindProperty(SupportsGet = true)]
    public string? ClothingCategory {get; set;}

    [BindProperty(SupportsGet = true)]
    public string? Color {get; set;}

    [BindProperty(SupportsGet = true)]
    public string? Style {get; set;}

    [BindProperty(SupportsGet = true)]
    public string? SelectedVillagerName {get; set;}

    public IndexModel(IHttpClientFactory httpClientFactory, IMemoryCache cache){ 

        _httpClientFactory = httpClientFactory;
        _cache = cache;

    }

    public async Task OnGet()
    {
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
            IEnumerable<Clothing>? clothing = await JsonSerializer.DeserializeAsync<IEnumerable<Clothing>>(contentStream);
            ClothingList = clothing?.Take(100).ToArray() ?? new Clothing[0];
        }

        if(!_cache.TryGetValue(cacheKey, out IEnumerable<Villager>? Villagers)){
            String getVillagers = "/villagers?game=nh&nhdetails=true";
            httpResponseMessage = await httpClient.GetAsync(getVillagers);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                Villagers = await JsonSerializer.DeserializeAsync<IEnumerable<Villager>>(contentStream);
                _cache.Set(cacheKey, Villagers);
            }
        }

        VillagerList = Villagers?.ToArray() ?? new Villager[0];

        if(!string.IsNullOrEmpty(SelectedVillagerName)){
            Villager? SelectedVillager = Array.Find(VillagerList, v=> v.Name == SelectedVillagerName);
                CompareColors = SelectedVillager?.Details.Colors ?? new string[0];
                CompareStyles = SelectedVillager?.Details.Styles ?? new string[0];
        }

    }
}

