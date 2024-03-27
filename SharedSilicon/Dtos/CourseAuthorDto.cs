namespace SharedSilicon.Dtos
{
    public class CourseAuthorDto
    {
        public string AuthorImageUrl { get; set; } = null!;
        public string FirstName { get; set; } = null!;
        public string LastName { get; set; } = null!;
        public string? Headline { get; set; }
        public string? Description { get; set; }
        public int? NumberOfSubscribers { get; set; }
        public int? NumberOfFollowers { get; set; }
    }
}
