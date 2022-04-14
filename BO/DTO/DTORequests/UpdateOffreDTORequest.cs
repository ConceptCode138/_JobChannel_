using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests
{
    public class UpdateOffreDTORequest
    {
        #region PROPERTIES

        public int IdOfferForUpdate { get; set; }

        /// <summary>
        /// Titre de l'UpdateOffreDTORequest
        /// </summary>
        public string TitleOfferForUpdate { get; set; }

        /// <summary>
        /// Lien de l'UpdateOffreDTORequest
        /// </summary>
        public string LinkOfferForUpdate { get; set; }

        /// <summary>
        /// Description de l'UpdateOffreDTORequest
        /// </summary>
        public string DescriptionOfferForUpdate { get; set; }

        /// <summary>
        /// Date de publication de l'UpdateOffreDTORequest
        /// </summary>
        //public DateTime DatePublication { get; set; }

        /// <summary>
        /// Type de poste de l'UpdateOffreDTORequest 
        /// </summary>
        public int IdPosteOfferForUpdate { get; set; }           //  ( à tester en cas de non existance )

        /// <summary>
        /// Type de contrat de l'UpdateOffreDTORequest
        /// </summary>
        public int IdContratOfferForUpdate { get; set; }

        /// <summary>
        /// Region de l'UpdateOffreDTORequest
        /// </summary>
        public int IdRegionOfferForUpdate { get; set; }
        #endregion


        /// <summary>
        /// This method allow to create a new instance "Offre".
        /// </summary>
        /// <returns></returns>
        public Offre ToOffre()
        {
            return new Offre()
            {
                IdOffre       = IdOfferForUpdate         ,
                Title         = TitleOfferForUpdate      ,
                Link          = LinkOfferForUpdate       ,
                Description   = DescriptionOfferForUpdate,
                IdTypePoste   = IdPosteOfferForUpdate    ,
                IdTypeContrat = IdContratOfferForUpdate  ,
                IdRegionName  = IdRegionOfferForUpdate
            };
        }
    }
}
