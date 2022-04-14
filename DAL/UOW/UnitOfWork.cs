using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Extensions.DependencyInjection;

namespace DAL.UOW
{
    /// <summary>
    /// Pattern Unit of Work
    /// </summary>
    internal class UnitOfWork : IUnitOfWork
    {
        private readonly IDbSession _session;
        private readonly IServiceProvider _serviceProvider;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="dbSession"></param>
        /// <param name="serviceProvider">accès à l'injecteur de dépendance permettant de récupérer un service à l'intérieur de Injecteur de dépendance</param>
        public UnitOfWork(IDbSession dbSession, IServiceProvider serviceProvider)
        {
            _session = dbSession;

            _serviceProvider = serviceProvider;
        }

        public void BeginTransaction()
        {
            _session?.Connection?.BeginTransaction();
        }

        public void Commit()
        {
            _session?.Transaction?.Commit();
        }

        public void Dispose()
        {
            // ? Appel de dispose seulement si la session est non nulle
            _session?.Dispose();
        }

        public void Rollback()
        {
            //? Appel de Rollback() seulement si la session est non nulle et transaction non nulle
            //if (_session.Transaction is not null)
            //{
            //    _session.Transaction.Rollback();
            //}

            //simplification de condition
            _session?.Transaction?.Rollback();
        }

        public T GetRepository<T>()
        {
            return _serviceProvider.GetService<T>();
        }
    }
}
