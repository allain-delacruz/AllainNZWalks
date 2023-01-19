using FluentValidation;

namespace AllainNZWalks.Validators
{
    public class AddUpdateWalkDifficultyRequestValidator : AbstractValidator<Models.DTO.AddUpdateWalkDifficultyRequest>
    {
        public AddUpdateWalkDifficultyRequestValidator()
        {
            RuleFor(x => x.Code).NotEmpty();
        }
    }
}
