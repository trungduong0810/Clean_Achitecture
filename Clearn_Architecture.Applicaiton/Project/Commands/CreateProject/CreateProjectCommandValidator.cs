using Clean_Architecture.Applicaiton.Common.Interfaces;
using FluentValidation;
using Microsoft.EntityFrameworkCore;

namespace Clean_Architecture.Applicaiton.Project.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator(IApplicationDbContext context)
        {
            RuleFor(x => x.Project.Name)
                .NotEmpty().WithMessage("Tên dự án không được để trống.")
                .MaximumLength(500).WithMessage("Tên dự án tối đa 500 ký tự.")
                .MustAsync(async (name, cancellation) =>
                 {
                     var exists = await context.Projects.AnyAsync(p => p.Name == name, cancellationToken: cancellation);
                     return !exists;
                 }).WithMessage("Tên dự án đã tồn tại.");
        }
    }
}
