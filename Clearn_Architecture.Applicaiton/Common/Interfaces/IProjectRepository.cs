namespace Clean_Architecture.Applicaiton.Common.Interfaces
{
    // generic repository IRepotitory<T>
    public interface IProjectRepository
    {
        Task<Domain.Entities.Project?> GetProjectByIdAsync(int id);
    }
}
