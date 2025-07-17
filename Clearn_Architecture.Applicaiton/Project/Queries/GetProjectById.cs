using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Share.ApiResponse;
using Clean_Architecture.Share.Project.Response;
using MediatR;

namespace Clean_Architecture.Applicaiton.Project.Queries
{
    public class GetProjectById
    {
        public record GetProjectByIdQuery(int Id) : IRequest<RESTfulAPIResponse<ProjectResponse>>;

        public class GetProjectByIdQueryHandler : IRequestHandler<GetProjectByIdQuery, RESTfulAPIResponse<ProjectResponse>>
        {
            private readonly IGenericRepository<Domain.Entities.Project> _repositoryGeneric; // Use generic repository (entyties.Project)
            public GetProjectByIdQueryHandler(IGenericRepository<Domain.Entities.Project> repositoryGeneric)
            {
                _repositoryGeneric = repositoryGeneric;
            }

            public async Task<RESTfulAPIResponse<ProjectResponse>> Handle(GetProjectByIdQuery request, CancellationToken cancellationToken)
            {
                var project = await _repositoryGeneric.GetByIdAsync(request.Id);

                if (project == null)
                    throw new KeyNotFoundException($"Project with ID {request.Id} not found.");

                var projectResponse = new ProjectResponse
                {
                    Id = project.Id,
                    Name = project.Name,
                    Description = project.Description,
                    Status = project.Status.ToString(),
                    Created = project.Created
                };

                return RESTfulAPIResponse<ProjectResponse>.SuccessResponse(
                    data: projectResponse,
                    message: "Lấy thông tin dự án thành công."
                );
            }
        }
    }
}
