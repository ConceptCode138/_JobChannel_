using BO.DTO.DTORequests.FiltersDTO;
using BO.DTO.DTOResponses;
using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;


namespace BLL.Interfaces
{

    public interface IOffreService
    {
        #region MyRegion

        #endregion
        /// <summary>
        /// Allow to retrieve the list of offers
        /// </summary>
        /// <returns>reurn a collection</returns>
        Task<IEnumerable<Offre>> GetAllOffresInCatalogAsync(PageDTOWithFilters pageDTOWithFilters);

        /// <summary>
        /// Allow to retrieve an offer by him ID
        /// </summary>
        /// <param name="idOffre"></param>
        /// <returns>return an offer</returns>
        Task<Offre> GetOffreByIDAsync(int idOffre);

        /// <summary>
        /// Allow to create a new offer in database
        /// </summary>
        /// <param name="offre"></param>
        /// <returns>return the offer created</returns>
        Task<Offre> CreateOffreAsync(Offre offre);

        /// <summary>
        /// Allow to modify an offer selected by him ID
        /// </summary>
        /// <param name="offre"></param>
        /// <returns>return the offer updated</returns>
        Task<Offre> UpdateOffreAsync(Offre offre);

        /// <summary>
        /// Allow to delete an offer selected by him ID
        /// </summary>
        /// <param name="idOffre"></param>
        /// <returns>return void</returns>
        Task DeleteOffreAsync(int idOffre);

        /// <summary>
        /// Allow to retrieve the list of typeContrat in list offers.
        /// </summary>
        /// <returns></returns>
        Task<IEnumerable<TypeContrat>> GetAllTypeContratsInOfferCatalogAsync(); 



        
    }
}
