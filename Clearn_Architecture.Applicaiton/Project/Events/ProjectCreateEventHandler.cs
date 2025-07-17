using Clean_Architecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Applicaiton.Project.Events
{
    public class ProjectCreateEventHandler : INotificationHandler<ProjectCreatedEvent>
    {
        private readonly ILogger<ProjectCreateEventHandler> _logger;
        public ProjectCreateEventHandler(ILogger<ProjectCreateEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ProjectCreatedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Project Created: {ProjectName}", notification.Project.Name);
            Console.WriteLine($"Project Created: {notification.Project.Name}");
            return Task.CompletedTask;
        }
    }
}
