using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Project.Request;
using MediatR;

namespace Clean_Architecture.Applicaiton.Project.Commands.UpdateProject
{
    public class UpdateProject
    {
        public record UpdateProjectCommand(int Id, ProjectRequest Project) : IRequest<RESTfulAPIResponse<bool>>;

        public class UpdateProjectCommandHandler : IRequestHandler<UpdateProjectCommand, RESTfulAPIResponse<bool>>
        {
            private readonly IApplicationDbContext _context;
            private readonly IGenericRepository<Domain.Entities.Project> _repositoryGeneric; // Use generic repository (entyties.Project)
            public UpdateProjectCommandHandler(IApplicationDbContext context, IGenericRepository<Domain.Entities.Project> repositoryGeneric)
            {
                _context = context;
                _repositoryGeneric = repositoryGeneric;
            }

            public async Task<RESTfulAPIResponse<bool>> Handle(UpdateProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _repositoryGeneric.GetByIdAsync(request.Id);

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
