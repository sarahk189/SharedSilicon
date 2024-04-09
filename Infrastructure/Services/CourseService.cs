using Infrastructure.Dtos;
using Microsoft.Extensions.Configuration;
using Newtonsoft.Json;


namespace Infrastructure.Services;

public class CourseService(HttpClient http, IConfiguration configuration)
{

	private readonly HttpClient _http = http;
	private readonly IConfiguration _configuration = configuration;


	public async Task<IEnumerable<CourseDto>> GetCoursesAsync()
	{
		var response = await _http.GetAsync(_configuration["ApiUris:Courses"]);
		if (response.IsSuccessStatusCode)
		{
			var courses = JsonConvert.DeserializeObject<IEnumerable<CourseDto>>(await response.Content.ReadAsStringAsync());
			return courses ??= null!;
		}

		return null!;
	}
}
