using Clean_Architecture.Share.Project.Request;
using FluentValidation;

namespace Clean_Architecture.Applicaiton.Project.Validators
{
    public class ProjectRequestValidator : AbstractValidator<ProjectRequest>
    {
        public ProjectRequestValidator()
        {
            RuleFor(x => x.Name)
                .NotEmpty().WithMessage("Tên dự án không được để trống.")
                .MaximumLength(500).WithMessage("Tên dự án tối đa 500 ký tự.");
        }
    }
}
