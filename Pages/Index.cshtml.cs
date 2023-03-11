using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.WebUtilities;
using System.Text.Json;

namespace AnimalCrossingWardrobe.Pages;

public class IndexModel : PageModel
{
    private readonly IHttpClientFactory _httpClientFactory;

    public Clothing[] ClothingList {get; set;} = new Clothing[0];
    public Villager[] VillagerList {get; set;} = new Villager[0];

    [BindProperty(SupportsGet = true)]
    public string? ClothingCategory {get; set;}

    [BindProperty(SupportsGet = true)]
    public string? Color {get; set;}

    [BindProperty(SupportsGet = true)]
    public string? Style {get; set;}

    [BindProperty(SupportsGet = true)]
    public string? SelectedVillager {get; set;}

    public string[] CompareStyles = new string[0];
    public string[] CompareColors = new string[0];
    public List<SelectListItem> Categories { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Tops", Text = "Tops" },
            new SelectListItem { Value = "Bottoms", Text = "Bottoms" },
            new SelectListItem { Value = "Dress-up", Text = "Dresses"  },
            new SelectListItem { Value = "Headwear", Text = "Headwear"  },
            new SelectListItem { Value = "Accessories", Text = "Accessories"  },
            new SelectListItem { Value = "Socks", Text = "Socks"  },
            new SelectListItem { Value = "Shoes", Text = "Shoes"  },            
            new SelectListItem { Value = "Bags", Text = "Bags"  },         
            new SelectListItem { Value = "Umbrellas", Text = "Umbrellas"  },
        };
    public List<SelectListItem> Colors { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Aqua", Text = "Aqua" },
            new SelectListItem { Value = "Beige", Text = "Beige" },
            new SelectListItem { Value = "Black", Text = "Black"  },
            new SelectListItem { Value = "Blue", Text = "Blue"  },
            new SelectListItem { Value = "Brown", Text = "Brown"  },
            new SelectListItem { Value = "Colorful", Text = "Colorful"  },
            new SelectListItem { Value = "Gray", Text = "Gray"  },            
            new SelectListItem { Value = "Green", Text = "Green"  },         
            new SelectListItem { Value = "Orange", Text = "Orange"  },            
            new SelectListItem { Value = "Pink", Text = "Pink"  },
            new SelectListItem { Value = "Purple", Text = "Purple"  },
            new SelectListItem { Value = "Red", Text = "Red"  },           
            new SelectListItem { Value = "White", Text = "White"  },
            new SelectListItem { Value = "Yellow", Text = "Yellow"  }
        };
    public List<SelectListItem> Styles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Active", Text = "Active" },
            new SelectListItem { Value = "Cool", Text = "Cool" },
            new SelectListItem { Value = "Cute", Text = "Cute"  },
            new SelectListItem { Value = "Elegant", Text = "Elegant"  },
            new SelectListItem { Value = "Gorgeous", Text = "Gorgeous"  },
            new SelectListItem { Value = "Simple", Text = "Simple"  },
        };

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

