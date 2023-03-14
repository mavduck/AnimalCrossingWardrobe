using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace AnimalCrossingWardrobe.Pages;

public class IndexModel : PageModel
{
    private INookiService _nookiService;
    private IMemoryCache _cache;
    string cacheKey = "123";
    public List<ClothingItem> ClothingList {get; set;} = new List<ClothingItem>();
    public List<Villager> VillagerList {get; set;} = new List<Villager>();
    public Villager? SelectedVillager {get; set;}

    [BindProperty(SupportsGet = true)]
    public string ClothingType {get; set;} = "";

    [BindProperty(SupportsGet = true)]
    public string Color {get; set;} = "";

    [BindProperty(SupportsGet = true)]
    public string Style {get; set;} = "";

    [BindProperty(SupportsGet = true)]
    public string SelectedVillagerName {get; set;} = "";

    [BindProperty(SupportsGet = true)]
    public int PageIndex {get; set;} = 1;
    public int PageSize = 48;
    public int TotalPages {get; set;}

    public IndexModel(IMemoryCache cache, INookiService nookiService){ 

        _cache = cache;
        _nookiService = nookiService;

    }

    public async Task OnGet()
    {
        ClothingList = await _nookiService.GetClothing(ClothingType, Color, Style);

        if(ClothingList.Count > 0){
            TotalPages = (int) Math.Ceiling((Decimal)ClothingList.Count/(Decimal)PageSize);

            //If the PageIndex exceeds the TotalPages, keep user on the last page
            //If the PageIndex is negative or zero, keep user on the first page
            PageIndex = PageIndex > TotalPages ? TotalPages : PageIndex <= 0 ? 1 : PageIndex;

            //If the remaining clothing items to view is less than page size, only get that amount
            int NumClothingItemFromPageIndex = ClothingList.Count - ((PageIndex - 1) * PageSize);
            PageSize = NumClothingItemFromPageIndex <= PageSize ? NumClothingItemFromPageIndex : PageSize;

            ClothingList = ClothingList.GetRange((PageIndex - 1) * PageSize, PageSize);
        }
        else {
            PageIndex = 1;
            TotalPages = 1;
        }

        if(!_cache.TryGetValue(cacheKey, out List<Villager> Villagers)){
            Villagers = await _nookiService.GetVillagers();
            _cache.Set(cacheKey, Villagers);
        }
        
        VillagerList = Villagers;

        if(!String.IsNullOrEmpty(SelectedVillagerName)){
            SelectedVillager = VillagerList.Find(v=> v.Name == SelectedVillagerName);  
        }  
    }
}

