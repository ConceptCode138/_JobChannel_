using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTOResponses
{
    /// <summary>
    /// This class allow to give a response with id
    /// </summary>
    public class OfferDTOResponse
    {
        #region PROPERTIES

        /// <summary>
        /// Id de l'offreDTO
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titre de l'offreDTO
        /// </summary>
        public string Titre { get; set; }

        /// <summary>
        /// Lien url de l'offreDTO
        /// </summary>
        public string Lien { get; set; }

        /// <summary>
        /// Description de l'offreDTO
        /// </summary>
        public string Description { get; set; }

        /// <summary>
        /// Date de publication de l'offreDTO
        /// </summary>
        public DateTime DateCreation { get; set; }

        /// <summary>
        /// Type de poste de l'offreDTO (Développeur)
        /// </summary>
        public TypePoste Poste { get; set; }

        /// <summary>
        /// Localisation de l'offreDTO correspondant à la region
        /// </summary>
        public Region Lieu { get; set; }

        /// <summary>
        /// Type de contrat de l'offreDTO (CDD,CDI, Contrat de professionnalisation)
        /// </summary>
        public TypeContrat Contrat { get; set; }

        #endregion


        #region CONSTRUCTORS

        public OfferDTOResponse() { }

        public OfferDTOResponse(Offre offre)
        {
            Id           = offre.IdOffre;
            Titre        = offre.Title;
            Lien         = offre.Link;
            Description  = offre.Description;
            DateCreation = offre.DtCreated;
            Poste        = offre.Poste;
            Lieu         = offre.Localisation;
            Contrat      = offre.Contrat;
        }

        #endregion
    }
}
