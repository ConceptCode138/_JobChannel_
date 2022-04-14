using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests.Validators
{
    /// <summary>
    /// This class allow to validate the region about an offer
    /// </summary>
    class CreateRegionDTOValidator : AbstractValidator<CreateNewRegionDTORequest>
    {
        /// <summary>
        /// This constructor allow to validate the region derived from request Data transfert Object
        /// </summary>
        public CreateRegionDTOValidator()
        {
            RuleFor(region => region.RegionName).NotEmpty()
                                                .WithMessage("Specify the field")
                                                .Length(3, 255)
                                                .WithMessage("The field doesn't respest the constraint");
        }
    }
}
