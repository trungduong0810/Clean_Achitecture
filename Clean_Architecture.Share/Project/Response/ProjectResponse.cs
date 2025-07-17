namespace Clean_Architecture.Share.Project.Response
{
    public class ProjectResponse
    {
        public int Id { get; set; }
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public string Status { get; set; }
        public DateTimeOffset? Created { get; set; }
    }
}
