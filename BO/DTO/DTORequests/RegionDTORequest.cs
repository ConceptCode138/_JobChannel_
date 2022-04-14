using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BO.DTO.DTORequests
{
    public class RegionDTORequest
    {
        /// <summary>
        /// Identity of 'regionID' -> RegionDTORequest
        /// </summary>
        //public int RegionID { get; set; }

        /// <summary>
        /// Correspond to the "RegionName > RegionDTORequest
        /// </summary>
        public string RegionName { get; set; }

        /// <summary>
        /// This method allow to create a new instance of Region.
        /// </summary>
        /// <returns>return a new object Region</returns>
        public Region ToRegion()
        {
            return new Region()
            {
                //IdRegion       = RegionID,
                IntituleRegion = RegionName
            };
        }
    }
}
