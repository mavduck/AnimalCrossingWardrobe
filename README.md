
# Animal Crossing Wardrobe

Animal Crossing Wardrobe allows you to select a Villager, filter all the New Horizons clothing by category, color, and style and view the satisfaction that your Villager has with each of them. 





## Project Status

In the future I hope to add an ability to login and save Villagers and create little "Wardrobes" for them where you can add Clothing that you would like to gift them on your island in hopes that they wear. Which is more likely if they like it!

For now, you can filter clothing based on what you're looking for and select a Villager to see how much they like it.  
## Run Locally

You will need to download [.NET](https://dotnet.microsoft.com/en-us/download) if you don't have it, I use .NET 6.0

Clone the project, cd to it

```bash
  git clone https://github.com/mavduck/AnimalCrossingWardrobe.git

  cd AnimalCrossingWardrobe

```
AnimalCrossingWardrobe makes use of private [NookipediaApi](https://api.nookipedia.com/?pk_campaign=legacy-api-page).
You will need to apply for an API key using the link and either edit Program.cs directly when it uses the API key or 

Use secret storage:
```bash
dotnet user-secrets init
dotnet user-secrets set "Nookipedia:ApiKey" "{APIKEY}"
```


Start the server

```bash
  dotnet run
```


## Acknowledgements

 - Big thanks to [Nookipedia API](https://api.nookipedia.com/?pk_campaign=legacy-api-page)



## Authors

- [@mavduck](https://github.com/mavduck)

