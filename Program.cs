var builder = WebApplication.CreateBuilder(args);
var nookiApiKey = builder.Configuration["Nookipedia:ApiKey"];

// Add services to the container.
builder.Services.AddRazorPages();
builder.Services.AddHttpClient("Nookipedia", httpClient => {
    httpClient.BaseAddress = new Uri("https://api.nookipedia.com");
    httpClient.DefaultRequestHeaders.Add("X-API-KEY", nookiApiKey);
    httpClient.DefaultRequestHeaders.Add("Accept-Version", "1.5.0");
});
var app = builder.Build();


// Configure the HTTP request pipeline.
if (!app.Environment.IsDevelopment())
{
    app.UseExceptionHandler("/Error");
    // The default HSTS value is 30 days. You may want to change this for production scenarios, see https://aka.ms/aspnetcore-hsts.
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();

app.UseRouting();

app.UseAuthorization();

app.MapRazorPages();

app.Run();
