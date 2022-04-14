using BLL.Interfaces;
using BO.Entities;
using BO.Exceptions;
using DAL.UOW;
using DAL.UOW.Repositories;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BLL.Services
{
    class PosteService : IPosteService
    {
        #region CONSTANTES
        private string PATH = @"C:\Users\cda4rive\OneDrive - AMIO\ACTIVITE - 3\JALON - 4\ProjetCSharpFR\traceJobChannel.txt";

        private readonly ITraceService _traceService;
        private readonly IUnitOfWork _uow;

        private readonly ILogger<PosteService> _logger;
        #endregion



        #region CONSTRUCTORS

        public PosteService(ITraceService traceService, IUnitOfWork uow, ILogger<PosteService> logger)
        {
            _traceService = traceService;
            _uow          = uow;
            _logger       = logger;
        }
        #endregion

        public async Task<TypePoste> CreatePosteAsync(TypePoste poste)
        {
            _logger.LogDebug($"-> CreatePosteAsync() <- CREATION OF A CURRENT JOB TYPE");

            //action
            //retrieve the repertory of offers with UOW in order to call the method allowing to add a new offer.
            var jobRepository = _uow.GetRepository<ITypePosteRepository>();

            //Call this method for inserting a new offer.
            var newJob = await jobRepository.ADDAsync(poste);

            if (newJob != null)
            {
                //Add a track on file text previously created
                using (StreamWriter streamWriter = File.AppendText(PATH))
                {
                    streamWriter.WriteLine(_traceService.TraceOnFileTXT($"a ajouté un nouveau type de poste portant l'id: {newJob.IdPoste}"));
                }

                return newJob;
            }
            else
            {
                throw new NotFoundException($"{nameof(newJob)} pour id ({newJob.IdPoste})");
            }
        }

        public async Task DeletePosteAsync(int idPoste)
        {
            //before action
            _logger.LogDebug($"-> DeletePosteAsync(int idOffre) <- DELETING OF A CURRENT JOB TYPE");

            //action
            //retrieve the repertory of offers with UOW in order to call the method allowing to delete an offer.
            var jobRepository = _uow.GetRepository<ITypePosteRepository>();

            //Call this method for deleting an offer.
            var job = await jobRepository.DeleteAsync(idPoste);

            if (job == 0)
            {
                throw new NotFoundException($"{nameof(TypePoste)} with id ({idPoste}) wasn't exist");
            }
            else
            {
                if (File.Exists(PATH))
                {
                    //Add a track on file text previously created
                    using (StreamWriter streamWriter = File.AppendText(PATH))
                    {
                        streamWriter.WriteLine(_traceService.TraceOnFileTXT($"SUPPRESSION type de poste numéro: {idPoste}"));
                    }
                }
            }
        }

        public async Task<IEnumerable<TypePoste>> GetAllPostesAsync()
        {
            //before action
            _logger.LogDebug($"-> GetAllPostesAsync() <- RETRIEVE ALL jobs");

            //action
            // récupération du répertoire des offres grace à l'UOW, afin d'appeler la méthode permettant de récupérer toutes les offres.
            var jobRepository = _uow.GetRepository<ITypePosteRepository>();

            //appel de la méthode pour récupérer la liste d'offres.
            var jobs = await jobRepository.FindAllAsync();

            if (File.Exists(PATH))
            {
                //Add a track on file text previously created
                using (StreamWriter streamWriter = File.AppendText(PATH))
                {
                    streamWriter.WriteLine(_traceService.TraceOnFileTXT($"a récupéré {jobs.Count()} job(s)"));
                }
            }

            _traceService.TraceOnServer($"-> {jobs.Count()} job(s) récupérée(s)...");

            return jobs;
        }

        public async Task<TypePoste> GetPosteByIDAsync(int idPoste)
        {
            //before action
            _traceService.TraceOnServer($"Récupération du type poste: {idPoste} en cours...");

            // récupération du répertoire des offres grace UOW, afin d'appeler la méthode permettant de récupérer toutes les offres.
            var jobRepository = _uow.GetRepository<ITypePosteRepository>();

            //appel de la méthode pour récupérer la liste d'offres.
            var job = await jobRepository.GetByIdAsync(idPoste);

            if (job is null)
            {
                throw new NotFoundException(nameof(job), new { Id = idPoste });
            }
            else
            {
                //Add a track on file text previously created
                using (StreamWriter streamWriter = File.AppendText(PATH))
                {
                    streamWriter.WriteLine(_traceService.TraceOnFileTXT($"a récupéré le type poste :{job.IdPoste} -> {job.IntitulePoste}"));
                }

                _traceService.TraceOnServer($"-> Le type poste :{job.IdPoste} a été récupérée...");

                return job;
            }
        }

        public Task<TypePoste> UpdatePosteAsync(TypePoste poste)
        {
            throw new NotImplementedException();
        }
    }
}
