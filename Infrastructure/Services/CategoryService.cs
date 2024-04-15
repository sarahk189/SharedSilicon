using Infrastructure.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;

namespace Infrastructure.Services;

public class CategoryService(HttpClient http, IConfiguration configuration)
{

	private readonly HttpClient _http = http;
	private readonly IConfiguration _configuration = configuration;


    public async Task<IEnumerable<CategoryDto>> GetCategoriesAsync()
    {
        var apiKey = _configuration["ApiKey:Secret"];
        var response = await _http.GetAsync($"{_configuration["ApiUris:categories"]}?key={apiKey}");
        if (response.IsSuccessStatusCode)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());
            return categories ??= null!;
        }

		Console.Error.WriteLine($"Failed to fetch categories, status code: {response.StatusCode}");
		return null!;
	}
}
