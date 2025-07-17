using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Entities;

namespace Clean_Architecture.Domain.Events
{
    public class ProjectCreatedEvent : BaseEvent
    {
        public ProjectCreatedEvent(Project project)
        {
            Project = project;
        }
        public Project Project { get; }
    }
}
