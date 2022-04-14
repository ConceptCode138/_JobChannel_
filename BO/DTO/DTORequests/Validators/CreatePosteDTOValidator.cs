using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests.Validators
{
    class CreatePosteDTOValidator : AbstractValidator<CreateTypePosteDTORequest>
    {
        public CreatePosteDTOValidator()
        {
            RuleFor(poste => poste.PosteType).NotEmpty()
                                            .WithMessage("Specify the field.")
                                            .Length(3, 255)
                                            .WithMessage("The field doesn't respect size length!");
        }
    }
}
