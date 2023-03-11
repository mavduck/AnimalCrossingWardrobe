using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace AnimalCrossingWardrobe.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

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
    public string? SelectedVillager {get; set;}

    public IndexModel(IHttpClientFactory httpClientFactory){ 

        _httpClientFactory = httpClientFactory;
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
            ClothingList = clothing?.Take(20).ToArray() ?? new Clothing[0];
        }

        String getVillagers = "/villagers?game=nh";
        httpResponseMessage = await httpClient.GetAsync(getVillagers);
        if (httpResponseMessage.IsSuccessStatusCode)
        {
            Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
            IEnumerable<Villager>? villagers = await JsonSerializer.DeserializeAsync<IEnumerable<Villager>>(contentStream);
            VillagerList = villagers?.ToArray() ?? new Villager[0];
        }

        if(!string.IsNullOrEmpty(SelectedVillager)){
            String getVillager = "/villagers?game=nh&nhdetails=true&name=" + SelectedVillager;
            httpResponseMessage = await httpClient.GetAsync(getVillager);
            if (httpResponseMessage.IsSuccessStatusCode)
            {
                Stream contentStream = await httpResponseMessage.Content.ReadAsStreamAsync();
                IEnumerable<Villager>? villager = await JsonSerializer.DeserializeAsync<IEnumerable<Villager>>(contentStream);
                CompareColors = villager?.First().Details.Colors ?? new string[0];
                CompareStyles = villager?.First().Details.Styles ?? new string[0];
            }
        }

    }
}

