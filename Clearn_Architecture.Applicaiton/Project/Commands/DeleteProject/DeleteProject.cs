using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Events;
using Clean_Architecture.Share.ApiResponse;
using MediatR;

namespace Clean_Architecture.Applicaiton.Project.Commands.DeleteProject
{
    public class DeleteProject
    {
        public record DeleteProjectCommand(int Id) : IRequest<RESTfulAPIResponse<bool>>;

        public class DeleteProjectCommandHandler : IRequestHandler<DeleteProjectCommand, RESTfulAPIResponse<bool>>
        {
            private readonly IApplicationDbContext _context;

            public DeleteProjectCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<RESTfulAPIResponse<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects.FindAsync(new object[] { request.Id }, cancellationToken);

                if (project == null)
                    throw new KeyNotFoundException($"Project with ID {request.Id} not found.");

                project.AddDomainEvent(new ProjectDeletedEvent(project));

                _context.Projects.Remove(project);

                await _context.SaveChangesAsync(cancellationToken);

                return RESTfulAPIResponse<bool>.SuccessResponse(
                    data: true,
                    message: "Project deleted successfully"
                );
            }
        }
    }
}
