using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Domain.Enums;
using Clean_Architecture.Infrastructure.Persistence;

namespace Clean_Architecture.Infrastructure.Repositories
{
    public class ProjectRepository : GenericRepository<Project>, IProjectRepository
    {

        public ProjectRepository(ApplicationDBContext context) : base(context) { }

        // Update status of a project
        public async Task<bool> UpdateStatusAsync(int id, ProjectStatus status)
        {
            var project = await _dbSet.FindAsync(id);

            if (project is null)
                return false;

            project.Status = status;
            await _context.SaveChangesAsync();

            return true;
        }
    }
}
