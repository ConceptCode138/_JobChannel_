using BLL.Interfaces;
using BO.DTO.DTORequests;
using BO.DTO.DTORequests.FiltersDTO;
using BO.DTO.DTOResponses;
using BO.Entities;
using BO.Exceptions;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Controllers
{
    /// <summary>
    /// 
    /// </summary>
    [ApiController]
    [Route("/api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class OffreController : ControllerBase
    {
        private readonly IOffreService _offreService;

        #region CONSTRUCTORS
        /// <summary>
        /// A la création de ' OffreController ', résolution des dépendances de OffreService. Si elle n'existe pas-> création de cell-ci car elle est de type singleton
        ///                                                                                   Ou récupération dans son contenant.
        /// </summary>
        /// <param name="offreService"></param>
        public OffreController(IOffreService offreService)
        {
            _offreService = offreService;
        }
        #endregion

        #region GET - LIST
        /// <summary>
        /// Allow to retrieve the list of offers without id except IdOffre
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetOffers([FromQuery] PageDTOWithFilters pagination)//, [FromQuery] int page)
        {
            //Calls on services                                                                                                             var offres = (await _offreService.GetAllOffresInCatalogAsync()).Skip((pagination.CurrentPage -1) * pagination.DataPerPage).Take(pagination.DataPerPage);                                                    

            var offers = (await _offreService.GetAllOffresInCatalogAsync(pagination)); // param- pagination.ToFilterOffer()

            // construction response business object -> DTO
            List<OfferWithoutIdDTOResponse> listOffers = new List<OfferWithoutIdDTOResponse>();

            //Add an offer of type 'OfferWithoutIdDTOResponse' in the list .
            foreach (var offer in offers)
            {
                listOffers.Add(new OfferWithoutIdDTOResponse(offer));
            }

            return Ok( listOffers);// ok(new PageDTOResponse( )
        }
        #endregion

        #region GET - ID
        /// <summary>
        /// Allow to research an offer in database by the id.
        /// </summary>
        /// <param name="idOffre"></param>
        /// <returns></returns>
        [HttpGet("{idOffre}")]
        public async Task<IActionResult> GetOfferPerID([FromRoute] int idOffre)
        {
            // research an offer in DB without TryCatch [ plus besoin de try catch, l'exception va être levée dans la BLL (OffreController), mais ici aussi filtrée par la classe ApiException...
            // qui elle va renvoyer un not found object result Avec l'exception message à l'intérieur ]

            // requete transfomée en objet métier DTO -> Objets métier

            //appels sur services
            var offerFind = await _offreService.GetOffreByIDAsync(idOffre);

            // construction de la réponse Objet métier -> DTO
            return Ok(new OfferDTOResponse(offerFind));
        }
        #endregion

        #region CREATE
        /// <summary>
        /// Allow to create a new offer in the DB via the request Data Transfert Object.
        /// </summary>
        /// <param name="offerRequest"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateOffer([FromBody] OffreDTORequest offerRequest)
        {
            // requete transfomée en objet métier DTO -> Objets métier
            var businessObject = offerRequest.ToOffre();

            //appels sur services
            var newOffer = await _offreService.CreateOffreAsync(businessObject);

            // construction de la réponse Objet métier -> DTO
            return Ok( new OfferDTOResponse(newOffer) );
        }
        #endregion

        #region UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer([FromRoute] int id,[FromBody] UpdateOffreDTORequest updateOfferDTORequest)
        {
            if (id != updateOfferDTORequest.IdOfferForUpdate)
            {
                //code 400
                return BadRequest();
            }
            else
            {
                // requete transfomée en objet métier DTO -> Objets métier
                var businessObject = updateOfferDTORequest.ToOffre();

                //appel sur service
                var offerRetrieved = await _offreService.UpdateOffreAsync(businessObject);

                return Ok(offerRetrieved);
            }
        }
        #endregion

        #region DELETE
        /// <summary>
        /// Allow to delete an offer in the DB via the request Data Transfert Object.
        /// </summary>
        /// <param name="idRequestDTO">Id of request Client part</param>
        /// <returns></returns>
        [HttpDelete("{idOffer}")]
        public async Task<IActionResult> DeleteOffer([FromRoute(Name = "idOffer")] DeleteOffreDTORequest idRequestDTO )
        {
            // requete transfomée en objet métier DTO -> Objets métier
            var idRequest = idRequestDTO.RequestDeleteOffre();

            //appels sur services
            await _offreService.DeleteOffreAsync( idRequest );

            //Response
            return NoContent();
        }
        #endregion





        //public int[] BuilderParameters(PaginationDTORequest pagination)
        //{
        //    var totalItemPerPage = pagination.TotalItem;
           
        //}

    }
}
