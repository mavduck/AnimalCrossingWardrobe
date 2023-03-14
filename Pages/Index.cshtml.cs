using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalCrossingWardrobe.Pages;

public class IndexModel : PageModel
{
    private INookiService _nookiService;
    private IMemoryCache _cache;
    string cacheKey = "123";
    public ClothingItem[] ClothingList {get; set;} = new ClothingItem[0];
    public Villager[] VillagerList {get; set;} = new Villager[0];
    public string[] CompareStyles = new string[0];
    public string[] CompareColors = new string[0];
    public string SelectedVillagerIconUrl = "";

    [BindProperty(SupportsGet = true)]
    public string ClothingCategory {get; set;} = "";

    [BindProperty(SupportsGet = true)]
    public string Color {get; set;} = "";

    [BindProperty(SupportsGet = true)]
    public string Style {get; set;} = "";

    [BindProperty(SupportsGet = true)]
    public string SelectedVillagerName {get; set;} = "";

    [BindProperty(SupportsGet = true)]
    public int PageIndex {get; set;} = 1;
    public int PageSize = 48;
    public int ClothingCount {get; set;}
    public int TotalPages {get; set;}

    public IndexModel(IMemoryCache cache, INookiService nookiService){ 

        _cache = cache;
        _nookiService = nookiService;

    }

    public async Task OnGet()
    {
        ClothingList = await _nookiService.GetClothing(ClothingCategory, Color, Style);
        ClothingCount = ClothingList.Length;
        TotalPages = (int) Math.Ceiling((Decimal)ClothingCount/(Decimal)PageSize);
        ClothingList = ClothingList.Skip((PageIndex - 1) * PageSize).Take(PageSize).ToArray();

        if(!_cache.TryGetValue(cacheKey, out Villager[] Villagers)){
            Villagers = await _nookiService.GetVillagers();
            _cache.Set(cacheKey, Villagers);
        }
        
        VillagerList = Villagers;

        if(!string.IsNullOrEmpty(SelectedVillagerName)){
            Villager? SelectedVillager = Array.Find(VillagerList, v=> v.Name == SelectedVillagerName);
            if(SelectedVillager == null){
                SelectedVillagerName = "";
                CompareColors = new string[0];
                CompareStyles = new string[0];
            }
            else{
                SelectedVillagerIconUrl = SelectedVillager.Details.IconUrl;
                CompareColors = SelectedVillager.Details.Colors;
                CompareStyles = SelectedVillager.Details.Styles;
            }
                
        }
    }
}

