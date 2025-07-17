using Clean_Architecture.Applicaiton.Common.Interfaces;
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

            private readonly IGenericRepository<Domain.Entities.Project> _repositoryGeneric; // Use generic repository (entyties.Project)

            public DeleteProjectCommandHandler(IApplicationDbContext context, IGenericRepository<Domain.Entities.Project> repositoryGeneric)
            {
                _context = context;
                _repositoryGeneric = repositoryGeneric;
            }

            public async Task<RESTfulAPIResponse<bool>> Handle(DeleteProjectCommand request, CancellationToken cancellationToken)
            {
                var project = await _repositoryGeneric.ExistsAsync(request.Id);

                if (!project)
                    throw new KeyNotFoundException($"Project with ID {request.Id} not found.");

                await _repositoryGeneric.DeleteByIdAsync(request.Id);

                await _context.SaveChangesAsync(cancellationToken);

                return RESTfulAPIResponse<bool>.SuccessResponse(
                    data: true,
                    message: "Project deleted successfully"
                );
            }
        }
    }
}
