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
        var url = $"{_configuration["ApiUris:Categories"]}?key={_configuration["ApiKey:Secret"]}";
        Console.WriteLine($"URL: {url}");

        var response = await _http.GetAsync(url);
        if (response.IsSuccessStatusCode)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());
            return categories ??= null!;
        }
        else
        {
            
            Console.WriteLine($"Response status code: {response.StatusCode}");
            Console.WriteLine($"Response content: {await response.Content.ReadAsStringAsync()}");
            Console.WriteLine("Hey! IM HERE!!!!!!");
        }

		Console.Error.WriteLine($"Failed to fetch categories, status code: {response.StatusCode}");
		return null!;
	}
}
