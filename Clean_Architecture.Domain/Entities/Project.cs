using Clean_Architecture.Domain.Common;
using Clean_Architecture.Domain.Enums;
using Clean_Architecture.Domain.Events;

namespace Clean_Architecture.Domain.Entities
{
    public class Project : BaseAuditableEntity
    {
        public string Name { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public ProjectStatus Status { get; set; }

        private Project()
        {
        }

        public static Project Create(string name, string? description)
        {
            var project = new Project
            {
                Name = name,
                Description = description ?? string.Empty,
                Created = DateTime.UtcNow,
                Status = ProjectStatus.NotStarted
            };

            project.AddDomainEvent(new ProjectCreatedEvent(project));

            return project;
        }
    }
}
