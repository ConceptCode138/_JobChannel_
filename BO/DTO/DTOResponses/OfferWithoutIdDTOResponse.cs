using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTOResponses
{
    /// <summary>
    /// This class Allow to give a response without id
    /// </summary>
    public class OfferWithoutIdDTOResponse
    {
        #region PROPERTIES

        /// <summary>
        /// Id de l'offreDTO
        /// </summary>
        public int Id { get; set; }

        /// <summary>
        /// Titre de l'offreDTO
        /// </summary>
        public string TitreResponse { get; set; }

        /// <summary>
        /// Lien url de l'offreDTO
        /// </summary>
        public string LienResponse { get; set; }

        /// <summary>
        /// Description de l'offreDTO
        /// </summary>
        public string DescriptionResponse { get; set; }

        /// <summary>
        /// Date de publication de l'offreDTO
        /// </summary>
        public DateTime DateCreationResponse { get; set; }

        /// <summary>
        /// Type de poste de l'offreDTO (Développeur)
        /// </summary>
        public string PosteResponse { get; set; }

        /// <summary>
        /// Localisation de l'offreDTO correspondant à la region
        /// </summary>
        public string LieuResponse { get; set; }

        /// <summary>
        /// Type de contrat de l'offreDTO (CDD,CDI, Contrat de professionnalisation)
        /// </summary>
        public string ContratResponse { get; set; }
        #endregion


        #region CONSTRUCTORS

        /// <summary>
        /// Default constructor
        /// </summary>
        public OfferWithoutIdDTOResponse() { }

        /// <summary>
        /// CONSTRUCTOR without ID
        /// </summary>
        /// <param name="offre">Entity offre</param>
        public OfferWithoutIdDTOResponse(Offre offre)
        {
            Id                   = offre.IdOffre;
            TitreResponse        = offre.Title;
            LienResponse         = offre.Link;
            DescriptionResponse  = offre.Description;
            DateCreationResponse = offre.DtCreated;
            PosteResponse        = offre.Poste.IntitulePoste;
            LieuResponse         = offre.Localisation.IntituleRegion;
            ContratResponse      = offre.Contrat.IntituleContrat;
        }

        #endregion  

    }
}
