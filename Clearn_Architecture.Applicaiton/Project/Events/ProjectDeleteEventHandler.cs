using Clean_Architecture.Domain.Events;
using MediatR;
using Microsoft.Extensions.Logging;

namespace Clean_Architecture.Applicaiton.Project.Events
{
    public class ProjectDeleteEventHandler : INotificationHandler<ProjectDeletedEvent>
    {
        private readonly ILogger<ProjectDeleteEventHandler> _logger;
        public ProjectDeleteEventHandler(ILogger<ProjectDeleteEventHandler> logger)
        {
            _logger = logger;
        }
        public Task Handle(ProjectDeletedEvent notification, CancellationToken cancellationToken)
        {
            _logger.LogInformation("Project deleted: {ProjectName}", notification.Project.Name);
            Console.WriteLine($"Project deleted: {notification.Project.Name}");
            return Task.CompletedTask;
        }
    }
}
