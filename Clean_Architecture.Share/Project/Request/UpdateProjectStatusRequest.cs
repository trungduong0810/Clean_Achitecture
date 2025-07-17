using Clean_Architecture.Domain.Enums;

namespace Clean_Architecture.Share.Project.Request
{
    public class UpdateProjectStatusRequest
    {
        public ProjectStatus Status { get; set; }
    }
}
