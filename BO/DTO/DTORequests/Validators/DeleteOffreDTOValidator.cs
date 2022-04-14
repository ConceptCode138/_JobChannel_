using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests.Validators
{
    public class DeleteOffreDTOValidator : AbstractValidator<DeleteOffreDTORequest>
    {
        public DeleteOffreDTOValidator()
        {
            RuleFor(offreForDelete => offreForDelete.IdOffreForDelete).NotEmpty().WithMessage("Specify an offer to delete, please!");

            RuleFor(offreForDelete => offreForDelete.IdOffreForDelete).GreaterThan(0).WithMessage("No negative value accepted");
        }
    }
}
