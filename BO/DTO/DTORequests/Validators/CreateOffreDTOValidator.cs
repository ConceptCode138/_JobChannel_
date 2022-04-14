using FluentValidation;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests.Validators
{
    public class CreateOffreDTOValidator : AbstractValidator<OffreDTORequest>
    {
        public CreateOffreDTOValidator()
        {
            RuleFor(offre => offre.LienOffre).MaximumLength(255)
                                             .WithMessage("[Url] doesn't contain more letter than 255 char");

            RuleFor(offre => offre.DescriptionOffre).NotEmpty()
                                                    .WithMessage("Specify the field [Description]");

            RuleFor(offre => offre.DescriptionOffre).Length(50, 2000)
                                                    .WithMessage("[Description] doesn't contain more letter than two thousand char AND less letter than fifty");

            RuleFor(offre => offre.TitreOffre).NotEmpty()
                                              .WithMessage("Specify the field [Titre]");
            RuleFor(offre => offre.TitreOffre).MinimumLength(3)
                                              .WithMessage("[Titre] doesn't contain more letter than one hundred and fifty char AND less letter than three");

            RuleFor(offre => offre.PosteOffre).NotEmpty()
                                              .WithMessage("Specify the field [Poste]");

            RuleFor(offre => offre.ContratOffre).NotEmpty()
                                  .WithMessage("Specify the field [Contrat]");

            RuleFor(offre => offre.RegionOffre).NotEmpty()
                                  .WithMessage("Specify the field [Region]");

        }
    }
}
