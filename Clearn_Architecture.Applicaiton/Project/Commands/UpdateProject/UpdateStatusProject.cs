using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Enums;
using Clean_Architecture.Share.ApiResponse;
using MediatR;

namespace Clean_Architecture.Applicaiton.Project.Commands.UpdateProject
{
    public class UpdateStatusProject
    {
        public record UpdateStatusProjectCommand(int Id, ProjectStatus Status) : IRequest<RESTfulAPIResponse<bool>>;

        public class UpdateStatusProjectCommandHandler : IRequestHandler<UpdateStatusProjectCommand, RESTfulAPIResponse<bool>>
        {
            private readonly IProjectRepository _projectRepository; // Use respository project handler for update status

            public UpdateStatusProjectCommandHandler(IProjectRepository projectRepository)
            {
                _projectRepository = projectRepository;
            }

            public async Task<RESTfulAPIResponse<bool>> Handle(UpdateStatusProjectCommand request, CancellationToken cancellationToken)
            {
                var updated = await _projectRepository.UpdateStatusAsync(request.Id, request.Status);

                if (!updated)
                {
                    return RESTfulAPIResponse<bool>.FailResponse(
                        message: $"Project with ID {request.Id} not found or update failed."
                    );
                }

                return RESTfulAPIResponse<bool>.SuccessResponse(
                    data: true,
                    message: "Project status updated successfully."
                );
            }
        }
    }
}
