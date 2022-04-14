using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests.FiltersDTO
{
    public class PageDTOWithFilters : PageOffreDTORequest
    {
        #region PROPERTIES
        /// <summary>
        /// Filtering by job type
        /// </summary>
        public TypePosteDTORequest Poste { get; set; }
        /// <summary>
        /// Filtering by contract type
        /// </summary>
        public TypeContratDTORequest Contrat { get; set; }
        /// <summary>
        /// Filtering by location
        /// </summary>
        public RegionDTORequest Region { get; set; }
        /// <summary>
        /// Filtering by date min
        /// </summary>
        public DateTime MinDate { get; set; }
        /// <summary>
        /// Filtering by date max
        /// </summary>
        public DateTime MaxDate { get; set; }

        #endregion

        #region CONSTRUCTORS
        public PageDTOWithFilters() { }
        #endregion
    }
}
