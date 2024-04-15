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
        var response = await _http.GetAsync(_configuration["ApiUris:categories"]);
        if (response.IsSuccessStatusCode)
        {
            var categories = JsonConvert.DeserializeObject<IEnumerable<CategoryDto>>(await response.Content.ReadAsStringAsync());
            return categories ??= null!;
        }

        return null!;
    }

}
