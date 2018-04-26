using Domain;
using FluentAssertions;
using MicroBreweryCatalog.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using System.Collections.Generic;
using Xunit;

namespace DomainUnitTestProject
{
    public class MicrobreweryControllerTest
    {
        [Fact]
        public void GettingAllMicrobreweriesReturnsOkObjectResult()
        {
            var repo = Substitute.For<IMicrobreweryRepository>();

            MicrobreweryController mc = new MicrobreweryController(repo);

            mc.GetAll().Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GettingAllBeersReturnsOkObjectResult()
        {
            var repo = Substitute.For<IMicrobreweryRepository>();

            MicrobreweryController mc = new MicrobreweryController(repo);

            mc.GetAllBeers().Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GettingAllBeersFromAMicrobreweryReturnsOkObjectResult()
        {
            var repo = Substitute.For<IMicrobreweryRepository>();

            MicrobreweryController mc = new MicrobreweryController(repo);

            repo.Get(Arg.Any<Guid>()).Returns(new Microbrewery());

            mc.GetAllBeers(Guid.NewGuid()).Should().BeOfType<OkObjectResult>();
        }

        [Fact]
        public void GettingByNameAndNotFoundReturnsNotFoundResult()
        {
            var repo = Substitute.For<IMicrobreweryRepository>();

            repo.GetByName("Halibut").Returns(null as Microbrewery);

            MicrobreweryController mc = new MicrobreweryController(repo);

            mc.GetByName("Halibut").Should().BeOfType<NotFoundObjectResult>();
        }

        [Fact]
        public void AddingABeerToAMicrobreweryReturnsOkObjectResult()
        {
            Beer newBeer = new Beer
            {
                Id = Guid.NewGuid(),
                Abv = 3.2m,
                IsGlutenFree = true,
                Name = "Keelhaul Surprise!"
            };

            var repo = Substitute.For<IMicrobreweryRepository>();
            
            MicrobreweryController mc = new MicrobreweryController(repo);

            mc.AddBeerToBrewery(Guid.NewGuid(), newBeer);

            repo.Received().AddBeer(Arg.Any<Guid>(), newBeer);
        }

        [Fact]
        public void AddingMultipleABeersToAMicrobreweryReturnsOkObjectResult()
        {
            HashSet<Beer> beers = new HashSet<Beer> {

            new Beer
            {
                Id = Guid.NewGuid(),
                Abv = 3.2m,
                IsGlutenFree = true,
                Name = "Keelhaul Surprise!"
            },new Beer
            {
                Id = Guid.NewGuid(),
                Abv = 3.1m,
                IsGlutenFree = true,
                Name = "Brig"
            },new Beer
            {
                Id = Guid.NewGuid(),
                Abv = 5.2m,
                IsGlutenFree = false,
                Name = "Cutlass IPA"
            }};

            var repo = Substitute.For<IMicrobreweryRepository>();

            MicrobreweryController mc = new MicrobreweryController(repo);

            mc.AddBeersToBrewery(Guid.NewGuid(), beers);

            repo.Received().AddBeers(Arg.Any<Guid>(), beers);
        }

        [Fact]
        public void DeletingABeerFromAMicrobreweryReturnsOkObjectResult()
        {
            Beer newBeer = new Beer
            {
                Id = Guid.NewGuid(),
                Abv = 3.2m,
                IsGlutenFree = true,
                Name = "Keelhaul Surprise!"
            };
            
            var repo = Substitute.For<IMicrobreweryRepository>();

            MicrobreweryController mc = new MicrobreweryController(repo);

            mc.DeleteBeer(Guid.NewGuid(), newBeer.Id);

            repo.Received().DeleteBeer(Arg.Any<Guid>(), newBeer.Id);
        }
    }
}
