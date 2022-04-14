using BLL.Interfaces;
using BO.Entities;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mime;
using System.Threading.Tasks;

namespace API.Controllers
{

    [ApiController]
    [Route("/api/[controller]")]
    [Produces(MediaTypeNames.Application.Json)]
    public class TypePosteController : ControllerBase
    {

        private readonly IPosteService _posteService;

        #region CONSTRUCTORS
        /// <summary>
        /// A la création de ' OffreController ', résolution des dépendances de OffreService. Si elle n'existe pas-> création de cell-ci car elle est de type singleton
        ///                                                                                   Ou récupération dans son contenant.
        /// </summary>
        /// <param name="posteService"></param>
        public TypePosteController(IPosteService posteService)
        {
            _posteService = posteService;
        }
        #endregion

        #region GET - LIST
        /// <summary>
        /// Allow to retrieve the list of offers without id except IdOffre
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetJobs()
        {
            //appels sur services
            var postes = await _posteService.GetAllPostesAsync();

            // construction de la réponse Objet métier -> DTO
            List<TypePoste> listDePoste = new List<TypePoste>();

            foreach (var poste in postes)
            {
                listDePoste.Add(poste);
            }

            return Ok(listDePoste);
        }
        #endregion

        #region GET - ID
        /// <summary>
        /// Allow to research an offer in database by the id.
        /// </summary>
        /// <param name="idPoste"></param>
        /// <returns></returns>
        [HttpGet("{idPoste}")]
        public async Task<IActionResult> GetOfferPerID([FromRoute] int idPoste)
        {
            // research an offer in DB without TryCatch [ plus besoin de try catch, l'exception va être levée dans la BLL (OffreController), mais ici aussi filtrée par la classe ApiException...
            // qui elle va renvoyer un not found object result Avec l'exception message à l'intérieur ]

            // requete transfomée en objet métier DTO -> Objets métier

            //appels sur services
            var jobFind = await _posteService.GetPosteByIDAsync(idPoste);

            // construction de la réponse Objet métier -> DTO
            return Ok(jobFind);
        }
        #endregion


        #region CREATE
        /// <summary>
        /// Allow to create a new offer in the DB via the request Data Transfert Object.
        /// </summary>
        /// <param name="offerRequest"></param>
        /// <returns></returns>
        [HttpPost()]
        public async Task<IActionResult> CreateOffer([FromBody] TypePoste posterRequest)
        {
            // requete transfomée en objet métier DTO -> Objets métier
            var businessObject = posterRequest;

            //appels sur services
            var newJob = await _posteService.CreatePosteAsync(businessObject);

            // construction de la réponse Objet métier -> DTO
            return Ok(newJob);
        }
        #endregion

        #region UPDATE
        [HttpPut("{id}")]
        public async Task<IActionResult> UpdateOffer([FromRoute] int id, [FromBody] TypePoste updateOfferDTORequest)
        {
            if (id != updateOfferDTORequest.IdPoste)
            {
                //code 400
                return BadRequest();
            }
            else
            {
                // requete transfomée en objet métier DTO -> Objets métier
                var businessObject = updateOfferDTORequest;

                //appel sur service
                var offerRetrieved = await _posteService.UpdatePosteAsync(businessObject);

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
        [HttpDelete("{idJob}")]
        public async Task<IActionResult> DeleteOffer([FromRoute(Name = "idJob")] TypePoste idRequestDTO)
        {
            // requete transfomée en objet métier DTO -> Objets métier
            var idRequest = idRequestDTO.IdPoste;

            //appels sur services
            await _posteService.DeletePosteAsync(idRequest);

            //Response
            return NoContent();
        }
        #endregion
    }
}
