using Domain;
using FluentAssertions;
using MicroBreweryCatalog.Controllers;
using Microsoft.AspNetCore.Mvc;
using NSubstitute;
using System;
using Xunit;

namespace DomainUnitTestProject
{
    public class MicrobreweryControllerTest
    {
        [Fact]
        public void GettingAllReturnsOkObjectResult()
        {
            var repo = Substitute.For<IMicrobreweryRepository>();

            MicrobreweryController mc = new MicrobreweryController(repo);

            mc.GetAll().Should().BeOfType<OkObjectResult>();
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
        public void AddinABeerToAMicrobreweryReturnsOkObjectResult()
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
    }
}
