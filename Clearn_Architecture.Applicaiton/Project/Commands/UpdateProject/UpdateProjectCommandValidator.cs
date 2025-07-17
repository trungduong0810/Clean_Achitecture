using Clean_Architecture.Applicaiton.Project.Validators;
using FluentValidation;
using static Clean_Architecture.Applicaiton.Project.Commands.UpdateProject.UpdateProject;

namespace Clean_Architecture.Applicaiton.Project.Commands.UpdateProject
{
    public class UpdateProjectCommandValidator : AbstractValidator<UpdateProjectCommand>
    {
        public UpdateProjectCommandValidator()
        {
            RuleFor(x => x.Project).SetValidator(new ProjectRequestValidator());
        }
    }
}
