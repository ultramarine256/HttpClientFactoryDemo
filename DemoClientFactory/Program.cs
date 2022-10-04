using Microsoft.Net.Http.Headers;

var builder = WebApplication.CreateBuilder(args);

// Add services to the container.
builder.Services.AddControllers();
builder.Services.AddHttpClient("BurgerApi", httpClient =>
{
    // using Microsoft.Net.Http.Headers;
    httpClient.BaseAddress = new Uri("https://burgers1.p.rapidapi.com");
    httpClient.DefaultRequestHeaders.Add("X-RapidAPI-Key", "37c9993b09mshc41dc4f6dc793fap181029jsnf7fa2596915f");
});

var app = builder.Build();

// Configure the HTTP request pipeline.
app.UseHttpsRedirection();
app.UseAuthorization();
app.MapControllers();

app.Run();
