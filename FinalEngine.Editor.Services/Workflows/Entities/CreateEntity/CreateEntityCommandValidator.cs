namespace FinalEngine.Editor.Services.Workflows.Entities.CreateEntity
{
    using FluentValidation;

    public class CreateEntityCommandValidator : AbstractValidator<CreateEntityCommand>
    {
        public CreateEntityCommandValidator()
        {
            this.RuleFor(x => x.Identifier)
                .NotEmpty()
                .WithMessage("The identifier specified cannot be null, empty or consist of only whitespace characters.");

            this.RuleFor(x => x.Tag)
                .NotEmpty()
                .WithMessage("The tag specified cannot be null, empty or consist of only whitespace characters.");
        }
    }
}
