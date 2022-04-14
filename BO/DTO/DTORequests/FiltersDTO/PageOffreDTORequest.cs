using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests.FiltersDTO
{
    /// <summary>
    /// This class represents the page only (wihtout data).
    /// </summary>
    public class PageOffreDTORequest
    {
        /// <summary>
        /// Current page
        /// </summary>
        public int Page { get; set; }

        /// <summary>
        /// limit data per page
        /// </summary>
        public int NbDataPerPage { get; set; }
    }
}
