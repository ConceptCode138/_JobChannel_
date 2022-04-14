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
    /// [ Au moment de l'utilisation de cette interface, il faudra donner un type qui dérive de Entity / Commun à tous les répertoires]
    /// création de cette interface ressemblant à un répertoire typé (offre, typeContrat,...)
    /// Création de l'interface du type <T> déclaré (au moment de l'utilisation de l'interface),
    /// permettant ainsi d'utilisé <T>  dans toute cette interface.
    /// where permet de dire que cela soit un type qui dérive/enfant d'une classe ou d'une entité ou du type de classe souhaité
    /// Les répertoires sont que pour les entités pas les DTO car pas de persistance sur les DTO.
    /// </summary>
    public interface IGenericRepository<T> where T : Entity
    {
        /// <summary>
        /// [ Research an entity by the ID ]
        /// </summary>
        /// <param name="id">id of entity to research </param>
        /// <returns>retourne le type de l'entité</returns>
        Task<T> GetByIdAsync(int id);

        /// <summary>
        /// [ Allow to retrieve all entities presents in the repertory. ]
        /// </summary>
        /// <returns>liste</returns>
        Task<IEnumerable<T>> FindAllAsync();

        /// <summary>
        /// [Allow to add an entity present in the repertory.]
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        Task<T> ADDAsync(T entity);

        /// <summary>
        /// [ Allow to delete an entity present in the repertory. ]
        /// </summary>
        /// <param name="idEntity"></param>
        /// <returns></returns>
        Task<int> DeleteAsync(int idEntity);

        Task<T> ModifyAsync(T entity);
    }
}
