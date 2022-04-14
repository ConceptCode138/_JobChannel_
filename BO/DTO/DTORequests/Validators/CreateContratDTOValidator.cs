using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests.Validators
{
    /// <summary>
    /// This class allow to validate the type of contract about an offer
    /// </summary>
    class CreateContratDTOValidator : AbstractValidator<CreateTypeContratDTORequest>
    {
        /// <summary>
        /// This constructor allow to validate the type of contract derived from request Data transfert Object
        /// </summary>
        public CreateContratDTOValidator()
        {
            RuleFor(typeContract => typeContract.ContratType).NotEmpty()
                                                           .WithMessage("Please specify a type of contract")
                                                           .MaximumLength(3)
                                                           .MaximumLength(255);
        }
    }
}
