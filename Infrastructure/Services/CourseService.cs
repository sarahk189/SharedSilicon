using Infrastructure.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace Infrastructure.Services;

public class CourseService(HttpClient http, IConfiguration configuration)
{

	private readonly HttpClient _http = http;
	private readonly IConfiguration _configuration = configuration;


    public async Task<CourseResult> GetCoursesAsync(string category = "", string searchQuery = "", int pageNumber = 1, int pageSize = 6)
    {
        var apiKey = _configuration["ApiKey:Secret"];
        var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}?category={Uri.UnescapeDataString(category)}&searchQuery={Uri.UnescapeDataString(searchQuery)}&pageNumber={pageNumber}&pageSize{pageSize}&key={apiKey}");
        if (response.IsSuccessStatusCode)
        {
            var result = JsonConvert.DeserializeObject<CourseResult>(await response.Content.ReadAsStringAsync());
            if (result != null && result.Succeeded)
                return result;
        }

        return null!;
    }

    public async Task<CourseDto> GetCourseAsync(int id)
    {
        var apiKey = _configuration["ApiKey:Secret"];
        var response = await _http.GetAsync($"{_configuration["ApiUris:Courses"]}/{id}?key={apiKey}");
        if (response.IsSuccessStatusCode)
        {
            var course = JsonConvert.DeserializeObject<CourseDto>(await response.Content.ReadAsStringAsync());
            if (course != null)
            {
                return course;
            }
        }

        throw new Exception("Course not found");
    }

}
