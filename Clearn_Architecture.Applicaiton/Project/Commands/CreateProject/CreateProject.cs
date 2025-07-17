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

        public CreateProjectCommandHandler(IApplicationDbContext context)
        {
            _context = context;
        }

        public async Task<RESTfulAPIResponse<object>> Handle(CreateProjectCommand request, CancellationToken cancellationToken)
        {
            // domain driven design

            var entity = Domain.Entities.Project.Create(request.Project.Name, null);

            _context.Projects.Add(entity);

            await _context.SaveChangesAsync(cancellationToken);

            return RESTfulAPIResponse<object>.SuccessResponse(
                data: new { ProjectId = entity.Id },
                message: "Project created successfully"
            );
        }
    }
}