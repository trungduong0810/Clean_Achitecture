using Microsoft.EntityFrameworkCore;

namespace Clean_Architecture.Applicaiton.Common.Interfaces
{
    public interface IApplicationDbContext
    {
        DbSet<Domain.Entities.Project> Projects { get; }

        Task<int> SaveChangesAsync(CancellationToken cancellationToken);
    }
}
