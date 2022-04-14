using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests
{
    /// <summary>
    /// correspond à la Requete provenant du client qui sera désérialiser d'un objet JSON en objet C#.
    /// </summary>
    public class OffreDTORequest
    {
        #region PROPERTIES

        /// <summary>
        /// Titre de l'offreDTORequest
        /// </summary>
        public string TitreOffre { get; set; }

        /// <summary>
        /// Lien de l'offreDTORequest
        /// </summary>
        public string LienOffre { get; set; }

        /// <summary>
        /// Description de l'offreDTORequest
        /// </summary>
        public string DescriptionOffre { get; set; }

        /// <summary>
        /// Date de publication de l'offreDTORequest
        /// </summary>
        public DateTime DatePublication { get; set; }

        /// <summary>
        /// Type de poste de l'offreDTORequest 
        /// </summary>
        public TypePoste PosteOffre { get; set; }           //  ( à tester en cas de non existance )

        /// <summary>
        /// Type de contrat de l'offreDTORequest
        /// </summary>
        public TypeContrat ContratOffre { get; set; }

        /// <summary>
        /// Region de l'offreDTORequest
        /// </summary>
        public Region RegionOffre { get; set; }

        #endregion



        /// <summary>
        /// This method allow to create a new instance "Offre".
        /// </summary>
        /// <returns></returns>
        public Offre ToOffre()
        {
            return new Offre()
            {
                Title        = TitreOffre      ,
                Link         = LienOffre       ,
                Description  = DescriptionOffre,
                DtCreated    = DatePublication ,
                Poste        = PosteOffre      ,
                Contrat      = ContratOffre    ,
                Localisation = RegionOffre
            };
        }
    }
}
