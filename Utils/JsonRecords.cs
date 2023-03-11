using System.Text.Json.Serialization;

public record class Clothing(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("styles")] string[] Styles,
    [property: JsonPropertyName("variations")] Variation[] Variations
);

public record class Variation(
    [property: JsonPropertyName("variation")] string VariationName,
    [property: JsonPropertyName("image_url")] string ImageUrl,
    [property: JsonPropertyName("colors")] string[] Colors
);

public record class Villager(
    [property: JsonPropertyName("name")] string Name,
    [property: JsonPropertyName("nh_details")] Details Details
);

public record class Details(
    [property: JsonPropertyName("icon_url")] string IconUrl,
    [property: JsonPropertyName("fav_styles")] string[] Styles,
    [property: JsonPropertyName("fav_colors")] string[] Colors
);

