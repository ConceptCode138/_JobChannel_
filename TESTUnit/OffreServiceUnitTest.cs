using BLL.Interfaces;
using BLL.Services;
using BO.DTO.DTORequests.FiltersDTO;
using BO.Entities;
using BO.Exceptions;
using DAL.UOW;
using DAL.UOW.Repositories;
using Microsoft.Extensions.Logging;
using Moq;
using System;
using System.Collections.Generic;
using System.Linq;
using Xunit;

namespace TESTUnit
{
    /// <summary>
    /// Allows to Test the 'OffreService' class
    /// </summary>
    public class OffreServiceUnitTest
    {
        /// <summary>
        /// This method allows to test all conditions in method
        /// </summary>
        [Fact]
        public async void GetAllOffersAsyncShouldReturnAListOfOffers()
        {
            #region ARRANGE
            #region In order to control the dependencies with the framework 'Moq'-> Sub-systems
            // creation implementing resembling at 'ITraceService'
            var traceService    = Mock.Of<ITraceService>();
            var loggerService   = Mock.Of<ILogger<OffreService>>();
            var unitOfWork      = Mock.Of<IUnitOfWork>();
            var offreRepository = Mock.Of<IOffreRepository>();
            var pageDTO         = Mock.Of<PageDTOWithFilters>();
            #endregion
            pageDTO.Page = 1;
            pageDTO.NbDataPerPage = 20;
            pageDTO.MaxDate = Convert.ToDateTime("2022/04/08");
            pageDTO.MinDate = Convert.ToDateTime("2022/02/02");

            Mock.Get(offreRepository).Setup(mock => mock.FindAllWithFilter(pageDTO)).ReturnsAsync(new List<Offre>
            {
                new Offre()
                {
                    IdOffre      = 1,
                    Title        = "Titre 1 test",
                    Description  = "Description 1 test",
                    DtCreated    = Convert.ToDateTime("02/02/2022"),
                    Link         = "test 1",
                    Contrat      = new TypeContrat(),
                    Localisation = new Region(),
                    Poste        = new TypePoste()
                },
                new Offre()
                {
                    IdOffre      = 2,
                    Title        = "Titre 2 test",
                    Description  = "Description 2 test",
                    DtCreated    = Convert.ToDateTime("02/02/2022"),
                    Link         = "test 2",
                    Contrat      = new TypeContrat(),
                    Localisation = new Region(),
                    Poste        = new TypePoste()
                },
                new Offre()
                {
                    IdOffre      = 3,
                    Title        = "Titre 3 test",
                    Description  = "Description 3 test",
                    DtCreated    = Convert.ToDateTime("02/02/2022"),
                    Link         = "test 3",
                    Contrat      = new TypeContrat(),
                    Localisation = new Region(),
                    Poste        = new TypePoste()
                }
            });
            #region FindAll without params
            Mock.Get(offreRepository).Setup(mock => mock.FindAllAsync()).ReturnsAsync(new List<Offre>
            {
                new Offre()
                {
                    IdOffre      = 1,
                    Title        = "Titre 1 test",
                    Description  = "Description 1 test",
                    DtCreated    = Convert.ToDateTime("02/02/2022"),
                    Link         = "test 1",
                    Contrat      = new TypeContrat(),
                    Localisation = new Region(),
                    Poste        = new TypePoste()
                },
                new Offre()
                {
                    IdOffre      = 2,
                    Title        = "Titre 2 test",
                    Description  = "Description 2 test",
                    DtCreated    = Convert.ToDateTime("02/02/2022"),
                    Link         = "test 2",
                    Contrat      = new TypeContrat(),
                    Localisation = new Region(),
                    Poste        = new TypePoste()
                },
                new Offre()
                {
                    IdOffre      = 3,
                    Title        = "Titre 3 test",
                    Description  = "Description 3 test",
                    DtCreated    = Convert.ToDateTime("02/02/2022"),
                    Link         = "test 3",
                    Contrat      = new TypeContrat(),
                    Localisation = new Region(),
                    Poste        = new TypePoste()
                },
                new Offre()
                {
                    IdOffre      = 4,
                    Title        = "Titre 4 test",
                    Description  = "Description 4 test",
                    DtCreated    = Convert.ToDateTime("02/02/2022"),
                    Link         = "test 4",
                    Contrat      = new TypeContrat(),
                    Localisation = new Region(),
                    Poste        = new TypePoste()
                }
            });
            #endregion

            //when calling the unitOfWork mock, on the method -> GetRepository with the type -> IOffreRepository , returning offreRepository (which is also a mock)
            Mock.Get(unitOfWork).Setup(mock => mock.GetRepository<IOffreRepository>()).Returns(offreRepository);

            IOffreService offreService = new OffreService(traceService, unitOfWork, loggerService);
            #endregion

            #region ACTION
            var offersWithPagination = await offreService.GetAllOffresInCatalogAsync(pageDTO);
            var offersWithoutPagination = await offreService.GetAllOffresInCatalogAsync(new PageDTOWithFilters() {Page=0, NbDataPerPage=0});

            var notFoundOffer = await offreService.GetAllOffresInCatalogAsync(pageDTO);
            #endregion

            #region ASSERT
            Assert.Equal(3, offersWithPagination.Count());
            Assert.Equal(4, offersWithoutPagination.Count()); // 4 without pagination
            #endregion
        }

