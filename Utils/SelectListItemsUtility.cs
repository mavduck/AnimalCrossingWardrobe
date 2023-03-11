using Microsoft.AspNetCore.Mvc.Rendering;

public static class SelectListItemsUtility {
        public static List<SelectListItem> Categories {get;} = new List<SelectListItem>
        {
            new SelectListItem { Value = "Tops", Text = "Tops" },
            new SelectListItem { Value = "Bottoms", Text = "Bottoms" },
            new SelectListItem { Value = "Dress-up", Text = "Dresses/Jumpsuits"  },
            new SelectListItem { Value = "Headwear", Text = "Headwear"  },
            new SelectListItem { Value = "Accessories", Text = "Accessories"  },
            new SelectListItem { Value = "Socks", Text = "Socks"  },
            new SelectListItem { Value = "Shoes", Text = "Shoes"  },            
            new SelectListItem { Value = "Bags", Text = "Bags"  },         
            new SelectListItem { Value = "Umbrellas", Text = "Umbrellas"  },
        };

        public static List<SelectListItem> Colors { get; } = new List<SelectListItem>
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
            public static List<SelectListItem> Styles { get; } = new List<SelectListItem>
        {
            new SelectListItem { Value = "Active", Text = "Active" },
            new SelectListItem { Value = "Cool", Text = "Cool" },
            new SelectListItem { Value = "Cute", Text = "Cute"  },
            new SelectListItem { Value = "Elegant", Text = "Elegant"  },
            new SelectListItem { Value = "Gorgeous", Text = "Gorgeous"  },
            new SelectListItem { Value = "Simple", Text = "Simple"  },
        };
}