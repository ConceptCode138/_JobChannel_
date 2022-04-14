using BLL.Interfaces;
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
    public class TypeContratController : ControllerBase
    {
        private readonly IOffreService _offreService;

        #region CONSTRUCTORS
        /// <summary>
        /// A la création de ' TypeContratController ', résolution des dépendances de OffreService. Si elle n'existe pas-> création de celle-ci car elle est de type singleton
        ///                                                                                   Ou récupération dans son contenant.
        /// </summary>
        /// <param name="offreService"></param>
        public TypeContratController(IOffreService offreService)
        {
            _offreService = offreService;
        }
        #endregion

        #region GET - LIST
        /// <summary>
        /// Allow to retrieve the list of type contract available in offers.
        /// </summary>
        /// <returns></returns>
        [HttpGet()]
        public async Task<IActionResult> GetALLTypeContracts()
        {
            var typeContrat = await _offreService.GetAllTypeContratsInOfferCatalogAsync();
            return Ok(typeContrat);
        }
        #endregion
    }
}