        [Fact]
        public async void GetOfferByIdAsyncShouldBeOK()
        {
            #region ARRANGE
            #region In order to control the dependencies with the framework 'Moq'-> Sub-systems
            // creation implementing resembling at 'ITraceService'
            var traceService    = Mock.Of<ITraceService>();
            var loggerService   = Mock.Of<ILogger<OffreService>>();
            var unitOfWork      = Mock.Of<IUnitOfWork>();
            var offreRepository = Mock.Of<IOffreRepository>();
            var pageDTO         = Mock.Of<PageDTOWithFilters>();
            #endregion
            pageDTO.Page          = 1;
            pageDTO.NbDataPerPage = 20;
            pageDTO.MaxDate       = Convert.ToDateTime("2022/04/08");
            pageDTO.MinDate       = Convert.ToDateTime("2022/02/02");

            Mock.Get(offreRepository).Setup(mock => mock.GetByIdAsync(10)).ReturnsAsync(new Offre()
            {
                IdOffre      = 10,
                Title        = "Titre 1 test",
                Description  = "Description 1 test",
                DtCreated    = Convert.ToDateTime("02/02/2022"),
                Link         = "test 1",
                Contrat      = new TypeContrat(),
                Localisation = new Region(),
                Poste        = new TypePoste()
            }
            );
            Mock.Get(offreRepository).Setup(mock => mock.GetByIdAsync(5)).ReturnsAsync(null as Offre);

            //when calling the unitOfWork mock, on the method -> GetRepository with the type -> IOffreRepository , returning offreRepository (which is also a mock)
            Mock.Get(unitOfWork).Setup(mock => mock.GetRepository<IOffreRepository>()).Returns(offreRepository);

            IOffreService offreService = new OffreService(traceService, unitOfWork, loggerService);
            #endregion

            #region ACTION
            var offer           = await offreService.GetOffreByIDAsync(10);
            var offerNegativeID = offreService.GetOffreByIDAsync(-10);
            #endregion

            #region ASSERT
            //Equality test.
            Assert.Equal(10, offer.IdOffre);
            //Test with value id negative.
            await Assert.ThrowsAsync<NotFoundException>((async () => await offerNegativeID));
            //Test no offer found.
            await Assert.ThrowsAsync<NotFoundException>((async () => await offreService.GetOffreByIDAsync(5)));
            #endregion
        }

        [Fact]
        public async void DeleteOffreAsyncShouldBeOK()
        {
            #region ARRANGE
            #region In order to control the dependencies with the framework 'Moq'-> Sub-systems
            // creation implementing resembling at 'ITraceService'
            var traceService = Mock.Of<ITraceService>();
            var loggerService = Mock.Of<ILogger<OffreService>>();
            var unitOfWork = Mock.Of<IUnitOfWork>();
            var offreRepository = Mock.Of<IOffreRepository>();
            #endregion
            //----------------------------------------------ffff
            Mock.Get(offreRepository).Setup(mock => mock.GetByIdAsync(10)).ReturnsAsync(new Offre()
            {
                IdOffre = 10,
                Title = "Titre 1 test",
                Description = "Description 1 test",
                DtCreated = Convert.ToDateTime("02/02/2022"),
                Link = "test 1",
                Contrat = new TypeContrat(),
                Localisation = new Region(),
                Poste = new TypePoste()
            }
            );
            Mock.Get(offreRepository).Setup(mock => mock.GetByIdAsync(5)).ReturnsAsync(null as Offre);

            //when calling the unitOfWork mock, on the method -> GetRepository with the type -> IOffreRepository , returning offreRepository (which is also a mock)
            Mock.Get(unitOfWork).Setup(mock => mock.GetRepository<IOffreRepository>()).Returns(offreRepository);

            IOffreService offreService = new OffreService(traceService, unitOfWork, loggerService);
            #endregion

            #region ACTION
            var offer = await offreService.GetOffreByIDAsync(10);
            var offerNegativeID = offreService.GetOffreByIDAsync(-10);
            #endregion

            #region ASSERT
            //Equality test.
            Assert.Equal(10, offer.IdOffre);
            //Test with value id negative.
            await Assert.ThrowsAsync<NotFoundException>((async () => await offerNegativeID));
            //Test no offer found.
            await Assert.ThrowsAsync<NotFoundException>((async () => await offreService.GetOffreByIDAsync(5)));
            #endregion
        }
    }
}
