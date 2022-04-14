using BO.DTO.DTORequests.FiltersDTO;
using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW.Repositories
{
    /// <summary>
    /// Cette interface permet de créer des méthodes spécifique à l'offre en plus.
    /// Plus il y a de fonctionnalités dans le répertoire, plus il y aura qu'un seul appel à faire de la BLLServer à la BDD.
    /// </summary>
    public interface IOffreRepository : IGenericRepository<Offre>
    {
        /// <summary>
        /// Récupération de l'offre par son type de contrat
        /// </summary>
        /// <param name="idTypeContrat">identifiant du type de contrat</param>
        /// <returns></returns>
        public Task<Offre> GetByTypeContratIdAsync(int idTypeContrat);




        public Task<IEnumerable<Offre>> FindAllWithFilter(PageDTOWithFilters pageDTOWithFilters);

        //Récupération de l'offre par son type de poste.

    }
}
