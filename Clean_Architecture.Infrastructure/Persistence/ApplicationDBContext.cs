using Clean_Architecture.Applicaiton.Common.Interfaces;
using Clean_Architecture.Domain.Entities;
using Clean_Architecture.Infrastructure.Persistence.EntityConfigurations.Models;
using MediatR;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Diagnostics;

namespace Clean_Architecture.Infrastructure.Persistence
{
    public class ApplicationDBContext : IdentityDbContext<ApplicationUser, ApplicationRole, Guid>, IApplicationDbContext
    {
        private readonly IMediator _mediator;

        private readonly IEnumerable<ISaveChangesInterceptor> _interceptors;
        public DbSet<ApplicationUser> ApplicationUsers { get; set; }
        public DbSet<ApplicationRole> ApplicationRoles { get; set; }
        public DbSet<Project> Projects { get; set; }


        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options, IMediator mediator, IEnumerable<ISaveChangesInterceptor> interceptors)
            : base(options)
        {
            _mediator = mediator;
            _interceptors = interceptors;

        }

        public override async Task<int> SaveChangesAsync(CancellationToken cancellationToken = default)
        {
            return await base.SaveChangesAsync(cancellationToken);
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            foreach (var interceptor in _interceptors)
            {
                optionsBuilder.AddInterceptors(interceptor);
            }
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
        }
    }
}
