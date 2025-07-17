using Clean_Architecture.Applicaiton.Project.Validators;
using FluentValidation;

namespace Clean_Architecture.Applicaiton.Project.Commands.CreateProject
{
    public class CreateProjectCommandValidator : AbstractValidator<CreateProjectCommand>
    {
        public CreateProjectCommandValidator()
        {
            RuleFor(x => x.Project).SetValidator(new ProjectRequestValidator());
        }
    }
}
