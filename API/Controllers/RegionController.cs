using BLL.Interfaces;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace API.Controllers
{
    public class RegionController : ControllerBase
    {

        private readonly IOffreService _offreService;

        public RegionController(IOffreService offreService)
        {
            _offreService = offreService;
        }

        //public async Task<IActionResult> GetRegions()
        //{
        //    var regions = await _offreService.GetAllRegionsAsync();
        //    return Ok(regions);
        //}
    }
}
