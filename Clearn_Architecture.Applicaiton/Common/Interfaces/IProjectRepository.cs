using Clean_Architecture.Domain.Enums;

namespace Clean_Architecture.Applicaiton.Common.Interfaces
{
    public interface IProjectRepository
    {
        Task<bool> UpdateStatusAsync(int id, ProjectStatus status);
    }
}
