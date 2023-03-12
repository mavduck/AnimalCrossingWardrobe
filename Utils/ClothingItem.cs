

public class ClothingItem
{
    public String ClothingName;
    public String VariationName;
    public String[] Colors;
    public String[] Styles;
    public String ImageUrl;

    public ClothingItem(String clothingName, String variationName, String[] colors, String[] styles, String imageUrl){
        ClothingName = clothingName;
        VariationName = variationName;
        Colors = colors;
        Styles = styles;
        ImageUrl = imageUrl;
    }
    
}
