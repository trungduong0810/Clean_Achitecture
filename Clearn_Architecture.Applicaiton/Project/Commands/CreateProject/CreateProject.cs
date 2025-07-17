using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Project.Request;
using MediatR;

namespace Clean_Architecture.Applicaiton.Project.Commands.CreateProject
{
    public record CreateProjectCommand(ProjectRequest Project) : IRequest<RESTfulAPIResponse<object>>;

    public class CreateProjectCommandHandler : IRequestHandler<CreateProjectCommand, RESTfulAPIResponse<object>>
    {
        private readonly IApplicationDbContext _context;

        private readonly IGenericRepository<Domain.Entities.Project> _repositoryGeneric; // Use generic repository (entyties.Project)
        public CreateProjectCommandHandler(IApplicationDbContext context, IGenericRepository<Domain.Entities.Project> repositoryGeneric)
        {
            _repositoryGeneric = repositoryGeneric;
            _context = context;
        }

        public async Task<RESTfulAPIResponse<object>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {

            if (await _repositoryGeneric.AnyAsync(x => x.Name.ToLower() == request.Project.Name.ToLower()))
                return RESTfulAPIResponse<object>.FailResponse($"A project with the name '{request.Project.Name}' already exists.");

            // domain driven design
            var entity = Domain.Entities.Project.Create(request.Project.Name, request.Project.Description);

            await _repositoryGeneric.AddAsync(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return RESTfulAPIResponse<object>.SuccessResponse(
                data: new { ProjectId = entity.Id },
                message: "Project created successfully"
            );
        }
    }
}