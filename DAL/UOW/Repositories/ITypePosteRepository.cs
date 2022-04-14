using BO.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DAL.UOW.Repositories
{
    /// <summary>
    /// Cette interface permet de créer des méthodes spécifique au type de poste en plus.
    /// Plus il y a de fonctionnalités dans le répertoire, plus il y aura qu'un seul appel à faire de la BLLServer à la BDD.
    /// </summary>
    public interface ITypePosteRepository : IGenericRepository<TypePoste>
    {
        Task<TypePoste> GetPosteByIntituleAsync(string intitule);
    }
}
