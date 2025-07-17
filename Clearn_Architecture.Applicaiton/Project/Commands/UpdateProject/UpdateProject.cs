using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Project.Request;
using MediatR;
using Microsoft.EntityFrameworkCore;

namespace Clean_Architecture.Applicaiton.Project.Commands.UpdateProject
{
    public class UpdateProject
    {
        public record UpdateProjectCommand(int Id, ProjectRequest Project) : IRequest<RESTfulAPIResponse<bool>>;

        public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, RESTfulAPIResponse<bool>>
        {
            private readonly IApplicationDbContext _context;

            public UpdateProjectCommandHandler(IApplicationDbContext context)
            {
                _context = context;
            }

            public async Task<RESTfulAPIResponse<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _context.Projects
                    .FirstOrDefaultAsync(p => p.Id == request.Id, cancellationToken);

                if (project == null)
                    throw new KeyNotFoundException($"Project with ID {request.Id} not found.");

                project.Name = request.Project.Name;
                project.Description = request.Project.Description ?? string.Empty;

                await _context.SaveChangesAsync(cancellationToken);

                return RESTfulAPIResponse<bool>.SuccessResponse(
                    data: true,
                    message: "Project updated successfully"
                );
            }
        }
    }
}
